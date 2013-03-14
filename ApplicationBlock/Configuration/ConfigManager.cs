using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Paramount.ApplicationBlock.Configuration
{
    using System.Configuration;
    using System.Threading;

    public static class ConfigManager
    {
        private const string ApplicationNameKey = "ApplicationName";
        private const string MaxRetriesKey = "MaxRetries";
        private const string DomainNameKey = "Domain";

        public static object GetSection(string name)
        {
            return ConfigurationManager.GetSection(name);
        }

        public static string ConfigurationContext
        {
            get { return ConfigurationManager.AppSettings.Get("ConfigurationContext"); }
        }

        public static string GetSetting(string section, string name)
        {
            var value = GetSection(section) as ConfigurationSectionHandler;

            if (value != null) return value[name].Value;
            throw new ConfigurationErrorsException(string.Format("Configuration Error: Cannot find section:{0} ", section));
        }

        //public  static ConfigurationItem GetSetting(string section, string name)
        //{
        //    var value = GetSection(section) as ConfigurationSectionHandler;

        //    if (value != null) return value[name];
        //    throw new ConfigurationErrorsException(string.Format("Configuration Error: Cannot find section:{0} ", section));
        //}

        public static int MaxDslRetries
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings.Get(MaxRetriesKey));
            }
        }

        public static string GetApplicationName()
        {
            return WebConfigurationManager.AppSettings.Get(ApplicationNameKey);
        }

        public static string GetDomain()
        {
            return WebConfigurationManager.AppSettings.Get(DomainNameKey);
        }

        public static T ReadAppSetting<T>(string key) where T : class
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(value))
                return null;
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
