using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGuestListViewModel
    {
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public List<EventTicketFieldViewModel> DynamicFields { get; set; }
        public string BarcodeData { get; set; }
    }
}