using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class BookingTestPage : ITestPage
    {
        protected readonly IWebDriver _webdriver;

        protected BookingTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        public virtual void Proceed()
        {
            _webdriver.FindElement(By.Id("btnSubmit")).ClickOnElement();
        }
    }
}