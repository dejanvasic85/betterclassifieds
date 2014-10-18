using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class AdminPageBrowser : PageBrowser
    {
        public AdminPageBrowser(IWebDriver webDriver, IConfig config)
            : base(webDriver, config)
        { }

        public override string GetConfiguredUrl()
        {
            return _config.BaseAdminUrl;
        }
    }
}