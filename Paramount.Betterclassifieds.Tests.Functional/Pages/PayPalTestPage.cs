using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    internal class PayPalTestPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public PayPalTestPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement PasswordElement { get; set; }

        [FindsBy(How = How.Id, Using = "btnLogin")]
        public IWebElement LoginButton { get; set; }

        public PayPalTestPage WithEmailAddress(string email)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            element.SendKeys(email);
            return this;
        }

        public PayPalTestPage WithPassword(string password)
        {
            PasswordElement.SendKeys(password);
            return this;
        }

        public PayPalTestPage Login()
        {
            LoginButton.ClickOnElement();
            return this;
        }

        public PayPalTestPage WaitForLoaderToFinish()
        {
            Thread.Sleep(500);
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("loader")));
            return this;
        }

        public PayPalTestPage Continue()
        {
            _webDriver.SwitchTo().ParentFrame();
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            var button = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("confirmButtonTop")));
            button.ClickOnElement();
            return this;
        }
    }
}
