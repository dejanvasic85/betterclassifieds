using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    [NavRoute("EditAd/EventDashboard/{0}")]
    internal class EventDashboardPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EventDashboardPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "lnkAddGuest")]
        private IWebElement AddGuestButton { get; set; }
        
        [FindsBy(How = How.Id, Using = "lnkManageGroups")]
        private IWebElement ManageGroupsButton { get; set; }

        public EventDashboardPage AddGuest()
        {
            AddGuestButton.Click();
            return this;
        }

        public EventDashboardPage ManageGroups()
        {
            ManageGroupsButton.Click();
            return this;
        }
    }
}
