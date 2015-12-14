using System;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPaymentRequest
    {
        public int EventPaymentRequestId { get; set; }
        public int EventId { get; set; }
        public decimal RequestedAmount { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public bool? IsComplete { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}