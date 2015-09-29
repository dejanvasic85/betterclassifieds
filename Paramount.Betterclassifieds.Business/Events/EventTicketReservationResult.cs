using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationResult
    {
        public IEnumerable<EventTicketReservation> Reservations { get; set; }
    }
}