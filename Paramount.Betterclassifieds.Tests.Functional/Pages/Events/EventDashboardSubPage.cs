using System.Linq;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    internal class EventDashboardSubPage
    {
        private readonly IWebDriver _webDriver;

        public EventDashboardSubPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ClickBackToDashboard()
        {
            _webDriver.FindElements(By.ClassName("btn")).SingleOrDefault(el => el.Text.Contains("Event Dashboard"))?.Click();
        }
    }
}