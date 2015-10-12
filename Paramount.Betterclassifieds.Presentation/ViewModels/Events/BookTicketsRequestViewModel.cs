using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used to load the ticketing booking page
    /// </summary>
    public class BookTicketsRequestViewModel
    {
        public List<EventTicketReservedViewModel> Reservations { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}