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
                .WithPhone(phone)
                .Submit();
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
