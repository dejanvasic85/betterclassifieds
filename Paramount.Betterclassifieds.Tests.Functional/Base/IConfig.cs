using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BrowserType { get; }
        string BaseAdminUrl { get; }
    }

    public class TestConfiguration : IConfig
    {
        public string BaseUrl
        {
            get
            {
                return GetSafeUrl("BaseUrl");
            }
        }

        public string BrowserType
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Browser");
            }
        }

        public string BaseAdminUrl
        {
            get
            {
                return GetSafeUrl("BaseAdminUrl");
            }
        }

        private string GetSafeUrl(string urlSettingName)
        {
            var url = ConfigurationManager.AppSettings.Get(urlSettingName);
            if (url.IsNullOrEmpty())
                throw new ConfigurationErrorsException(string.Format("Missing [{0}] configuration from app.config", urlSettingName));

            return !url.EndsWith("/") ? url.Append("/") : url;
        }
    }

    public class EnvironmentConfiguration : IConfig
    {
        public string BaseUrl
        {
            get { return System.Environment.GetEnvironmentVariable("BaseUrl"); }
        }

        public string BrowserType
        {
            get { return System.Environment.GetEnvironmentVariable("BrowserType"); }
        }

        public string BaseAdminUrl
        {
            get { return System.Environment.GetEnvironmentVariable("BaseAdminUrl"); }
        }
    }

    internal class ConfigFactory
    {
        public IConfig CreateConfig()
        {
            if (System.Environment.GetEnvironmentVariable("TEAMCITY_JRE").HasValue())
            {
                return new EnvironmentConfiguration();
            }

            return new TestConfiguration();
        }
    }

}
