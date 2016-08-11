using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventGuestDetails
    {
        public EventGuestDetails()
        {
            this.DynamicFields = new List<EventBookingTicketField>();
        }

        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public IList<EventBookingTicketField> DynamicFields { get; set; }
        public string BarcodeData { get; set; }
        public string TicketName { get; set; }
        public int TicketNumber { get; set; }
        public decimal TotalTicketPrice { get; set; }
        public DateTime DateOfBooking { get; set; }
        public DateTime DateOfBookingUtc { get; set; }
    }
}