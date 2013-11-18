using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public class OnlineAdPage : BasePage
    {
        public OnlineAdPage(IWebDriver webdriver)
            : base(webdriver)
        {

        }
        
        public override string RelativePath
        {
            get { return "Ad/{0}/{1}"; } // Ad/title/id
        }

        public string GetContactName()
        {
            return WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName")).Text;
        }
    }
}
