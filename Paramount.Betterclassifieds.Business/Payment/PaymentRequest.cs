using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            this.ChargeableItems = new List<ChargeableItem>();
        }
        public string PayReference { get; set; }
        
        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }

        public IList<ChargeableItem> ChargeableItems { get; set; }
        public decimal Total { get; set; }
    }
}