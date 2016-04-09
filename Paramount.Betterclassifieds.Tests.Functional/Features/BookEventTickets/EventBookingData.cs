using System;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    internal class EventBookingData
    {
        public int EventBookingId { get; set; }
        public decimal TotalCost { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime TicketsSentDateUtc { get; set; }
    }
}