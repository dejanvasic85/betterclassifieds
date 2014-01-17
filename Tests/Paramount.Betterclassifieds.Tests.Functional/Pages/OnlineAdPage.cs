using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Ad/{0}/{1}")]
    public class OnlineAdPage : BasePage
    {
        public OnlineAdPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        public string GetContactName()
        {
            return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName")).Text;
        }
    }
}
