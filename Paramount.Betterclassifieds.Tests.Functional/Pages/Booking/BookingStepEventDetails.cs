using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Step/2/Event")]
    public class BookingStepEventDetails : BookingTestPage
    {
        public BookingStepEventDetails(IWebDriver webdriver) : base(webdriver)
        {
        }

        [FindsBy(How = How.Id, Using = "Title")]
        private IWebElement Title;

        [FindsBy(How = How.Id, Using = "Description")]
        private IWebElement Description;

        [FindsBy(How = How.ClassName, Using = "fileinput-button")]
        private IWebElement UploadButton;

        public BookingStepEventDetails WithAdDetails(string title, string description)
        {
            this.Title.FillText(title);
            this.Description.FillText(description);
            this.UploadButton.ClickOnElement();
            return this;
        }
    }
}