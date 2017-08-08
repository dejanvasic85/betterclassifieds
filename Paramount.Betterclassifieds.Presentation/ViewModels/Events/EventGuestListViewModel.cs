using System;

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

        /// <summary>
        /// The actual online paid amount which may include discount
        /// </summary>
        public decimal TotalTicketPrice { get; set; }

        /// <summary>
        /// The original price of the ticket type. Not the actual online paid amount.
        /// </summary>
        public decimal TicketPrice { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string GroupName { get; set; }
        public string SeatNumber { get; set; }
        public string PromoCode { get; set; }
    }
}