using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class BookTicketsViewModel
    {
        public string Title { get; set; }

        public List<EventTicketRequestViewModel> Reservations { get; set; }
        public int ReservationExpiryMinutes { get; set; }
        public int ReservationExpirySeconds { get; set; }
        public int TotelReservationExpiryMinutes { get; set; }
    }
}