using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class PayPalSteps
    {
        private readonly PageBrowser _browser;

        public PayPalSteps(PageBrowser browser)
        {
            _browser = browser;
        }

        [When(@"paypal payment is completed")]
        public void WhenPaypalPaymentIsCompleted()
        {
            var paypalLoginPage = _browser.Init<PayPalTestPage>(ensureUrl: false);
            paypalLoginPage
                .WaitForLoaderToFinish()
                .WithEmailAddress("support-test2@paramountit.com.au")
                .WithPassword("Paramount01")
                .Login()
                .WaitForLoaderToFinish()
                .Continue();
        }

    }
}
