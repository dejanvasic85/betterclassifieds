using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Error/NotFound")]
    public class NotFoundTestPage : TestPage
    {
        public NotFoundTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        [FindsBy(How = How.Id, Using = "notFoundHeading"), UsedImplicitly]
        private IWebElement HeadingElement;

        [FindsBy(How = How.Id, Using = "notFoundDescription"), UsedImplicitly]
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
