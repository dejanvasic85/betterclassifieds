using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Payments.Stripe;

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [Ignore]
    [TestFixture]
    public class StripeIntegrationTests
    {
        [Test]
        public void CardCharge_ShouldWork()
        {
            var mockPaymentRepository = new Mock<IPaymentsRepository>();
            var mockLogService = new Mock<ILogService>();

            mockPaymentRepository.Setup(call => call.CreateTransaction(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.Is<PaymentType>(p => p == PaymentType.CreditCard)
                ));

            mockLogService.Setup(call => call.Info(It.IsAny<string>()));

            // Todo : Need to get the token first... This is done in the UI and then it can be processed here.
            var api = new StripeApi(mockPaymentRepository.Object, mockLogService.Object);
            api.CompletePayment(new StripeChargeRequest
            {
                StripeToken = "tok_183dsEGpwUgT3gsDPJS04y6u",
                StripeEmail = "dejanvasic24@gmail.com",
                AmountInCents = 1049,
                Description = "whatevs",
                Currency = "aud"
            });
        }
    }
}
