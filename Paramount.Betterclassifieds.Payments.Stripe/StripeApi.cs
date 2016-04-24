using Stripe;

namespace Paramount.Betterclassifieds.Payments.Stripe
{
    public class StripeApi 
    {
        public StripeChargeResponse CompletePayment(StripeChargeRequest request)
        {
            var myCharge = new StripeChargeCreateOptions();
            // always set these properties
            myCharge.Amount = request.AmountInCents;
            myCharge.Currency = request.Currency;

            // set this if you want to
            myCharge.Description = request.Description;

            myCharge.SourceTokenOrExistingSourceId = request.StripeToken;

            // set this property if using a customer - this MUST be set if you are using an existing source!
            //myCharge.CustomerId = "";

            // set this if you have your own application fees (you must have your application configured first within Stripe)
            //myCharge.ApplicationFee = 25;

            // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            myCharge.Capture = true;

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);

            return new StripeChargeResponse
            {
                TransactionId = stripeCharge.BalanceTransactionId
            };
        }
    }
}
