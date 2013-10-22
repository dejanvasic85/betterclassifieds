using OpenQA.Selenium;

namespace iFlog.Tests.Functional
{
    public abstract class BaseStep
    {
        public readonly IWebDriver WebDriver;
        public readonly Router Router;

        protected BaseStep(IWebDriver webDriver)
        {
            this.WebDriver = webDriver;
            this.Router = new Router(webDriver);
        }

    }
}