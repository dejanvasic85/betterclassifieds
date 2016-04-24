using Paramount.Betterclassifieds.Business.Payment;
using Stripe;

namespace Paramount.Betterclassifieds.Payments.Stripe
{
    public class StripeApi : ICreditCardService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        
        public StripeApi(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public StripeChargeResponse CompletePayment(StripeChargeRequest request)
        {
            var myCharge = new StripeChargeCreateOptions();
            myCharge.Amount = request.AmountInCents;
            myCharge.Currency = request.Currency;
            myCharge.Description = request.Description;
            myCharge.SourceTokenOrExistingSourceId = request.StripeToken;
            myCharge.Capture = true;

            var chargeService = new StripeChargeService();
            var stripeCharge = chargeService.Create(myCharge);
            var amount = (decimal)(request.AmountInCents / 100);

            _paymentsRepository.CreateTransaction(
                request.UserId,
                stripeCharge.BalanceTransactionId,
                request.Description,
                amount,
                PaymentType.CreditCard);

            return new StripeChargeResponse
            {
                TransactionId = stripeCharge.BalanceTransactionId
            };
        }
    }
}
