using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Managers;

namespace BetterClassified.Repository
{
    public class AppConfig : IApplicationConfig
    {
        public string BaseUrl { get { return ConfigManager.ReadAppSetting<string>("BaseUrl"); } }
        public string DslImageUrlHandler { get { return ConfigManager.ReadAppSetting<string>("DslImageUrlHandler"); } }
        public string ClientCode { get { return ConfigManager.ReadAppSetting<string>("ClientCode"); } }
    }
}