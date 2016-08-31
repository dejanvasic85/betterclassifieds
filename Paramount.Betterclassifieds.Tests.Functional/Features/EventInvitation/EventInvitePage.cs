using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.EventInvitation
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

        public EventInvitePage SelectTicket(string ticketName)
        {
            var ticketCallToAction = TicketActionButtons.FirstOrDefault(el => el.GetAttribute("data-ticketName").Equals(ticketName));
            if (ticketCallToAction == null)
                throw new NoSuchElementException("Cannot find the button for the ticket " + ticketName);

            ticketCallToAction.Click();
            return this;
        }

        public string GetEventName()
        {
            return EventNameHeading.Text;
        }
    }
}