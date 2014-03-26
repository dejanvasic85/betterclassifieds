using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step3.aspx")]
    [TestPage(RelativeUrl = "BundleBooking/BundlePage3.aspx")]
    public class BookingStep3TestPage : BookingBaseTestPage
    {
        public BookingStep3TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Private Elements

        private IWebElement OnlineHeaderElement
        {
            get
            {
                return FindElement("ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_txtOnlineHeading", "ContentPlaceHolder1_ucxOnlineAd_txtOnlineHeading");
            }
        }

        private IWebElement OnlineDescriptionFrameElement
        {
            get
            {
                return FindElement("ctl00_ContentPlaceHolder1_ucxDesignOnlineAd_radEditor_contentIframe", "ContentPlaceHolder1_ucxOnlineAd_radEditor_contentIframe");
            }
        }

        private IWebElement LineAdHeaderElement
        {
            get { return FindElement(By.Id("ContentPlaceHolder1_lineAdNewDesign_ctl01_txtBoldHeaderText")); }
        }

        private IWebElement LineAdDescriptionElement
        {
            get { return FindElement(By.Id("ContentPlaceHolder1_lineAdNewDesign_ctl03_txtLineAdText")); }
        }

        #endregion
        
        public void FillOnlineHeader(string adTitle)
        {
            OnlineHeaderElement.Clear();
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

        public void FillLineAdHeader(string adTitle)
        {
            LineAdHeaderElement.Clear();
            LineAdHeaderElement.SendKeys(adTitle);
        }
        
        public void FillLineAdDescription(string adTitle)
        {
            LineAdDescriptionElement.Clear();
            LineAdDescriptionElement.SendKeys(adTitle);
        }
    }
}