using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly PageBrowser _pageBrowser;

        public LoginSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAsWithPassword(string username, string password)
        {
            // Navigate to the login page
            var loginPage = _pageBrowser.GoTo<LoginTestPage>();

            loginPage.WithUsername(username);
            loginPage.WithPassword(password);
            loginPage.ClickLogin();
        }
    }
}