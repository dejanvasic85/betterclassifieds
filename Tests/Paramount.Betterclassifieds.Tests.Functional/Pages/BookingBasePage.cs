using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class BookingBasePage : BasePage
    {
        protected BookingBasePage(IWebDriver webdriver) : base(webdriver)
        {
        }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxNavButton_btnNext")]
        private readonly IWebElement _nextButtonElement = null;

        public void Proceed()
        {
            _nextButtonElement.Click();
        }
    }
}