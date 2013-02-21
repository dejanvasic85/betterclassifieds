using System.Configuration;

namespace Paramount.Betterclassified.Utilities.Configuration
{
    public static class ParamountConfigurationManager
    {
        private const string Environment = "Environment";
       
        public static object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(string.Format("{0}{1}", sectionName, GetEnvironment()));
        }
      

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[string.Format("{0}{1}", key, GetEnvironment())].ConnectionString;
        }

        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[string.Format("{0}{1}", key, GetEnvironment())];
        }

        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        public static string GetEnvironment()
        {
            string env = ConfigurationManager.AppSettings[Environment];
            if(!string.IsNullOrEmpty(env))
            {
                env = string.Format("/{0}", env);
            }
            return env;
        }
    }
}
