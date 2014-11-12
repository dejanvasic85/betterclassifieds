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
            get { return ConfigurationManager.ConnectionStrings["ClassifiedsDb"].ConnectionString; }
        }

        public string BroadcastDbConnection
        {
            get { return ConfigurationManager.ConnectionStrings["BroadcastDb"].ConnectionString; }
        }

        public string AppUserDbConnection
        {
            get { return ConfigurationManager.ConnectionStrings["AppUserConnection"].ConnectionString; }
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
            Console.WriteLine("Resolving configuration");
            Console.WriteLine("TEAMCITY_JRE variable value {0}", Environment.GetEnvironmentVariable("TEAMCITY_JRE"));
            Console.WriteLine("env.TEAMCITY_JRE variable value {0}", Environment.GetEnvironmentVariable("env.TEAMCITY_JRE"));


            return new TestConfiguration();

        }
    }

}
