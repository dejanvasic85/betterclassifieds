using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step1.aspx")]
    public class OnlineBookingStep1TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep1TestPage(IWebDriver webdriver, IConfig config)
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
            OnlineOnlyBookingOptionElement.Click();
        }

        public void SelectBundleBooking()
        {
            BundleOptionElement.Click();
        }

        #endregion

    }
}