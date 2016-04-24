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
            api.SubmitPayment(new StripeChargeRequest
            {
                StripeToken = "card_183dQkGpwUgT3gsDQ1HOggQH",
                StripeEmail = "dejanvasic24@gmail.com",
                AmountInCents = 999,
                Description = "whatevs",
                Currency = "aud"
            });
        }
    }
}
