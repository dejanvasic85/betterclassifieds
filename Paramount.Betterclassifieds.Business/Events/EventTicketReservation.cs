using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservation
    {
        public int EventTicketReservationId { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; }
        public bool Active { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}