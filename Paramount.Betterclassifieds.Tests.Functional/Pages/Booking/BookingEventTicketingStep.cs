using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Pages.Components;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Booking
{
    [NavRoute(RelativeUrl = "Booking/EventTickets")]
    internal class BookingEventTicketingStep : BookingTestPage
    {
        private int _numberOfTickets = 0;
        private int _numberOfFields = 0;

        public BookingEventTicketingStep(IWebDriver webdriver) : base(webdriver)
        {
        }

        [FindsBy(How = How.Id, Using = "addTicketType"), UsedImplicitly]
        private IWebElement AddTicketButton;

        [FindsBy(How = How.Id, Using = "addDynamicField"), UsedImplicitly]
        private IWebElement AddDynamicFieldButton;

        public BookingEventTicketingStep AddTicketType(string ticketName, decimal price, int quantity)
        {
            _webdriver.JsClick(AddTicketButton);

            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));
            wait.Until(drv => drv.FindElements(By.ClassName("ticket-form")).Count > _numberOfTickets);

            // Get the last element that was just added
            var ticketForms = _webdriver.FindElements(By.ClassName("ticket-form"));
            var lastForm = ticketForms.Last();

            var ticketDefinition = new TicketDefinition(lastForm);
            ticketDefinition.SetTicketData(ticketName, price, quantity);

            _numberOfTickets++;

            return this;
        }

        public BookingEventTicketingStep AddDynamicField(string fieldName, bool isRequired)
        {
            AddDynamicFieldButton.Click();
            
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));
            wait.Until(drv => drv.FindElements(By.ClassName("ticket-field-edit")).Count > _numberOfFields);

            var fieldForms = _webdriver.FindElements(By.ClassName("ticket-field-edit"));
            var lastForm = fieldForms.Last();

            var fieldDefinition = new FieldDefinition(lastForm);
            fieldDefinition.SetField(fieldName, isRequired);

            _numberOfFields++;
            return this;
        }
    }


}