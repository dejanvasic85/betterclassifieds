using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    [NavRoute("Event/{0}/{1}")]
    internal class EventDetailsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EventDetailsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "btnGetTickets")]
        private IWebElement GetTicketsButton { get; set; }

        public EventDetailsPage SelectTickets(int numberOfTickets, string ticketType)
        {
            _webDriver.FindElement(By.CssSelector("[data-ticket-name='" + ticketType + "']"))
                .ToSelectElement()
                .WithSelectedOptionValue(numberOfTickets.ToString());
            return this;
        }

        public EventDetailsPage ConfirmTicketSelection()
        {
            GetTicketsButton.ClickOnElement();
            return this;
        }
    }
}
