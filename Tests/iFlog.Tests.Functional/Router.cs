using System;
using System.Configuration;
using OpenQA.Selenium;

namespace iFlog.Tests.Functional
{
    public class Router
    {
        private readonly IWebDriver _webDriver;

        public Router(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void NavigateTo(BasePage page, params object[] args)
        {
            _webDriver.Navigate().GoToUrl( string.Format(page.RelativePath, args));
            _webDriver.Navigate().GoToUrl(new Uri(ConfigurationManager.AppSettings.Get("BaseUrl")));
            page.InitElements();
        }
    }
}