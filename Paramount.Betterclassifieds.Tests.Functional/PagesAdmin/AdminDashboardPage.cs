using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Admin
{
    [NavRoute(RelativeUrl = "Default.aspx")]
    public class AdminDashboardPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public AdminDashboardPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }


        public string GetTitle()
        {
            return _webdriver.Title;
        }

    }
}