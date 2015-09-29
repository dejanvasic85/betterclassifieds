using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventRepository
    {
        EventModel GetEventDetails(int onlineAdId);
        IEnumerable<EventTicketReservation> GetEventTicketReservationsForSession(string sessionId, bool activeOnly);
        IEnumerable<EventTicketReservation> GetEventTicketReservations(int ticketId, bool activeOnly);
        void UpdateEventTicketReservation(EventTicketReservation existingSessionReservation);        
    }
}