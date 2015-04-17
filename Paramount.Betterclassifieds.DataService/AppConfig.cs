using System.IO;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System.Configuration;
    using Business;
    using ApplicationBlock.Configuration;

    public class AppConfig : IApplicationConfig
    {
        public string BaseUrl
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("BaseUrl");
            }
        }

        public string Brand
        {
            get { return ConfigManager.ReadAppSetting<string>("Brand"); }
        }

        public string DslImageUrlHandler
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("DslImageUrlHandler");
            }
        }

        public bool UseHttps
        {
            get
            {
                return ConfigManager.ReadAppSetting("UseHttps", false);
            }
        }

        public string ImageCacheDirectory
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("ImageCacheDirectory");
            }
        }

        public DirectoryInfo ImageCropDirectory
        {
            get
            {
                var dir = new DirectoryInfo(ConfigManager.ReadAppSetting<string>("ImageCropDirectory"));
                if (!dir.Exists)
                {
                    dir.Create();
                }

                return dir;
            }
        }

        public int MaxImageUploadBytes
        {
            get { return ConfigManager.ReadAppSetting<int>("MaxImageUploadBytes"); }
        }

        public string[] AcceptedImageFileTypes
        {
            get { return ConfigManager.ReadAppSetting<string>("AcceptedFileTypes").Split('|'); }
        }

        public bool IsPaymentEnabled
        {
            get { return ConfigManager.ReadAppSetting<bool>("IsPaymentEnabled"); }
        }
    }
}