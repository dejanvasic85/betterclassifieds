using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    [NavRoute("editad/add-guest/{0}?eventId={1}")]
    public class AddGuestPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public AddGuestPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "guestFullName")]
        private IWebElement FullNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "guestEmail")]
        private IWebElement EmailInput { get; set; }

        [FindsBy(How = How.Id, Using = "tickets")]
        private IWebElement TicketsDropdown { get; set; }

        [FindsBy(How = How.Id, Using = "btnAddGuest")]
        private IWebElement AddGuestButton { get; set; }

        public AddGuestPage WithGuest(string fullName, string email, string ticketType)
        {
            FullNameInput.SendKeys(fullName);
            EmailInput.SendKeys(email);
            TicketsDropdown.SelectOption(ticketType);
            return this;
        }

        public AddGuestPage Add()
        {
            AddGuestButton.Click();
            WebDriverWait wait= new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("guestAddedSuccessfully")));
            return this;
        }
    }
}