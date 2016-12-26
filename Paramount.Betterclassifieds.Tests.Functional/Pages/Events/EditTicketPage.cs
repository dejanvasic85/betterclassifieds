using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("event-dashboard/{0}/event-ticket/{1}")]
    internal class EditTicketPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EditTicketPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "txtTicketName")]
        public IWebElement TicketNameElement { get; set; }

        [FindsBy(How = How.Id, Using = "txtTicketPrice")]
        public IWebElement PriceElement { get; set; }

        [FindsBy(How = How.Id, Using = "txtAvailableQuantity")]
        public IWebElement AvailableQuantityElement { get; set; }

        [FindsBy(How = How.Id, Using = "txtRemainingQuantity")]
        public IWebElement RemainingQuantityElement { get; set; }

        [FindsBy(How = How.Id, Using = "btnSaveTicket")]
        public IWebElement SaveButton { get; set; }
        
        public EditTicketPage WithTicketName(string name)
        {
            TicketNameElement.FillText(name);
            return this;
        }

        public EditTicketPage WithPrice(int price)
        {
            PriceElement.FillText(price.ToString());
            return this;
        }

        public EditTicketPage WithQuantity(int quantity)
        {
            RemainingQuantityElement.FillText(quantity.ToString());
            return this;
        }

        public EditTicketPage Save()
        {
            SaveButton.ClickOnElement();
            return this;
        }
    }
}