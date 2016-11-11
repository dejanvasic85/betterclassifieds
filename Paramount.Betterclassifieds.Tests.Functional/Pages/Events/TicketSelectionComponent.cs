using System.Linq;
using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    internal class TicketSelectionComponent
    {
        private readonly IWebDriver _webDriver;

        public TicketSelectionComponent(IWebDriver webDriver)
        {   
            _webDriver = webDriver;
        }

        public TicketSelectionComponent SelectTickets(int numberOfTickets, string ticketType)
        {
            _webDriver.FindElements(By.CssSelector("[data-ticket-name='" + ticketType + "'] td"))
                .Last(el => el.Displayed)
                .FindElement(By.TagName("select"))
                .ToSelectElement()
                .WithSelectedOptionValue(numberOfTickets.ToString());
            return this;
        }


        public TicketSelectionComponent PlaceOrder()
        {
            _webDriver.FindElements(By.ClassName("tst-order-tickets")).First(el => el.Displayed).Click();
            return this;
        }
    }
}
