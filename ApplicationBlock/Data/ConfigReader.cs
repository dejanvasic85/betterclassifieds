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

        private ConfigReader(string configSectionName) : this(configSectionName, null) { }

        private ConfigReader(string configSectionName, string configKey)
        {
            this.configSectionName = configSectionName;
            this.configKey = configKey;
            LoadConnectionSettingsFromConfig();
        }

        public static string GetConnectionString(string configSectionName)
        {
            if (Settings.ContainsKey(configSectionName))
            {
                return Settings[configSectionName].ConnectionString();
            }

            //add
            lock (settings)
            {
                if (!Settings.ContainsKey(configSectionName))
                {
                    Settings.Add(configSectionName, new ConfigReader(configSectionName));
                }
            }
            return Settings[configSectionName].ConnectionString();
        }

        public static string GetConnectionString(string configSectionName, string configKey)
        {
            var key = string.Format("{0}/{1}", configSectionName, configKey);

            if (Settings.ContainsKey(key))
            {
                return Settings[key].ConnectionString();
            }

            //add
            lock (settings)
            {
                if (!Settings.ContainsKey(key))
                {
                    Settings.Add(key, new ConfigReader(configSectionName, configKey));
                }
            }
            return Settings[key].ConnectionString();
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
