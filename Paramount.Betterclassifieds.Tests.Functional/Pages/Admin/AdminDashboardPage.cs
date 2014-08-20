using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Admin
{
    [TestPage(RelativeUrl = "")]
    public class AdminDashboardPage : TestPage
    {
        public AdminDashboardPage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        {
        }


    }
}