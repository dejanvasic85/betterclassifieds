using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class BookingTestPage : TestPage
    {
        protected BookingTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }
        
        protected IWebElement NextButtonElement
        {
            get
            {
                return FindElement("btnSubmit");
            }
        }

        public void Proceed()
        {
            NextButtonElement.Click();
            WaitForAjax(initElements: false);
        }
    }
}