using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationResult
    {
        public EventTicketReservationResult()
        {
            Reservations = new Collection<EventTicketReservation>();
            FailedReservations = new Collection<EventTicketFailedReservation>();
        }

        public ICollection<EventTicketReservation> Reservations { get; set; }
        public ICollection<EventTicketFailedReservation> FailedReservations { get; set; }

    }
}