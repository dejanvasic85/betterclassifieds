using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    using TechTalk.SpecFlow;
    using Pages;

    [Binding]
    public class AccountDetailSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _dataRepository;

        public AccountDetailSteps(PageBrowser pageBrowser, ITestDataRepository dataRepository)
        {
            _pageBrowser = pageBrowser;
            _dataRepository = dataRepository;
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
