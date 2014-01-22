using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Ad/{0}/{1}")]
    public class OnlineAdTestPage : BaseTestPage
    {
        public OnlineAdTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        private IWebElement ContactNameElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName")); }
        }

        public string GetContactName()
        {
            return ContactNameElement.Text;
        }

    }
}
