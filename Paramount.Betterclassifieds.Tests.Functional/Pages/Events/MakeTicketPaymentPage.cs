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

        private struct Locator
        {
            public static By PayWithCardButton => By.ClassName("stripe-button-el");
            public static By StripeEmail => By.CssSelector("input[type=email]");
            public static By StripeCardNumber => By.CssSelector("[autocomplete=cc-number]");
            public static By StripeMonthExpiry => By.CssSelector("[autocomplete=cc-exp]");
            public static By StripeCVC => By.CssSelector("[placeholder=CVC]");
            public static By StripeSubmit => By.CssSelector("button[type=submit]");
            public static By StripeEnterDetailsManuallyLink => By.ClassName("CodeNotReceived-actionMessage");
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

        public MakeTicketPaymentPage PayWithStripe()
        {
            _webDriver.JsClick(Locator.PayWithCardButton);
            _webDriver.SwitchTo().Frame("stripe_checkout_app");

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(Locator.StripeEmail));

            _webDriver.FindElement(Locator.StripeEmail).FillText("hello@world.com");
            _webDriver.JsClick(Locator.StripeEnterDetailsManuallyLink);
            _webDriver.FindElement(Locator.StripeCardNumber).FillText("4242424242424242");
            _webDriver.FindElement(Locator.StripeMonthExpiry).FillText("1020");
            _webDriver.FindElement(Locator.StripeCVC).FillText("111");
            _webDriver.FindElement(Locator.StripeSubmit).Click();

            // Done. Switch back
            _webDriver.SwitchTo().DefaultContent();

            return this;
        }
    }
}