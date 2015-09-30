using System.Collections.Generic;
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
                return context.Events
                    .Include(e => e.Tickets)
                    .SingleOrDefault(e => e.EventId == eventId);
            }
        }

        public EventModel GetEventDetailsForOnlineAdId(int onlineAdId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.Events
                    .Include(e => e.Tickets)
                    .SingleOrDefault(e => e.OnlineAdId == onlineAdId);
            }
        }

        public EventTicket GetEventTicketDetails(int ticketId, bool includeReservations)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var query = context.EventTickets;
                if (includeReservations) { query.Include(t => t.EventTicketReservations); }
                return query.SingleOrDefault(e => e.TicketId == ticketId);
            }
        }

        public IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.EventTicketReservations.Where(reservation => reservation.SessionId == sessionId).ToList();
            }
        }

        public IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                var query = context.EventTicketReservations.Where(reservation => reservation.TicketId == ticketId);
                if (activeOnly) { query = query.Where(reservation => reservation.Active); }
                return query.ToList();
            }
        }

        public void CreateEventTicketReservation(EventTicketReservation eventTicketReservation)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEventTicketReservation(EventTicketReservation eventTicketReservation)
        {
            // Todo return only active and non-expired
        }

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
