using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingSteps
    {
        private readonly IPageFactory _pageFactory;
        private readonly PageNavigator _navigator;

        public BookingSteps(IPageFactory pageFactory, PageNavigator navigator)
        {
            _pageFactory = pageFactory;
            _navigator = navigator;
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAsWithPassword(string username, string password)
        {
            // Navigate to the login page
            _navigator.NavigateTo<LoginPage>();

            // Log in
            var loginPage = _pageFactory.Resolve<LoginPage>();
            loginPage.SetUsername(username);
            loginPage.SetPassword(password);
            loginPage.ClickLogin();
        }

    }
}
