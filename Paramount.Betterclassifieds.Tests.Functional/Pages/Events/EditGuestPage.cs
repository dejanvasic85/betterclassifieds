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

        [FindsBy(How = How.Id, Using = "guestFullName")]
        public IWebElement GuestFullNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "guestEmail")]
        public IWebElement GuestEmailInput { get; set; }

        [FindsBy(How = How.Id, Using = "btnUpdateGuest")]
        public IWebElement UpdateGuestButton { get; set; }

        [FindsBy(How = How.Id, Using= "btnResendGuestTicket")]
        public IWebElement ResentTicketButton { get; set; }

        public void RemoveGuest()
        {
            RemoveGuestButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnConfirmRemoveGuest")));

            element.Click();
        }

        public EditGuestPage WithName(string name)
        {
            GuestFullNameInput.FillText(name);
            return this;
        }

        public EditGuestPage WithEmail(string newGuestEmail)
        {
            GuestEmailInput.FillText(newGuestEmail);
            return this;
        }

        public EditGuestPage Update()
        {
            UpdateGuestButton.Click();
            return this;
        }

        public EditGuestPage WaitToInit()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("guestFullName")));
            return this;
        }

        public EditGuestPage WaitForToaster()
        {
            GetToastSuccessMsg();
            return this;
        }

        public string GetToastSuccessMsg()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            var el = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("toast-success")));
            return el.Text; 
        }

        public EditGuestPage ResentTicket()
        {
            ResentTicketButton.Click();
            return this;
        }
    }
}