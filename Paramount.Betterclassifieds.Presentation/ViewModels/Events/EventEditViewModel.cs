using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventEditViewModel
    {
        public EventEditViewModel()
        { }

        public List<EventTicketViewModel> Tickets { get; set; }
    }
}