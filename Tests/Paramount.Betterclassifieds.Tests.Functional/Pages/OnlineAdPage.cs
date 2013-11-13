using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public class OnlineAdPage : BasePage
    {
        public OnlineAdPage(IWebDriver webdriver) : base(webdriver)
        {
            
        }

        public override string RelativePath
        {
            get { return "Ad/{0}/{1}"; } // Ad/title/id
        }

        public string GetContactName()
        {
            var element = this.WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName"));
            return element.Text;
        }
    }
}
