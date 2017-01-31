using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("event-dashboard/{0}")]
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

        [FindsBy(How = How.Id, Using = "lnkManageTickets")]
        private IWebElement ManageTicketsButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".guest-list [data-email]")]
        private IList<IWebElement> GuestSearchElements { get; set; }

        [FindsBy(How = How.Id, Using = "totalSoldQty")]
        public IWebElement TotalSoldQtyStat { get; set; }

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

        public EventDashboardPage ManageTickets()
        {
            ManageTicketsButton.Click();
            return this;
        }

        public EventDashboardPage EditGuest(string guestEmail)
        {
            var targetGuestElement = GuestSearchElements.Single(el => el.HasAttributeValue("data-email", guestEmail));
            var button = targetGuestElement.FindElements(By.ClassName("btn")).Single(el => el.Text.Contains("Edit"));
            button.Click();
            return this;
        }

        public int GetTotalSoldQty()
        {
            return int.Parse(TotalSoldQtyStat.Text);
        }

        public int GetGuestsInSearchResult()
        {
            return GuestSearchElements.Count;
        }
    }
}
