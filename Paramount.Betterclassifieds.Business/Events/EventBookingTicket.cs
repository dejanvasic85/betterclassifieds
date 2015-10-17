using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicket
    {
        public int EventBookingTicketId { get; set; }
        public int EventBookingId { get; set; }
        public EventBooking EventBooking { get; set; }
        public int EventTicketId { get; set; }
        public EventTicket EventTicket { get; set; }
        public string TicketName { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
    }
}