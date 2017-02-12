using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("event-dashboard/{0}/event/{1}")]
    internal class ManageTicketsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public ManageTicketsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public int FieldCount { get; private set; } = 0;

        static class Locators
        {
            public const string SaveTicketButton = "btnSaveTicket";
            public const string CreateNewTicketButton = "btnCreateTicket";
            public const string AddFieldButton = "btnAddDynamicField";
            public const string CreateNewForm = "createTicketForm";
            public const string FieldNameInput = "[data-bind='value: fieldName']";
            public const string TicketNameInput = "txtTicketName";
            public const string PriceInput = "txtTicketPrice";
            public const string AvailableQuantity = "txtAvailableQuantity";
        }

        [FindsBy(How = How.Id, Using = Locators.CreateNewTicketButton)]
        public IWebElement CreateNewTicketButton { get; set; }

        [FindsBy(How = How.Id, Using = Locators.SaveTicketButton)]
        public IWebElement SaveTicketButton { get; set; }

        [FindsBy(How = How.Id, Using = Locators.AddFieldButton)]
        public IWebElement AddFieldButton { get; set; }

        [FindsBy(How = How.Id, Using = Locators.TicketNameInput)]
        public IWebElement TicketNameInput { get; set; }

        [FindsBy(How = How.Id, Using = Locators.PriceInput)]
        public IWebElement PriceInput { get; set; }

        [FindsBy(How = How.Id, Using = Locators.AvailableQuantity)]
        public IWebElement AvailableQuantityInput { get; set; }

        public ManageTicketsPage WithNewTicket()
        {
            CreateNewTicketButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Locators.CreateNewForm)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(Locators.SaveTicketButton)));
            return this;
        }

        public ManageTicketsPage WithTicketName(string ticketName)
        {
            TicketNameInput.FillText(ticketName);
            return this;
        }

        public ManageTicketsPage WithPrice(decimal price)
        {
            PriceInput.FillText(price.ToString());
            return this;
        }

        public ManageTicketsPage WithAvailableQuantity(int qty)
        {
            AvailableQuantityInput.FillText(qty.ToString());
            return this;
        }

        public void Save()
        {
            _webDriver.JsClick(SaveTicketButton);
            Thread.Sleep(800); // Wait a little until the db record is persisted
        }

        public ManageTicketsPage WithField(string fieldName, bool isRequired)
        {
            AddFieldButton.Click();
            FieldCount = FieldCount + 1;

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElements(By.CssSelector(Locators.FieldNameInput)).Count == FieldCount);
            
            var fieldNameInput = _webDriver.FindElements(By.CssSelector(Locators.FieldNameInput)).ElementAt(FieldCount - 1);
            fieldNameInput.FillText(fieldName);

            return this;
        }

        public ManageTicketsPage EditTicket(string ticketName, out int ticketId)
        {
            var editBtn = _webDriver.FindElements(By.CssSelector($"[data-ticketName='{ticketName}'] .btn"))
                .Single(btn => btn.Text.Equals("edit", StringComparison.OrdinalIgnoreCase));

            var ticketIdAttr = editBtn.GetAttribute("data-ticketId");
            ticketId = int.Parse(ticketIdAttr);

            editBtn.Click();
            return this;
        }
    }
}