using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("Invite/{0}")]
    internal class EventInvitePage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        [FindsBy(How = How.CssSelector, Using = "[data-ticketName]")]
        public IList<IWebElement> TicketActionButtons { get; set; }

        [FindsBy(How = How.TagName, Using = "h1")]
        public IWebElement EventNameHeading { get; set; }

        public EventInvitePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public EventInvitePage SelectTicket(string ticketType, int numberOfTickets = 1)
        {
            new TicketSelectionComponent(_webDriver).SelectTickets(numberOfTickets, ticketType).PlaceOrder();

            return this;
        }

        public string GetEventName()
        {
            return EventNameHeading.Text;
        }
    }
}