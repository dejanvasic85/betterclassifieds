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

        private ConfigReader(string configSectionName) : this(configSectionName, null) { }

        private ConfigReader(string configSectionName, string configKey)
        {
            this.configSectionName = configSectionName;
            this.configKey = configKey;
            this.LoadConnectionSettingsFromConfig();
        }

        public static string GetConnectionString(string configSectionName)
        {
            using (var configReader = new ConfigReader(configSectionName))
            {
                return configReader.ConnectionString();
            }
        }

        public static string GetConnectionString(string configSectionName, string configKey)
        {
            using (var configReader = new ConfigReader(configSectionName, configKey))
            {
                return configReader.ConnectionString();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged) { }

        private string ConnectionString()
        {
            if(configSection == null)
                throw new ConfigurationErrorsException("Config section has not been set up");

            return this.configSection[string.IsNullOrEmpty(this.configKey) ? "ConnectionString" : this.configKey].Value;
        }

        private void LoadConnectionSettingsFromConfig()
        {
            this.configSection = (ConfigurationDictionary)ConfigurationManager.GetSection(this.configSectionName);
        }
    }
}
