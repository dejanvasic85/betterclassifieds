using System;
using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BaseAdminUrl { get; }
        string ClassifiedsDbConnection { get; }
        string AppUserDbConnection { get; }
    }

    public class TestConfiguration : IConfig
    {
        public string BaseUrl => GetSafeUrl("BaseUrl");

        public string BaseAdminUrl => GetSafeUrl("BaseAdminUrl");

        public string ClassifiedsDbConnection => FromEnvironmentOrConfig("ClassifiedsConnection");

        public string AppUserDbConnection => FromEnvironmentOrConfig("AppUserConnection");

        private static string FromEnvironmentOrConfig(string key)
        {
            return Environment.GetEnvironmentVariable(key)
                .Default(ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        private string GetSafeUrl(string urlSettingName)
        {
            var url = ConfigurationManager.AppSettings.Get(urlSettingName);
            if (url.IsNullOrEmpty())
                throw new ConfigurationErrorsException(string.Format("Missing [{0}] configuration from app.config", urlSettingName));

            return !url.EndsWith("/") ? url.Append("/") : url;
        }
    }

    internal class ConfigFactory
    {
        public static IConfig CreateConfig()
        {

            return new TestConfiguration();

        }
    }

}
