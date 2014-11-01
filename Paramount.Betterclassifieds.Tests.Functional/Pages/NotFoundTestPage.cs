using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Error/NotFound")]
    public class NotFoundTestPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public NotFoundTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
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
