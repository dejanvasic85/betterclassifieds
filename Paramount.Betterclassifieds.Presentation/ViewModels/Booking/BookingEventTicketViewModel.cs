using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingEventTicketViewModel
    {
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
    }
}