using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventRepository
    {
        EventModel GetEventDetails(int eventId);
        EventModel GetEventDetailsForOnlineAdId(int onlineAdId);
        EventTicket GetEventTicketDetails(int ticketId, bool includeReservations = false);
        IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId);
        IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly);

        void CreateEventTicketReservation(EventTicketReservation eventTicketReservation);
        void UpdateEventTicketReservation(EventTicketReservation eventTicketReservation);
        
    }
}