using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used to load the ticketing booking page
    /// </summary>
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
        public string Description { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public int? EventId { get; set; }
    }
}