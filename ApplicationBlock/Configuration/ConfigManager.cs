using System;
using System.Configuration;
using System.Web.Configuration;

namespace Paramount.ApplicationBlock.Configuration
{
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

        public static string GetApplicationName()
        {
            return WebConfigurationManager.AppSettings.Get(ApplicationNameKey);
        }

        public static string GetDomain()
        {
            return WebConfigurationManager.AppSettings.Get(DomainNameKey);
        }

        /// <summary>
        /// Gets the app setting value using the ConfigurationManager and returns as string value
        /// </summary>
        public static string ReadAppSetting(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(value))
                return null;
            return value;
        }

        /// <summary>
        /// Gets the app setting value with conversion by direct cast
        /// </summary>
        public static T ReadAppSetting<T>(string key) 
        {
            return (T)Convert.ChangeType(ReadAppSetting(key), typeof(T));
        }

        /// <summary>
        /// Gets the app setting value with conversion but uses the defaultValue as a fallback 
        /// </summary>
        public static T ReadAppSetting<T>(string key, T defaultValue)
        {
            var value = ReadAppSetting(key);
            if (value.IsNullOrEmpty())
                return defaultValue;

            try
            {
                return (T) Convert.ChangeType(value, typeof (T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
