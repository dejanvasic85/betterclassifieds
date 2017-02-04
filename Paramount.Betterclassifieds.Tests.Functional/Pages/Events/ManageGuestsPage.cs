using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("event-dashboard/{0}/event/{1}/manage-guests")]
    internal class ManageGuestsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public ManageGuestsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }


        [FindsBy(How = How.CssSelector, Using = ".guest-list [data-email]")]
        private IList<IWebElement> GuestSearchElements { get; set; }

        [FindsBy(How = How.Id, Using = "lnkAddGuest")]
        private IWebElement AddGuestButton { get; set; }

        public ManageGuestsPage EditGuest(string guestEmail)
        {
            var targetGuestElement = GuestSearchElements.Single(el => el.HasAttributeValue("data-email", guestEmail));
            targetGuestElement.Click();
            return this;
        }

        public int GetGuestsInSearchResult()
        {
            return GuestSearchElements.Count;
        }

        public ManageGuestsPage AddGuest()
        {
            AddGuestButton.Click();
            return this;
        }

    }
}