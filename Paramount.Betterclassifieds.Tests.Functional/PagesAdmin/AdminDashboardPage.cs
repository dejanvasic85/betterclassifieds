using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Admin
{
    [TestPage(RelativeUrl = "Default.aspx")]
    public class AdminDashboardPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public AdminDashboardPage(IWebDriver webdriver, IConfig config)
        {
            _webdriver = webdriver;
        }


        public string GetTitle()
        {
            return _webdriver.Title;
        }

    }
}