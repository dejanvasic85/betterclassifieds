using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step1.aspx")]
    public class BookingStep1TestPage : BookingBaseTestPage
    {
        public BookingStep1TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements

        private IWebElement BundleOptionElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_rdoPremium")); }
        }

        private IWebElement OnlineOnlyBookingOptionElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_rdoOnlineOnly")); }
        }

        #endregion

        #region Public Methods

        public void SelectOnlineAdBooking()
        {
            OnlineOnlyBookingOptionElement.ClickOnElement();
        }

        public void SelectBundleBooking()
        {
            BundleOptionElement.ClickOnElement();
        }

        #endregion

    }
}