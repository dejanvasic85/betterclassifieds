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
            var ticketSelector = By.CssSelector( "[data-ticket-name='" + ticketType + "'] td");

            var element = _webDriver.FindElements(ticketSelector)
                .Last(el => el.Displayed)
                .FindElement(By.TagName("select"));

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            element
                .ToSelectElement()
                .WithSelectedOptionValue(numberOfTickets.ToString());

            return this;
        }

        public TicketSelectionComponent PlaceOrder()
        {
            var button = _webDriver.FindElements(By.ClassName("tst-order-tickets")).First(el => el.Displayed);

            // For some reason this button click is not clicking directly on the button
            // Using a javascript method click invoke does the trick very nicely!
            _webDriver.ExecuteJavaScript("arguments[0].click()", button);

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
