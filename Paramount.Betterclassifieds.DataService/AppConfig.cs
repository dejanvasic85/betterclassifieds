using System.IO;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Configuration;

namespace Paramount.Betterclassifieds.DataService.Repository
{

    public class AppConfig : IApplicationConfig
    {
        public string Brand
        {
            get
            {
                return ConfigManager.ReadAppSetting<string>("Brand");
            }
        }

        public string Environment
        {
            get { return ConfigManager.ReadAppSetting<string>("Environment"); }
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