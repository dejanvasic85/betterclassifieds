using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPageUrl(RelativeUrl = "Ad/{0}/{1}")]
    public class OnlineAdPage : BasePage
    {
        public OnlineAdPage(IWebDriver webdriver)
            : base(webdriver)
        { }

        public string GetContactName()
        {
            return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName")).Text;
        }
    }
}
