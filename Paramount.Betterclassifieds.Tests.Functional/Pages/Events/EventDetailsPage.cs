using System.Linq;
using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("Event/{0}/{1}")]
    internal class EventDetailsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EventDetailsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public EventDetailsPage SelectTickets(int numberOfTickets, string ticketType)
        {
            new TicketSelectionComponent(_webDriver).SelectTickets(numberOfTickets, ticketType);
            return this;
        }

        public string GetPriceForTicket(string ticketType)
        {
            var nameSelector = By.CssSelector("[data-ticket-name='" + ticketType + "']");
            var priceSelector = By.ClassName("tst-ticket-price");

            return GetVisibleTicket(ticketType)?
                .FindElement(priceSelector)
                .Text;
        }

        public EventDetailsPage ConfirmTicketSelection()
        {
            new TicketSelectionComponent(_webDriver).PlaceOrder();
            return this;
        }

        public bool IsPriceFreeForTicket(string ticketName)
        {
            return GetVisibleTicket(ticketName)
                .FindElement(By.ClassName("tst-ticket-free"))
                .Text.ToLower()
                .EqualTo("free");
        }

        private IWebElement GetVisibleTicket(string ticketName)
        {
            var nameSelector = By.CssSelector("[data-ticket-name='" + ticketName + "']");
            return _webDriver.FindElements(nameSelector)
                .FirstOrDefault(el => el.Displayed);
        }

        public EventDetailsPage SelectGroup(string groupName)
        {
            new TicketSelectionComponent(_webDriver).SelectGroup(groupName);

            return this;
        }
    }
}
