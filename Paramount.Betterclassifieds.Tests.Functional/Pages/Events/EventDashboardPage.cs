using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("event-dashboard/{0}")]
    internal class EventDashboardPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        struct Locator
        {
            public static By PageHeader = By.ClassName("page-header");
        }

        public EventDashboardPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        
        [FindsBy(How = How.Id, Using = "lnkManageGroups")]
        private IWebElement ManageGroupsButton { get; set; }

        [FindsBy(How = How.Id, Using = "lnkManageTickets")]
        private IWebElement ManageTicketsButton { get; set; }


        [FindsBy(How = How.Id, Using = "lnkManageGuests")]
        private IWebElement ManageGuestsButton { get; set; }

        [FindsBy(How = How.Id, Using = "totalSoldQty")]
        public IWebElement TotalSoldQtyStat { get; set; }


        public EventDashboardPage ManageGroups()
        {
            ManageGroupsButton.Click();
            return this;
        }

        public EventDashboardPage ManageTickets()
        {
            ManageTicketsButton.Click();
            return this;
        }

        public EventDashboardPage ManageGuests()
        {
            ManageGuestsButton.Click();
            return this;
        }


        public int GetTotalSoldQty()
        {
            return int.Parse(TotalSoldQtyStat.Text);
        }


        public string GetEventTitle()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            var pageHeading = wait.Until(ExpectedConditions.ElementIsVisible(Locator.PageHeader));

            return pageHeading.Text.Replace(" - Event Dashboard", string.Empty);
        }
    }
}
