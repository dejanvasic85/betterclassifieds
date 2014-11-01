using OpenQA.Selenium;
using System.Linq;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public abstract class TestPage
    {
        protected readonly IWebDriver WebDriver;
        protected readonly IConfig _config;

        protected TestPage(IWebDriver webdriver, IConfig config)
        {
            this.WebDriver = webdriver;
            _config = config;
        }

        public virtual string GetTitle()
        {
            return WebDriver.Title;
        }

        public virtual void InitElements()
        {
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(WebDriver, this);
            WebDriver.Manage().Window.Size = new System.Drawing.Size(1200, 768);
        }

        protected bool IsElementPresentBy(By by)
        {
            return WebDriver.FindElements(by).Any();
        }

        public IWebElement FindElement(params string[] identifiers)
        {
            foreach (var id in identifiers)
            {
                var element = WebDriver.FindElements(By.Id(id)).FirstOrDefault();

                if (element != null)
                    return element;
            }
            throw new NoSuchElementException("Unable to locate any of the identifiers in the web page.");
        }

        protected void WaitForAjax(bool initElements = true)
        {
            WebDriver.WaitForJqueryAjax(10);

            if (initElements)
                InitElements();
        }

    }
}
