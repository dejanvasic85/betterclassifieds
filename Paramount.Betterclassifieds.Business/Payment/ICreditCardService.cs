namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface ICreditCardService
    {
        StripeChargeResponse CompletePayment(StripeChargeRequest request);
    }
}