using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BrowserType { get; }
        string ErrorEmail { get; }
        bool SendScreenshotOneError { get; }
    }

    public class TestConfiguration : IConfig
    {
        public string BaseUrl { get { return ConfigurationManager.AppSettings.Get("BaseUrl"); } }
        public string BrowserType { get { return ConfigurationManager.AppSettings.Get("Browser"); } }
        public string ErrorEmail { get { return ConfigurationManager.AppSettings.Get("ErrorEmail").Default("support@paramountit.com.au"); } }
        
        public bool SendScreenshotOneError
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings.Get("SendScreenshotOnError").Default("true"));
            }
        }
    }
}
