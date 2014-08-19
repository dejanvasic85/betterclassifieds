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

        [Given(@"I have a registered admin account name ""(.*)""")]
        public void GivenIHaveARegisteredAdminAccountName(string accountName)
        {
            // _dataManager.AddUserIfNotExists("");
            //_browser.GoTo<AdminLoginPage>()
            //    .WithUsername("dvasic")
            //    .WithPassword("paramount")
            //    .Login();
        }
    }
}
