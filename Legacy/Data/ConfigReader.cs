using System.Collections.Generic;

namespace Paramount.ApplicationBlock.Data
{
    using Configuration;
    using System;
    using System.Configuration;

    public class ConfigReader : IDisposable
    {
        private readonly string configKey;
        private readonly string configSectionName;
        private ConfigurationDictionary configSection;
        private static Dictionary<string, ConfigReader> settings;

        internal static Dictionary<string, ConfigReader> Settings
        {
            get
            {
                if (settings != null)
                {
                    return settings;
                }

                return settings = new Dictionary<string, ConfigReader>();
            }
        }

        private ConfigReader(string configSectionName, string configKey)
        {
            this.configSectionName = configSectionName;
            this.configKey = configKey;
            LoadConnectionSettingsFromConfig();
        }

        public static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged) { }

        private string ConnectionString()
        {
            if (configSection == null)
                throw new ConfigurationErrorsException("Config section has not been set up");

            return configSection[string.IsNullOrEmpty(configKey) ? "ConnectionString" : configKey].Value;
        }

        private void LoadConnectionSettingsFromConfig()
        {
            configSection = (ConfigurationDictionary)ConfigurationManager.GetSection(configSectionName);
        }
    }
}
