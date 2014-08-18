using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.StepsAdmin
{
    using Pages.Admin;

    [Binding]
    public class AdminLoginPageSteps
    {
        private readonly AdminPageBrowser _browser;

        public AdminLoginPageSteps(AdminPageBrowser browser)
        {
            _browser = browser;
        }

        [Given(@"I have a registered admin account name ""(.*)""")]
        public void GivenIHaveARegisteredAdminAccountName(string p0)
        {
            // todo - setup before and after scenario
            //_browser.GoTo<AdminLoginPage>()
            //    .WithUsername("dvasic")
            //    .WithPassword("paramount")
            //    .Login();
        }
    }
}
