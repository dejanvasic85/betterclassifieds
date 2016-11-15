using System;
using System.Linq;
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
            var cssSelector = "[data-ticket-name='" + ticketType + "'] td";

            var element = _webDriver.FindElements(By.CssSelector(cssSelector))
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
            _webDriver.FindElements(By.ClassName("tst-order-tickets")).First(el => el.Displayed).Click();
            return this;
        }

        public TicketSelectionComponent SelectGroup(string groupName)
        {
            _webDriver.FindElement(By.CssSelector("[data-group-name='" + groupName + "']")).Click();
            return this;
        }
    }
}
