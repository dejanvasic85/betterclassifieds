﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                    .Include(e => e.TicketFields)
                    .Include(e => e.Address)
                    .SingleOrDefault(e => e.EventId == eventId);

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
                    eventModel.EventBookings = context.EventBookings.Where(b => b.EventId == eventModel.EventId)
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
                var eventTicket = context.EventTickets.SingleOrDefault(e => e.EventTicketId == ticketId);
                if (includeReservations && eventTicket != null)
                    eventTicket.EventTicketReservations = context.EventTicketReservations.Where(r => r.EventTicketId == eventTicket.EventTicketId).ToList();
                return eventTicket;
            }
        }

        public EventBooking GetEventBooking(int eventBookingId, bool includeTickets = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventBooking = context.EventBookings
                    .Where(b => b.EventBookingId == eventBookingId)
                    .Include(b => b.Event)
                    .SingleOrDefault();

                if (includeTickets && eventBooking != null)
                    eventBooking.EventBookingTickets = context.EventBookingTickets.Where(e => e.EventBookingId == eventBookingId).ToList();

                return eventBooking;
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

        public IEnumerable<EventBooking> GetEventBookingsForEvent(int eventId, bool includeTickets = false)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var eventBookings = context.EventBookings.Where(e => e.EventId == eventId);
                if (includeTickets)
                {
                    eventBookings.Include(e => e.EventBookingTickets);
                }
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
                var result = context.EventBookingTickets.Where(t => t.EventBooking.EventId == id)
                    .Include(t => t.TicketFieldValues)
                    .ToList();

                return result;
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
