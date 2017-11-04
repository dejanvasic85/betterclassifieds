using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventRepository : IEventRepository, ICategoryAdRepository<ICategoryAd>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public EventRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public EventModel GetEventDetails(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventModel = context.Events
                    .Include(e => e.Tickets)
                    .Include(e => e.Address)
                    .Include(e => e.EventOrganisers)
                    .Include(e => e.PromoCodes)
                    .SingleOrDefault(e => e.EventId == eventId);

                if (eventModel == null)
                    return null;

                if (eventModel.Address != null && !eventModel.Address.AddressId.HasValue)
                {
                    // For some reason entity framework is not loading the address object here!
                    eventModel.Address = context.Addresses.SingleOrDefault(a => a.AddressId == eventModel.AddressId);
                }

                foreach (var eventTicket in eventModel.With(e => e.Tickets))
                {
                    eventTicket.EventTicketFields = context.EventTicketFields
                        .Where(f => f.EventTicketId == eventTicket.EventTicketId)
                        .ToList();
                }

                return eventModel;
            }
        }

        public IEnumerable<EventModel> GetEventsForOrganiser(string username)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventOrganisers
                    .Where(eo => eo.UserId == username)
                    .Select(eo => eo.Event)
                    .ToList();
            }
        }

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventModel = context.Events
                    .Include(e => e.Tickets)
                    .Include(e => e.Address)
                    .Include(e => e.EventOrganisers)
                    .SingleOrDefault(e => e.OnlineAdId == onlineAdId);

                if (eventModel != null && includeBookings)
                {
                    eventModel.EventBookings = context.EventBookings
                        .Where(b => b.EventId == eventModel.EventId)
                        .Where(b => b.StatusAsString == EventBookingStatus.Active.ToString())
                        .Include(b => b.EventBookingTickets)
                        .ToList();
                }

                if (eventModel?.AddressId != null && !eventModel.Address.AddressId.HasValue)
                {
                    eventModel.Address = context.Addresses.Single(a => a.AddressId == eventModel.AddressId);
                }

                return eventModel;
            }
        }

        public EventTicket GetEventTicketDetails(int ticketId, bool includeReservations)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventTicket = context.EventTickets
                    .Include(e => e.EventTicketFields)
                    .SingleOrDefault(e => e.EventTicketId == ticketId);

                if (includeReservations && eventTicket != null)
                    eventTicket.EventTicketReservations = context.EventTicketReservations.Where(r => r.EventTicketId == eventTicket.EventTicketId).ToList();

                return eventTicket;
            }
        }

        public EventBooking GetEventBooking(int eventBookingId, bool includeTickets = false, bool includeEvent = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventBooking = context.EventBookings.SingleOrDefault(b => b.EventBookingId == eventBookingId);

                if (eventBooking != null)
                {
                    if (includeTickets)
                        eventBooking.EventBookingTickets = context.EventBookingTickets
                            .Where(e => e.EventBookingId == eventBookingId)
                            .Where(e => e.IsActive).ToList();

                    if (includeEvent)
                        eventBooking.Event = context.Events.SingleOrDefault(e => e.EventId == eventBooking.EventId);
                }

                return eventBooking;
            }
        }

        public EventBookingTicket GetEventBookingTicket(int eventBookingTicketId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventBookingTickets
                    .Where(t => t.IsActive)
                    .Include(b => b.TicketFieldValues)
                    .SingleOrDefault(t => t.EventBookingTicketId == eventBookingTicketId);
            }
        }

        public EventPaymentRequest GetEventPaymentRequestForEvent(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventPaymentRequests.Where(p => p.EventId == eventId)
                    .OrderBy(p => p.EventPaymentRequestId)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<EventBooking> GetEventBookingsForEvent(int eventId, bool activeOnly = true, bool includeTickets = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventBookings = context.EventBookings.Where(e => e.EventId == eventId);

                if (activeOnly)
                    eventBookings = eventBookings.Where(b => b.StatusAsString == EventBookingStatus.Active.ToString());

                if (includeTickets)
                    eventBookings.Include(e => e.EventBookingTickets);

                return eventBookings.ToList();
            }
        }

        public IEnumerable<EventTicketReservation> GetCurrentReservationsForEvent(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventTicketReservations
                    .Where(r => r.ExpiryDateUtc > DateTime.UtcNow)
                    .ToList();
            }
        }

        public IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventTicketReservations
                    .Where(reservation => reservation.SessionId == sessionId)
                    .Include(e => e.EventTicket)
                    .ToList();
            }
        }

        public IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var query = context.EventTicketReservations.Where(reservation => reservation.EventTicketId == ticketId);
                if (activeOnly) { query = query.Where(reservation => reservation.Status == EventTicketReservationStatus.Reserved); }
                return query.ToList();
            }
        }

        public IEnumerable<EventBookingTicket> GetEventBookingTicketsForEvent(int? eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var id = eventId.GetValueOrDefault();
                var query = context.EventBookingTickets
                    .Include(t => t.TicketFieldValues)
                    .Include(t => t.EventBooking)
                    .Where(t => t.EventBooking.EventId == id)
                    .Where(t => t.EventBooking.StatusAsString == EventBookingStatus.Active.ToString())
                    .Where(t => t.IsActive)
                    .Where(t => t.GuestFullName != null);

                return query.ToList();
            }
        }

        public EventBookingTicketValidation GetEventBookingTicketValidation(int eventBookingTicketId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventBookingTicketValidations.SingleOrDefault(v => v.EventBookingTicketId == eventBookingTicketId);
            }
        }

        public EventInvitation GetEventInvitation(long invitationId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventInvitations.SingleOrDefault(e => e.EventInvitationId == invitationId);
            }
        }

        public async Task<IEnumerable<EventGroup>> GetEventGroupsAsync(int eventId, int? eventTicketId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return await GetRawEventGroupsQuery(context, eventId, eventTicketId).ToListAsync();
            }
        }

        public IEnumerable<EventGroup> GetEventGroups(int eventId, int? eventTicketId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return GetRawEventGroupsQuery(context, eventId, eventTicketId).ToList();
            }
        }

        private DbRawSqlQuery<EventGroup> GetRawEventGroupsQuery(EventDbContext context, int eventId, int? eventTicketId)
        {
            return context.Database
                    .SqlQuery<EventGroup>("EventGroups_GetByEventId @eventId, @eventTicketId",
                        new SqlParameter("eventId", eventId),
                        new SqlParameter("eventTicketId", SqlDbType.Int) { SqlValue = eventTicketId.SqlNullIfEmpty() });

        }

        public async Task<EventGroup> GetEventGroup(int eventGroupId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return await context.Database
                    .SqlQuery<EventGroup>("EventGroups_GetById @eventGroupId",
                        new SqlParameter("eventGroupId", eventGroupId))
                    .SingleOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<EventTicket>> GetEventTickets(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return await context.EventTickets.Where(t => t.EventId == eventId).ToListAsync();
            }
        }

        public async Task<IEnumerable<EventTicket>> GetEventTicketsForGroup(int eventGroupId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return await context.Database
                    .SqlQuery<EventTicket>("EventTicket_GetByEventGroupId @eventGroupId",
                        new SqlParameter("eventGroupId", eventGroupId))
                    .ToListAsync();
            }
        }

        public EventOrganiser GetEventOrganiser(int eventOrganiserId)
        {
            using (var context = new EventDbContext())
            {
                return context.EventOrganisers.SingleOrDefault(o => o.EventOrganiserId == eventOrganiserId);
            }
        }

        public IEnumerable<EventOrganiser> GetEventOrganisersForEvent(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventOrganisers.Where(o => o.EventId == eventId).ToList();
            }
        }

        public IEnumerable<EventSeat> GetEventSeats(int eventId, int? eventTicketId = null, string seatNumber = "", string orderRequestId = "")
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.Database
                    .SqlQuery<EventSeat>("EventSeat_Get @eventId, @eventTicketId, @seatNumber, @orderRequestId",
                        new SqlParameter("eventId", SqlDbType.Int) { SqlValue = eventId },
                        new SqlParameter("eventTicketId", SqlDbType.Int) { SqlValue = eventTicketId.SqlNullIfEmpty() },
                        new SqlParameter("seatNumber", SqlDbType.VarChar) { SqlValue = seatNumber.SqlNullIfEmpty() },
                        new SqlParameter("orderRequestId", SqlDbType.VarChar) { SqlValue = orderRequestId.SqlNullIfEmpty() })
                    .ToList();
            }
        }

        public EventPromoCode GetEventPromoCode(long eventPromoCodeId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.PromoCodes.SingleOrDefault(p => p.EventPromoCodeId == eventPromoCodeId);
            }
        }

        public EventPromoCode GetEventPromoCode(int eventId, string promoCode)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.PromoCodes.SingleOrDefault(p => p.EventId == eventId
                    && p.PromoCode.Equals(promoCode, StringComparison.OrdinalIgnoreCase));
            }
        }

        public IEnumerable<EventPromoCode> GetEventPromoCodes(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.PromoCodes.Where(p => p.EventId == eventId).ToList();
            }
        }

        public EventSeat GetEventSeatByTicketAndSeatNumber(int eventTicketId, string seatNumber)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventSeats.SingleOrDefault(s => s.EventTicketId == eventTicketId && s.SeatNumber == seatNumber);
            }
        }

        public void CreateEventTicketReservation(EventTicketReservation eventTicketReservation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventTicketReservations.Add(eventTicketReservation);
                context.SaveChanges();
            }
        }

        public void CreateEventBookingTicketValidation(EventBookingTicketValidation eventBookingTicketValidation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookingTicketValidations.Add(eventBookingTicketValidation);
                context.SaveChanges();
            }
        }

        public void CreateEventInvitation(EventInvitation eventInvitation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventInvitations.Add(eventInvitation);
                context.SaveChanges();
            }
        }

        public void CreateEventGroup(EventGroup eventGroup, IEnumerable<int> tickets)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                Guard.NotNull(eventGroup);

                context.Database.ExecuteSqlCommand("EventGroup_Create @eventId, @groupName, @maxGuests, @createdDate, @createdDateUtc, @createdBy, @availableToAllTickets, @isDisabled, @tickets",
                    new SqlParameter("eventId", eventGroup.EventId),
                    new SqlParameter("groupName", eventGroup.GroupName),
                    new SqlParameter("maxGuests", SqlDbType.Int) { SqlValue = eventGroup.MaxGuests.SqlNullIfEmpty() },
                    new SqlParameter("createdDate", SqlDbType.DateTime) { SqlValue = eventGroup.CreatedDateTime.SqlNullIfEmpty() },
                    new SqlParameter("createdDateUtc", SqlDbType.DateTime) { SqlValue = eventGroup.CreatedDateTimeUtc.SqlNullIfEmpty() },
                    new SqlParameter("createdBy", SqlDbType.VarChar) { SqlValue = eventGroup.CreatedBy.SqlNullIfEmpty() },
                    new SqlParameter("availableToAllTickets", SqlDbType.Bit) { SqlValue = eventGroup.AvailableToAllTickets.SqlNullIfEmpty() },
                    new SqlParameter("isDisabled", eventGroup.IsDisabled),
                    new SqlParameter("tickets", SqlDbType.VarChar) { SqlValue = (tickets == null ? "" : string.Join(",", tickets)).SqlNullIfEmpty() });
            }
        }

        public void CreateEventOrganiser(EventOrganiser eventOrganiser)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventOrganisers.Add(eventOrganiser);
                context.SaveChanges();
            }
        }

        public void CreateEventPromoCode(EventPromoCode eventPromoCode)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.PromoCodes.Add(eventPromoCode);
                context.SaveChanges();
            }
        }

        public void UpdateEvent(EventModel eventModel)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Events.Attach(eventModel);
                context.Entry(eventModel).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventTicketReservation(EventTicketReservation eventTicketReservation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventTicketReservations.Attach(eventTicketReservation);
                context.Entry(eventTicketReservation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventBooking(EventBooking eventBooking)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookings.Attach(eventBooking);
                context.Entry(eventBooking).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventTicket(EventTicket eventTicket)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventTickets.Attach(eventTicket);
                context.Entry(eventTicket).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventTicketIncudingFields(EventTicket eventTicket)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var originalEventTicket = context.EventTickets
                    .Include(e => e.EventTicketFields)
                    .Single(e => e.EventTicketId == eventTicket.EventTicketId);

                context.Entry(originalEventTicket).CurrentValues.SetValues(eventTicket);

                for (int i = 0; i < originalEventTicket.EventTicketFields.Count; i++)
                {
                    var field = originalEventTicket.EventTicketFields[i];
                    if (!eventTicket.EventTicketFields.Any(f => f.FieldName.Equals(field.FieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.EventTicketFields.Remove(field);
                    }
                }

                foreach (var eventTicketField in eventTicket.EventTicketFields)
                {
                    var originalField = context.EventTicketFields
                        .Where(f => f.FieldName.Equals(eventTicketField.FieldName, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault(f => f.EventTicketId == eventTicket.EventTicketId);

                    if (originalField == null)
                    {
                        originalEventTicket.EventTicketFields.Add(eventTicketField);
                    }
                    else
                    {
                        // Map the updatable properties here
                        originalField.IsRequired = eventTicketField.IsRequired;
                        context.Entry(originalField).State = EntityState.Modified;
                    }
                }

                context.SaveChanges();
            }
        }

        public void UpdateEventAddress(Address address)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Addresses.Attach(address);
                context.Entry(address).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventBookingTicketValidation(EventBookingTicketValidation eventBookingTicketValidation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookingTicketValidations.Attach(eventBookingTicketValidation);
                context.Entry(eventBookingTicketValidation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventInvitation(EventInvitation invitation)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventInvitations.Attach(invitation);
                context.Entry(invitation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventBookingTicket(EventBookingTicket eventBookingTicket)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookingTickets.Attach(eventBookingTicket);
                context.Entry(eventBookingTicket).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventBookingTicketNames(int eventTicketId, string ticketName)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Database
                    .ExecuteSqlCommand("EventBookingTicket_UpdateName @eventTicketId, @ticketName",
                        new SqlParameter("eventTicketId", eventTicketId),
                        new SqlParameter("ticketName", ticketName));
            }
        }

        public void UpdateEventBookingTicketField(EventBookingTicketField eventBookingTicketField)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookingTicketFields.Attach(eventBookingTicketField);
                context.Entry(eventBookingTicketField).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventGroupStatus(int eventGroupId, bool isDisabled)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Database.ExecuteSqlCommand(
                    "UPDATE EventGroup " +
                    "SET IsDisabled = @isDisabled " +
                    "WHERE EventGroupId = @eventGroupId",
                    new SqlParameter("isDisabled", isDisabled),
                    new SqlParameter("eventGroupId", eventGroupId));
            }
        }

        public void UpdateEventOrganiser(EventOrganiser eventOrganiser)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventOrganisers.Attach(eventOrganiser);
                context.Entry(eventOrganiser).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventSeat(EventSeat eventSeat)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventSeats.Attach(eventSeat);
                context.Entry(eventSeat).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateEventPromoCode(EventPromoCode promo)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.PromoCodes.Attach(promo);
                context.Entry(promo).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void CreateEventTicket(EventTicket ticket)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventTickets.Add(ticket);
                context.Entry(ticket).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void CreateEventBookingTicket(EventBookingTicket eventBookingTicket)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookingTickets.Add(eventBookingTicket);
                context.Entry(eventBookingTicket).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void CreateEventPaymentRequest(EventPaymentRequest request)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventPaymentRequests.Add(request);
                context.Entry(request).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void CreateBooking(EventBooking eventBooking)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventBookings.Add(eventBooking);
                context.Entry(eventBooking).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void DeleteEventTicket(int eventId, string ticketName)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var ticket = context.EventTickets.SingleOrDefault(e => e.EventId == eventId && e.TicketName.Equals(ticketName, StringComparison.OrdinalIgnoreCase));
                if (ticket == null)
                {
                    Debug.Write($"The ticket {ticketName} for event Id {eventId} does not exist and cannot be deleted");
                    return;
                }

                context.EventTickets.Remove(ticket);
                context.SaveChanges();
            }
        }

        public void DeleteEventSeat(EventSeat seat)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.EventSeats.Attach(seat);
                context.EventSeats.Remove(seat);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// Supports the dynamic category ad types for the ad booking system. See <see cref="Business.Booking.BookingManager"/>
        /// </summary>
        /// <param name="model"></param>
        public void Add(ICategoryAd model)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Events.Add(model as EventModel);
                context.SaveChanges();
            }
        }
    }
}
