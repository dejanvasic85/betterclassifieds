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
    [NavRoute(RelativeUrl = "Booking/Step/2/Event")]
    internal class BookingEventStep : BookingTestPage
    {
        public BookingEventStep(IWebDriver webdriver) : base(webdriver)
        {
        }

        [FindsBy(How = How.Id, Using = "Title"), UsedImplicitly]
        private IWebElement Title;

        [FindsBy(How = How.Id, Using = "Description"), UsedImplicitly]
        private IWebElement Description;

        [FindsBy(How = How.Id, Using = "Location"), UsedImplicitly]
        private IWebElement LocationInput;

        [FindsBy(How = How.Id, Using = "OrganiserName"), UsedImplicitly]
        private IWebElement OrganiserNameInput;

        [FindsBy(How = How.Id, Using = "OrganiserPhone"), UsedImplicitly]
        private IWebElement OrganiserPhoneInput;

        [FindsBy(How = How.Id, Using = "AdStartDate"), UsedImplicitly]
        private IWebElement AdStartDateInput;

        [FindsBy(How = How.Id, Using = "TicketingToggle"), UsedImplicitly]
        private IWebElement TicketingToggleBtn;

        public BookingEventStep WithAdDetails(string title, string description)
        {
            Title.FillText(title);
            Description.FillText(description);
            return this;
        }

        public BookingEventStep WithLocation(string location)
        {
            // We need to scroll the location input to the middle of the screen
            // So that google map options will be visible and selectable on the screen
            _webdriver.ScrollElementToMiddle(LocationInput);
            LocationInput.FillText(location);

            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("pac-container")));

            var googleAddressResults = _webdriver.FindElements(By.ClassName("pac-item"));
            if (googleAddressResults.Count == 0)
            {
                throw new NoSuchElementException("Geolocation cannot find the address " + location);
            }
            googleAddressResults.First().ClickOnElement();
            return this;
        }

        public BookingEventStep WithOrganiser(string name, string contactNumber)
        {
            OrganiserNameInput.FillText(name);
            OrganiserPhoneInput.FillText(contactNumber);
            return this;
        }

        public BookingEventStep WithAdStartDateToday()
        {
            AdStartDateInput.ClickOnElement();
            var waitForDay = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));
            var currentDayElement = waitForDay.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".datepicker-days td.today")));
            currentDayElement.ClickOnElement();

            var waitForText = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));
            waitForText.Until(drv =>
            {
                var findElement = drv.FindElement(By.Id("AdStartDate"));
                if (findElement == null)
                {
                    return false;
                }
                return findElement.GetValue().HasValue();
            });

            return this;
        }

        public BookingEventStep WithTicketingEnabled(bool enabled)
        {
            var toggleButton = new ToggleButton(this.TicketingToggleBtn);
            toggleButton.Toggle(enabled);
            return this;
        }
    }
}