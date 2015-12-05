using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGuestListDownloadViewModel
    {
        public string EventName { get; set; }
        public List<EventGuestListViewModel> Guests { get; set; }
    }
}