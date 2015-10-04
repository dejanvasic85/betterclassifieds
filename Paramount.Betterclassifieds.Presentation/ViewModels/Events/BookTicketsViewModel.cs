using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class BookTicketsViewModel
    {
        public string Title { get; set; }

        public List<EventTicketRequestViewModel> Reservations { get; set; }
        
    }
}