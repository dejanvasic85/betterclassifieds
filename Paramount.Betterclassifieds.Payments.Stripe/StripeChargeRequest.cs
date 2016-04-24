namespace Paramount.Betterclassifieds.Payments.Stripe
{
    public class StripeChargeRequest
    {
        public string StripeToken { get; set; }
        public string StripeTokenType { get; set; }
        public string StripeEmail { get; set; }
        public int AmountInCents { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}