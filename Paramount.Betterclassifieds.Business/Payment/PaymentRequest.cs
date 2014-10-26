namespace Paramount.Betterclassifieds.Business.Payment
{
    public class PaymentRequest
    {
        public string PayReference { get; set; }

        public string PayerId { get; set; }

        public PriceBreakdown PriceBreakdown { get; set; }

        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }
    }
}