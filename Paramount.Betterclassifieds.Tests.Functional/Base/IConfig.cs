using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BrowserType { get; }
    }

    public class TestConfiguration : IConfig
    {
        public string BaseUrl { get { return ConfigurationManager.AppSettings.Get("BaseUrl"); } }
        public string BrowserType { get { return ConfigurationManager.AppSettings.Get("Browser"); } }
    }
}
