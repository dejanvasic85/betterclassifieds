using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace iFlog.Tests.Functional.Pages
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
