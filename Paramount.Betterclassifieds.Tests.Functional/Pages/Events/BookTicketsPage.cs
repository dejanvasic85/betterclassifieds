using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("Event/BookTickets")]
    internal class BookTicketsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public BookTicketsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "emailGuestsToggle")]
        public IWebElement EmailGuestsButton { get; set; }

        [FindsBy(How = How.Id, Using = "proceedToPaymentBtn")]
        public IWebElement CheckoutButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "event-ticket")]
        public IList<IWebElement> TicketInputs { get; set; }

        [FindsBy(How = How.Id, Using = "phone")]
        public IWebElement PhoneNumberElement { get; set; }

        [FindsBy(How = How.ClassName, Using = "tst-totalTicketCost")]
        public IWebElement TotalTicketCostElement { get; set; }

        [FindsBy(How = How.ClassName, Using = "tst-totalFees")]
        public IWebElement TotalFeesElement { get; set; }

        [FindsBy(How = How.ClassName, Using = "tst-subTotal")]
        public IWebElement SubTotalElement { get; set; }


        struct Locator
        {
            public static By EventTicketInputs => By.ClassName("event-ticket");
            public static By ProceedToPaymentBtn => By.Id("proceedToPaymentBtn");
        }

        public BookTicketsPage Checkout()
        {
            _webDriver.JsClick(Locator.ProceedToPaymentBtn);
            return this;
        }

        public BookTicketsPage WithSecondGuest(string email, string fullName)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(Locator.EventTicketInputs));

            if (TicketInputs.Count < 2)
                throw new IndexOutOfRangeException("To enter the second guest, there has to have been at least 2 tickets selected");

            var textInputs = TicketInputs[1].FindElements(By.TagName("input")).ToList();
            textInputs[0].SendKeys(fullName);
            textInputs[1].SendKeys(email);

            return this;
        }

        public BookTicketsPage WithPhone(string phone)
        {
            PhoneNumberElement.SendKeys(phone);
            return this;
        }

        public decimal GetTotalTicketsCost()
        {
            return GetPriceFromElement(TotalTicketCostElement);
        }

        public decimal GetTotalFees()
        {
            return GetPriceFromElement(TotalFeesElement);
        }

        public decimal GetSubTotal()
        {
            return GetPriceFromElement(SubTotalElement);
        }

        private decimal GetPriceFromElement(IWebElement element)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(d => element.Displayed);

            return decimal.Parse(element.Text.Trim().Replace("$", string.Empty));
        }
    }
}