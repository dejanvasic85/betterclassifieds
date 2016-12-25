using System;
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
        public decimal TicketTotalPrice { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string GroupName { get; set; }
    }
}