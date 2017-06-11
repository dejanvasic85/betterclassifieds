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
        public int TicketId { get; set; }
        public string GroupName { get; set; }
        public bool IsPublic { get; set; }
        public string SeatNumber { get; set; }
    }

    public class EventGuestPublicView
    {
        public EventGuestPublicView(string guestName, string groupName)
        {
            this.GuestName = guestName;
            this.GroupName = groupName;
        }

        public string GuestName { get; private set; }
        public string GroupName { get; private set; }
    }
}