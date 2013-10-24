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

        private void NavigateTo(BasePage page, params object[] args)
        {
            // Construct the path
            var webPath = ConfigurationManager.AppSettings.Get("BaseUrl") + string.Format(page.RelativePath, args);
            
            // Navigate using selenium driver
            _webDriver.Navigate().GoToUrl( webPath);
            
            // Initialise the UI elements
            page.InitElements();
        }
    }
}