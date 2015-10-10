using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class BookTicketsViewModel
    {
        public int AdId { get; set; }
        public string Title { get; set; }

        public List<EventTicketReservedViewModel> Reservations { get; set; }
        public bool OutOfTime { get; set; }
        public int ReservationExpiryMinutes { get; set; }
        public int ReservationExpirySeconds { get; set; }
        public int TotelReservationExpiryMinutes { get; set; }
        public string CategoryAdType { get; set; }
        public int SuccessfulReservationCount { get; set; }
        public int LargeRequestCount { get; set; }
    }
}