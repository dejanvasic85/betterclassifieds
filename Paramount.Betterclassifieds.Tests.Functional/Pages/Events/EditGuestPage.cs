using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("EditAd/edit-guest/{0}?ticketNumber={1}")]
    internal class EditGuestPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EditGuestPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "btnRemoveGuest")]
        public IWebElement RemoveGuestButton { get; set; }


        public void RemoveGuest()
        {
            RemoveGuestButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnConfirmRemoveGuest")));

            element.Click();
        }
    }
}