using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingEventTicketViewModel
    {
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public List<EventTicketFieldViewModel> EventTicketFields { get; set; }
    }
}