using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class OnlineBookingBaseTestPage : BaseTestPage
    {
        protected OnlineBookingBaseTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        protected IWebElement NextButtonElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxNavButtons_btnNext")); }
        }

        public void Proceed()
        {
            NextButtonElement.Click();
        }
    }
}