namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface ICreditCardService
    {
        CreditCardResponse CompletePayment(StripeChargeRequest request);
    }
}