using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.Payments.Stripe.Tests
{
    [TestClass]
    public class SimpleChargeTests
    {
        [TestMethod]
        public void CardCharge_ShouldWork()
        {
            var mockPaymentRepository = new Mock<IPaymentsRepository>();
            mockPaymentRepository.Setup(call => call.CreateTransaction(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.Is<PaymentType>(p => p == PaymentType.CreditCard)
                ));

            var api = new StripeApi(mockPaymentRepository.Object);
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
