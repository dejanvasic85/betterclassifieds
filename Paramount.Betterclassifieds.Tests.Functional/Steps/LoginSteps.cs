using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly RegistrationContext _registrationContext;

        public LoginSteps(PageBrowser pageBrowser, RegistrationContext registrationContext)
        {
            _pageBrowser = pageBrowser;
            _registrationContext = registrationContext;
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAsWithPassword(string username, string password)
        {
            // Navigate to the login page
            _pageBrowser.GoTo<LoginTestPage>()
                .WithUsername(username)
                .WithPassword(password)
                .ClickLogin();
        }

        [Given("I am logged in as the newly added user")]
        public void LoggedInAsNewlyAddedUser()
        {
            if (_registrationContext.Username.IsNullOrEmpty())
            {
                Assert.Fail("RegistrationContext is not available");
            }

            GivenIAmLoggedInAsWithPassword(_registrationContext.Username,
                _registrationContext.Password);
        }
    }
}