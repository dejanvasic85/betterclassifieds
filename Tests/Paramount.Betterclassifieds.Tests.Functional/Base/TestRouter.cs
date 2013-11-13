using System;
using System.Configuration;
using OpenQA.Selenium;

namespace iFlog.Tests.Functional
{
    public class TestRouter
    {
        private readonly IWebDriver webDriver;

        public TestRouter(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void NavigateTo(BasePage page, params object[] args)
        {
            // Construct the path
            var webPath = GetBaseUrl() + string.Format(page.RelativePath, args);
            
            // Navigate using selenium driver
            webDriver.Navigate().GoToUrl( webPath);
            
            // Initialise the UI elements
            page.InitElements();
        }

        public void NavigateTo(string relativePath)
        {
            // Construct the path with base url
            var webPath = GetBaseUrl() + relativePath;
            
            // Go!
            webDriver.Navigate().GoToUrl(webPath);
        }

        private string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings.Get("BaseUrl");
        }

    }
}