using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly PageFactory _pageFactory;

        public LoginSteps(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAsWithPassword(string username, string password)
        {
            // Navigate to the login page
            var loginPage = _pageFactory.NavigateToAndInit<LoginPage>();

            loginPage.SetUsername(username);
            loginPage.SetPassword(password);
            loginPage.ClickLogin();
        }
    }
}