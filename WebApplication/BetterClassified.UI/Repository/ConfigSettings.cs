using Paramount.ApplicationBlock.Configuration;

namespace BetterClassified.Repository
{
    public class ConfigSettings : IConfigManager
    {
        // Todo - read from database (but default these for TheMusic)
        public int RestrictedEditionCount { get { return 10; } }
        public int RestrictedOnlineDaysCount { get { return 30; } }
        public int NumberOfDaysAfterLastEdition { get { return 6; } }
        public bool IsOnlineAdFree { get { return true; } }


        public string BaseUrl { get { return ConfigManager.ReadAppSetting<string>("BaseUrl"); } }
        public string PublisherHomeUrl { get { return ConfigManager.ReadAppSetting<string>("PublisherHomeUrl"); } }
        public string FacebookAppId { get { return ConfigManager.ReadAppSetting<string>("FacebookAppId"); } }
        public string DslImageUrlHandler { get { return ConfigManager.ReadAppSetting<string>("DslImageUrlHandler"); } }
        public string ClientCode { get { return ConfigManager.ReadAppSetting<string>("ClientCode"); } }
    }
}