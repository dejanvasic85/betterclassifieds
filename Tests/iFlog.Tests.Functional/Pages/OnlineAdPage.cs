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
            get { return ""; } // Ad/title/id
        }
    }
}
