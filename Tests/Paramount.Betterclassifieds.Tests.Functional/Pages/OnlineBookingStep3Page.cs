using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step3.aspx")]
    public class OnlineBookingStep3TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep3TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_txtOnlineHeading")]
        private readonly IWebElement _txtOnlineHeader = null;
        
        public void FillOnlineHeader(string adTitle)
        {
            _txtOnlineHeader.SendKeys(adTitle);
        }

        public void FillOnlineDescription(string description)
        {
            // Capture the frame first
            var telerikTextFrame = WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_radEditor_contentIframe"));

            // Switch to description frame
            WebDriver.SwitchTo().Frame(telerikTextFrame);

            // Now we can fill it in!
            WebDriver.FindElement(By.CssSelector("body")).SendKeys(description);

            // Switch back to the main frame
            WebDriver.SwitchTo().DefaultContent();
        }
    }
}