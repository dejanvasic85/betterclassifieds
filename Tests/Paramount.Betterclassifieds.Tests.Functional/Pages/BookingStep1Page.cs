using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPageUrl(RelativeUrl = "Booking/Step1.aspx")]
    public class OnlineBookingStep1Page : OnlineBookingBasePage
    {
        public OnlineBookingStep1Page(IWebDriver webdriver) : base(webdriver)
        { }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_rdoPremium")]
        private readonly IWebElement _bundledOptionElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_rdoOnlineOnly")]
        private readonly IWebElement _onlineOnlyBookingOptionElement = null;

        public void SelectOnlineAdBooking()
        {
            _onlineOnlyBookingOptionElement.Click();
        }

        public void SelectBundleBooking()
        {
            _bundledOptionElement.Click();
        }

    }
}