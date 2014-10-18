using System.Collections.Generic;
using System.Configuration;
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

        public string[] AvailablePaymentProviders
        {
            get
            {
                var section = ConfigurationManager.GetSection("paymentProviders") as PaymentProvidersSection;

                if (section == null)
                    throw new ConfigurationErrorsException("Please ensure to have at least one payment provider available");

                var values = new List<string>();
                
                if (section.MockProvider != null && section.MockProvider.FriendlyName.HasValue())
                    values.Add(section.MockProvider.FriendlyName);

                if (section.PayPalProvider != null && section.PayPalProvider.FriendlyName.HasValue())
                    values.Add(section.PayPalProvider.FriendlyName);

                if (section == null)
                    throw new ConfigurationErrorsException("Please ensure to have at least one payment provider available");

                return values.ToArray();
            }
        }
    }
}