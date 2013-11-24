using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class TestRouter
    {
        private readonly IWebDriver _webDriver;
        private readonly IConfig _config;

        public TestRouter(IWebDriver webDriver, IConfig config)
        {
            _webDriver = webDriver;
            _config = config;
        }
        
        public void NavigateTo(BasePage page, params object[] args)
        {
            // Construct the path
            var webPath = GetBaseUrl() + string.Format(page.RelativePath, args);
            
            // Navigate using selenium driver
            _webDriver.Navigate().GoToUrl( webPath);
            
            // Initialise the UI elements
            page.InitElements();
        }

        public void NavigateTo(string relativePath)
        {
            // Construct the path with base url
            var webPath = GetBaseUrl() + relativePath;
            
            // Go!
            _webDriver.Navigate().GoToUrl(webPath);
        }

        private string GetBaseUrl()
        {
            return _config.BaseUrl;
        }
    }
}