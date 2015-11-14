using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventEditViewModel
    {
        public EventEditViewModel()
        { }

        public int AdId { get; set; }
        public int EventId { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; }
    }
}