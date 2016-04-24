using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paramount.Betterclassifieds.Payments.Stripe.Tests
{
    [TestClass]
    public class SimpleChargeTests
    {
        [TestMethod]
        public void CardCharge_ShouldWork()
        {
            var api = new StripeApi();
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
