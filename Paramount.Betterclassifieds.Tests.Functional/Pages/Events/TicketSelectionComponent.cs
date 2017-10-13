using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
            var ticketSelector = By.CssSelector( "[data-ticket-option='" + ticketType + "']");
            var displayWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
            var selectElement = displayWait.Until(driver =>
            {
                var select = driver.FindElements(ticketSelector)
                    .FirstOrDefault(item => item.Displayed);

                if (select != null)
                {
                    return select;
                }

                return null;
            });
            
            selectElement
                .ToSelectElement()
                .WithSelectedOptionValue(numberOfTickets.ToString());

            return this;
        }

        public TicketSelectionComponent PlaceOrder()
        {
            var button = _webDriver.FindElements(By.ClassName("tst-order-tickets")).First(el => el.Displayed);
            _webDriver.JsClick(button);
            return this;
        }

        public TicketSelectionComponent SelectGroup(string groupName)
        {
            _webDriver.FindElement(By.CssSelector("[data-group-name='" + groupName + "']")).Click();
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("ticketSelectionModal")));

            return this;
        }
    }
}
