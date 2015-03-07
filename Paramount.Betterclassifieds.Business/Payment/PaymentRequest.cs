using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class PaymentRequest
    {
        public string PayReference { get; set; }

        public string PayerId { get; set; }

        public BookingRateResult BookingRateResult { get; set; }

        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }
    }
}