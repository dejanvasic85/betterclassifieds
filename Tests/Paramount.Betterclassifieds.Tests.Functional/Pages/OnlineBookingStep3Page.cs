using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step3.aspx")]
    public class OnlineBookingStep3TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep3TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        private IWebElement OnlineHeaderElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_txtOnlineHeading")); }
        }

        private IWebElement OnlineDescriptionFrameElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_radEditor_contentIframe")); }
        }

        public void FillOnlineHeader(string adTitle)
        {
            OnlineHeaderElement.SendKeys(adTitle);
        }

        public void FillOnlineDescription(string description)
        {
            // Capture the frame first
            var telerikTextFrame = OnlineDescriptionFrameElement;

            // Switch to description frame
            WebDriver.SwitchTo().Frame(telerikTextFrame);

            // Now we can fill it in!
            WebDriver.FindElement(By.CssSelector("body")).SendKeys(description);

            // Switch back to the main frame
            WebDriver.SwitchTo().DefaultContent();
        }
    }
}