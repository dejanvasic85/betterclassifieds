using System;
using System.Net;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Payment;
using Stripe;

namespace Paramount.Betterclassifieds.Payments.Stripe
{
    public class StripeApi : ICreditCardService
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly ILogService _logService;

        public StripeApi(IPaymentsRepository paymentsRepository, ILogService logService)
        {
            _paymentsRepository = paymentsRepository;
            _logService = logService;
        }

        public CreditCardResponse CompletePayment(StripeChargeRequest request)
        {
            try
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    Amount = request.AmountInCents,
                    Currency = request.Currency,
                    Description = request.Description,
                    SourceTokenOrExistingSourceId = request.StripeToken,
                    Capture = true
                };

                var chargeService = new StripeChargeService();
                var stripeCharge = chargeService.Create(myCharge);
                var amount = (decimal) (request.AmountInCents/100);

                _paymentsRepository.CreateTransaction(
                    request.UserId,
                    stripeCharge.BalanceTransactionId,
                    request.Description,
                    amount,
                    PaymentType.CreditCard);

                _logService.Info("Payment complete. Transaction ID " + stripeCharge.BalanceTransactionId);

                return CreditCardResponse.Success(stripeCharge.BalanceTransactionId);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("your card was declined"))
                {
                    return CreditCardResponse.Failed(ResponseType.CardDeclined);
                }
                throw;
            }
        }
    }
}
