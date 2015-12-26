using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    using NUnit.Framework;
    using Pages;
    using TechTalk.SpecFlow;

    [Binding]
    public class AccountDetailSteps
    {
        private readonly PageBrowser _pageBrowser;

        public AccountDetailSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }
        
        [When(@"I go to MyAccountDetails page")]
        public void WhenIGoToMyAccountDetailsPage()
        {
            _pageBrowser.NavigateTo<AccountDetailsTestPage>();
        }


        [When(@"Update my address ""(.*)"", PhoneNumber ""(.*)""")]
        public void WhenUpdateMyAddressPhoneNumber(string addressLine1, string phone)
        {
            _pageBrowser.Init<AccountDetailsTestPage>()
                .WithAddressLine1(addressLine1)
                .WithPhone(phone);
        }

        [When(@"Update my paypal email ""(.*)""")]
        public void WhenUpdateMyPaypalEmail(string paypalEmail)
        {
            _pageBrowser.Init<AccountDetailsTestPage>(ensureUrl: false)
                .WithPayPalEmail(paypalEmail);
        }

        [When(@"Update my direct debit details ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)""")]
        public void WhenUpdateMyDirectDebitDetails(string bankName, 
            string accountName, string bsb, string accountNumber)
        {
            _pageBrowser.Init<AccountDetailsTestPage>(ensureUrl: false)
                .WithBankDetails(bankName, bsb, accountNumber, accountName);
        }

        [When(@"Update preferred payment method to be ""(.*)""")]
        public void WhenUpdatePreferredPaymentMethodToBe(string paymentMethod)
        {
            var page = _pageBrowser.Init<AccountDetailsTestPage>(ensureUrl: false);

            if (paymentMethod.Equals("Direct Debit"))
            {
                page.WithDirectDebitPaymentMethod();
            }

            if (paymentMethod.Equals("PayPal"))
            {
                page.WithPayPalPaymentMethod();
            }
        }

        [When(@"Submit my account changes")]
        public void WhenSubmitMyAccountChanges()
        {
            _pageBrowser.Init<AccountDetailsTestPage>().Submit();
        }


        [Then(@"I should see details updated message")]
        public void ThenIShouldSeeDetailsUpdatedMessage()
        {
            var result = _pageBrowser.Init<AccountDetailsTestPage>()
                .IsSuccessMessageDisplayed();

            Assert.That(result, Is.True);
        }

    }
}
