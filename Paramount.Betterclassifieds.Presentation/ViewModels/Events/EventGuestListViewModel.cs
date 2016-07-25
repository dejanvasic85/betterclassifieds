using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGuestListViewModel
    {
        public int TicketNumber { get; set; }
        public string TicketName { get; set; }
        public string BarcodeData { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public EventTicketFieldViewModel[] DynamicFields { get; set; }
    }
}