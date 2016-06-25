using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
                    .SingleOrDefault(e => e.EventId == eventId);

                if (eventModel?.Address != null && !eventModel.Address.AddressId.HasValue)
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

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId, bool includeBookings = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventModel = context.Events
                    .Include(e => e.Tickets)
                    .Include(e => e.Address)
                    .SingleOrDefault(e => e.OnlineAdId == onlineAdId);

                if (eventModel != null && includeBookings)
                {
                    eventModel.EventBookings = context.EventBookings
                        .Where(b => b.EventId == eventModel.EventId)
                        .Where(b => b.StatusAsString == EventBookingStatus.Active.ToString())
                        .Include(b => b.EventBookingTickets)
                        .ToList();
                }

                if (eventModel != null && eventModel.AddressId.HasValue && !eventModel.Address.AddressId.HasValue)
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
                        eventBooking.EventBookingTickets = context.EventBookingTickets.Where(e => e.EventBookingId == eventBookingId).ToList();

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
                    .Where(t => t.EventBooking.EventId == id && t.EventBooking.StatusAsString == EventBookingStatus.Active.ToString())
                    .Include(t => t.TicketFieldValues);

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

        public async Task<IEnumerable<EventGroup>> GetEventGroups(int eventId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return await context.Database
                    .SqlQuery<EventGroup>("EventGroups_GetByEventId @eventId", 
                        new SqlParameter("eventId", eventId))
                    .ToListAsync();

                //return await context.EventGroups
                //    .Where(eg => eg.EventId == eventId)
                //    .ToListAsync();
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
