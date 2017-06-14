using System;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPaymentRequest
    {
        public int EventPaymentRequestId { get; set; }
        public int EventId { get; set; }
        public decimal RequestedAmount { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public string PaymentMethodAsString
        {
            get { return PaymentMethod.ToString(); }
            set
            {
                PaymentType status;
                if (Enum.TryParse(value, out status))
                {
                    this.PaymentMethod = status;
                }
            }
        }

        public bool? IsPaymentProcessed { get; set; }
        public DateTime? PaymentProcessedDate { get; set; }
        public DateTime? PaymentProcessedDateUtc { get; set; }
        public string PaymentProcessedBy { get; set; }
        public string PaymentReference { get; set; }
    }
}