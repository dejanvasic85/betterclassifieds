using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [Binding]
    public class NavigationSteps
    {
        private readonly PageBrowser _pageBrowser;

        public NavigationSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [When(@"I navigate to relative url ""(.*)""")]
        public void WhenINavigateToRelativeUrl(string relativeUrl)
        {
            _pageBrowser.NavigateTo(relativeUrl);
        }

        [Then(@"I should see a code confirmation page")]
        public void ThenIShouldSeeACodeConfirmationPage()
        {
            var registrationPage = _pageBrowser.Init<RegistrationConfirmationPage>();

            Assert.That(registrationPage.GetConfirmationText(), Is.EqualTo("We sent you an email with a confirmation code. In order for us to ensure that the email belongs to you, please copy the number from the email in to the form below."));
            Assert.That(registrationPage.GetThankYouHeading(), Is.EqualTo("Confirmation Required"));
            Assert.That(registrationPage.IsCodeTextboxAvailable(), Is.True);
        }

        [Then(@"I should be on the registration page")]
        public void ThenIShouldBeOnTheRegistrationPage()
        {
            var page = _pageBrowser.Init<RegisterNewUserTestPage>();
            Assert.That(page, Is.Not.Null);
        }

        [Then(@"I should see the home page")]
        public void ThenIShouldSeeTheHomePage()
        {
            var homePage = _pageBrowser.Init<HomeTestPage>();

            Assert.That(homePage, Is.Not.Null);
        }
    }
}