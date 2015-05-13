using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly UserContext _userContext;

        public LoginSteps(PageBrowser pageBrowser, UserContext userContext)
        {
            _pageBrowser = pageBrowser;
            _userContext = userContext;
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAsWithPassword(string username, string password)
        {
            // Navigate to the login page
            _pageBrowser.GoTo<LoginTestPage>()
                .WithUsername(username)
                .WithPassword(password)
                .ClickLogin();

            _userContext.Username = username;
        }

        [Given("I am logged in as the newly added user")]
        public void LoggedInAsNewlyAddedUser()
        {
            if (_userContext.Username.IsNullOrEmpty())
            {
                Assert.Fail("RegistrationContext is not available");
            }

            GivenIAmLoggedInAsWithPassword(_userContext.Username,
                _userContext.Password);
        }
    }
}