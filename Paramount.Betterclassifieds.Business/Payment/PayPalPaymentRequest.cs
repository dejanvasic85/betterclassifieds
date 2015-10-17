using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class PayPalPaymentRequest
    {
        public PayPalPaymentRequest()
        {
            this.ChargeableItems = new List<PayPalChargeableItem>();
        }
        public string PayReference { get; set; }
        
        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }

        public IList<PayPalChargeableItem> ChargeableItems { get; set; }
        public decimal Total { get; set; }
    }
}