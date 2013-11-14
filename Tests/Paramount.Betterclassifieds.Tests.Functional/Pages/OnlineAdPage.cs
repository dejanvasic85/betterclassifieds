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

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxOnlineAdDetailView_lblContactName")] 
        private IWebElement ContactNameLabel;

        public override string RelativePath
        {
            get { return "Ad/{0}/{1}"; } // Ad/title/id
        }

        public string GetContactName()
        {
            return ContactNameLabel.Text;
        }
    }
}
