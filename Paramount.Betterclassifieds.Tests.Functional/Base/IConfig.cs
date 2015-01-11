using System;
using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BaseAdminUrl { get; }
        string ClassifiedsDbConnection { get; }
        string BroadcastDbConnection { get; }
        string AppUserDbConnection { get; }
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

        public string BaseAdminUrl
        {
            get
            {
                return GetSafeUrl("BaseAdminUrl");
            }
        }

        public string ClassifiedsDbConnection
        {
            get { return FromEnvironmentOrConfig("ClassifiedsConnection"); }
        }

        public string BroadcastDbConnection
        {
            get { return FromEnvironmentOrConfig("BroadcastConnection"); }
        }

        public string AppUserDbConnection
        {
            get { return FromEnvironmentOrConfig("AppUserConnection"); }
        }

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
