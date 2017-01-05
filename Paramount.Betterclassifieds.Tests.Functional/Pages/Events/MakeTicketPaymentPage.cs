using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("Event/MakePayment")]
    internal class MakeTicketPaymentPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public MakeTicketPaymentPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        
        public MakeTicketPaymentPage PayWithPayPal()
        {
            _webDriver.JsClick(By.Id("payWithPayPal"));
            return this;
        }

        public MakeTicketPaymentPage WaitForPayPal()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.TitleContains("PayPal Checkout - Log in"));
            _webDriver.SwitchTo().Frame("injectedUl");
            return this;
        }
    }
}