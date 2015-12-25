using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Step/2/Event")]
    public class BookingStepEventDetails : BookingTestPage
    {
        public BookingStepEventDetails(IWebDriver webdriver) : base(webdriver)
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

        public BookingStepEventDetails WithAdDetails(string title, string description)
        {
            Title.FillText(title);
            Description.FillText(description);
            return this;
        }

        public BookingStepEventDetails WithLocation(string location)
        {
            LocationInput.FillText(location);
            return this;
        }

        public BookingStepEventDetails WithOrganiser(string name, string contactNumber)
        {
            OrganiserNameInput.FillText(name);
            OrganiserPhoneInput.FillText(contactNumber);
            return this;
        }

        public BookingStepEventDetails WithAdStartDateToday()
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

        public BookingStepEventDetails WithTicketingEnabled(bool enabled)
        {
            var toggleButton = new ToggleButton(this.TicketingToggleBtn);
            if (enabled)
            {
                toggleButton.TurnOn();
            }
            else
            {
                toggleButton.TurnOff();
            }
            return this;
        }
    }
}