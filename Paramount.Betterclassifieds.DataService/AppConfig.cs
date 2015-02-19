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
                return ConfigurationManager.AppSettings["ImageCacheDirectory"];
            }
        }

        public DirectoryInfo ImageCropDirectory
        {
            get
            {
                var dir = new DirectoryInfo(ConfigurationManager.AppSettings["ImageCropDirectory"]);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                return dir;
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

        public bool IsPaymentEnabled
        {
            get { return ConfigManager.ReadAppSetting<bool>("IsPaymentEnabled"); }
        }

        public string Brand
        {
            get { return ConfigManager.ReadAppSetting<string>("Brand"); }
        }
    }
}