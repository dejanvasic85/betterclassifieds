using System;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class SeleniumPageFactory : IPageFactory
    {
        private readonly IWebDriver _webDriver;

        public SeleniumPageFactory(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public T Resolve<T>() where T : BasePage
        {
            var page = (T)Activator.CreateInstance(typeof(T), args: _webDriver);
            page.InitElements();
            return page;
        }
    }
}