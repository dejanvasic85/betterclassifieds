using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.StepsAdmin
{
    using Pages.Admin;

    [Binding]
    public class AdminLoginPageSteps
    {
        private readonly AdminPageBrowser _browser;
        private readonly ITestDataManager _dataManager;

        public AdminLoginPageSteps(AdminPageBrowser browser, ITestDataManager dataManager)
        {
            _browser = browser;
            _dataManager = dataManager;
        }

        [Given(@"I have a registered admin account with username ""(.*)"" and password ""(.*)""")]
        public void GivenIHaveARegisteredAdminAccountWithUsernameAndPassword(string username, string password)
        {
            _dataManager.AddUserIfNotExists(username, password, username, RoleType.Administrator);
        }

        [When(@"I login to administration with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginToAdministrationWithUsernameAndPassword(string username, string password)
        {
            _browser.GoTo<AdminLoginPage>()
                .WithUsername(username)
                .WithPassword(password)
                .Login();
        }

        [Then(@"I should see the admin home page")]
        public void ThenIShouldSeeTheAdminHomePage()
        {
            var dashboard = _browser.Init<AdminDashboardPage>();

            Assert.That(dashboard.GetTitle(), Is.EqualTo("Classies Admin"));
        }

    }
}
