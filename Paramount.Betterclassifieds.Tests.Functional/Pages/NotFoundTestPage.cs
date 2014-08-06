using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Error/NotFound")]
    public class NotFoundTestPage : BaseTestPage
    {
        public NotFoundTestPage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        {
        }

        [FindsBy(How = How.Id, Using = "notFoundHeading")] 
        private IWebElement HeadingElement;

        [FindsBy(How = How.Id, Using = "notFoundDescription")]
        private IWebElement ParagraphElement;

        public string GetHeadingText()
        {
            return HeadingElement.Text;
        }

        public string GetDescriptionText()
        {
            return ParagraphElement.Text;
        }
    }
}
