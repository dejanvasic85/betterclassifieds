using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventSeatBooking
    {
        public long EventSeatId { get; set; }
        public string SeatNumber { get; set; }
        public bool? NotAvailableToPublic { get; set; }
        public int? EventTicketId { get; set; } 
        public EventTicket EventTicket { get; set; }
    }
}