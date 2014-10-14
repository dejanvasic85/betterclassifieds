using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class AppConfig : IApplicationConfig
    {
        public string BaseUrl
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("BaseUrl");
            }
        }

        public string DslImageUrlHandler
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("DslImageUrlHandler");
            }
        }

        public string ClientCode
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("ClientCode");
            }
        }

        public string ConfigurationContext
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("ConfigurationContext");
            }
        }

        public bool UseHttps
        {
            get
            {
                // Todo - need to come back to this for the login page (very soon)
                return ConfigManager.ReadAppSetting("UseHttps", false);
            }
        }

        public string ImageCacheDirectory
        {
            get
            {
                return ConfigManager.GetSetting("paramount/dsl", "ImageCacheDirectory");
            }
        }

        public int MaxImageUploadBytes
        {
            get { return int.Parse(ConfigManager.GetSetting("paramount/dsl", "MaxImageUploadBytes")); }
        }

        public string[] AcceptedImageFileTypes
        {
            get { return ConfigManager.GetSetting("paramount/dsl", "AcceptedFileTypes").Split('|'); }
        }
    }
}