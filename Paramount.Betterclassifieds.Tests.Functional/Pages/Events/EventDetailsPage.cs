using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
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

            _webDriver.FindElements(By.CssSelector("[data-ticket-name='" + ticketType + "'] td"))
                .Last(el => el.Displayed)
                .FindElement(By.TagName("select"))
                .ToSelectElement()
                .WithSelectedOptionValue(numberOfTickets.ToString());

            return this;
        }

        public string GetPriceForTicket(string ticketType)
        {
            return _webDriver.FindElement(By.CssSelector("[data-ticket-name='" + ticketType + "']"))
                .FindElement(By.ClassName("tst-ticket-price"))
                .Text;
        }

        public EventDetailsPage ConfirmTicketSelection()
        {
            _webDriver.FindElements(By.ClassName("tst-order-tickets")).First(el => el.Displayed).Click();
            return this;
        }

        public bool IsPriceFreeForTicket(string ticketName)
        {
            return _webDriver.FindElement(By.CssSelector("[data-ticket-name='" + ticketName + "']"))
                .FindElement(By.ClassName("tst-ticket-free"))
                .Text
                .EqualTo("free");
        }
    }
}
