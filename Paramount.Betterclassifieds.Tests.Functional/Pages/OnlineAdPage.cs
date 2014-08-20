using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Ad/{0}/{1}")]
    public class OnlineAdTestPage : TestPage
    {
        public OnlineAdTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        private IWebElement ContactNameElement
        {
            get { return FindElement(By.Id("contactName")); }
        }

        private IWebElement ContactEmailElement
        {
            get { return FindElement(By.Id("contactEmail")); }
        }

        private IWebElement ContactPhoneElement
        {
            get { return FindElement(By.Id("contactPhone")); }
        }

        public string GetContactName()
        {
            return ContactNameElement.Text;
        }

        public string GetContactPhone()
        {
            return ContactPhoneElement.Text;
        }

        public string GetContactEmail()
        {
            return ContactEmailElement.Text;
        }
    }
}
