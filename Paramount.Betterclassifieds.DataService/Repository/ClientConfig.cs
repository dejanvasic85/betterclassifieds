using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class ClientConfig : IClientConfig
    {
        // Todo - read from database (but default these for TheMusic)
        public int RestrictedEditionCount { get { return 10; } }
        public int RestrictedOnlineDaysCount { get { return 30; } }
        public int NumberOfDaysAfterLastEdition { get { return 6; } }
        public bool IsOnlineAdFree { get { return true; } }

        public string PublisherHomeUrl { get { return ConfigManager.ReadAppSetting<string>("PublisherHomeUrl"); } }
        public string FacebookAppId { get { return ConfigManager.ReadAppSetting<string>("FacebookAppId"); } }
        
    }
}