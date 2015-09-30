using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketBooking
    {
        public int EventTicketBookingId { get; set; }
        public int TicketId { get; set; }
        public string UserEmail { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public bool Active { get; set; }
    }
}