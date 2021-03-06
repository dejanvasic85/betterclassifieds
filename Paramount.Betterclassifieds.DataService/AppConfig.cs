﻿using System.IO;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Configuration;

namespace Paramount.Betterclassifieds.DataService.Repository
{

    public class AppConfig : IApplicationConfig
    {
        public string Brand => ConfigManager.ReadAppSetting<string>("Brand");

        public string Environment => ConfigManager.ReadAppSetting<string>("Environment");

        public string GoogleTimezoneApiUrl => ConfigManager.ReadAppSetting<string>("GoogleTimezoneApiUrl");

        public string GoogleTimezoneApiKey => ConfigManager.ReadAppSetting<string>("GoogleTimezoneApiKey");

        public string StripeApiKey => ConfigManager.ReadAppSetting<string>("StripeApiKey");

        public string StripePublishableKey => ConfigManager.ReadAppSetting<string>("StripePublishableKey");

        public string Version => ConfigManager.ReadAppSetting<string>("Version");

        public string MailgunBaseUrl => ConfigManager.ReadAppSetting<string>("Mailgun.BaseUrl");

        public string MailgunApiKey => ConfigManager.ReadAppSetting<string>("Mailgun.ApiKey");
        public string MailgunDomain => ConfigManager.ReadAppSetting<string>("Mailgun.Domain");
        public bool GoogleCaptchaEnabled => ConfigManager.ReadAppSetting<bool>("GoogleRecaptcha.IsEnabled");

        public RecaptchaConfig GoogleRegistrationCatpcha => RecaptchaConfigReader.GetFromConfigSettings("Registration");
        public RecaptchaConfig GoogleGeneralEnquiryCatpcha => RecaptchaConfigReader.GetFromConfigSettings("GeneralEnquiry");
        public RecaptchaConfig GoogleAdEnquiryCatpcha => RecaptchaConfigReader.GetFromConfigSettings("AdEnquiry");
        public RecaptchaConfig ContactUs => RecaptchaConfigReader.GetFromConfigSettings("ContactUs");
        public RecaptchaConfig Enquiry => RecaptchaConfigReader.GetFromConfigSettings("Enquiry");

        public string DslImageUrlHandler => ConfigManager.ReadAppSetting<string>("DslImageUrlHandler");

        public bool UseHttps => ConfigManager.ReadAppSetting("UseHttps", false);

        public string ImageCacheDirectory => ConfigManager.ReadAppSetting<string>("ImageCacheDirectory");

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

        public int MaxImageUploadBytes => ConfigManager.ReadAppSetting<int>("MaxImageUploadBytes");

        public string[] AcceptedImageFileTypes => ConfigManager.ReadAppSetting<string>("AcceptedFileTypes").Split('|');

        public bool IsPaymentEnabled => ConfigManager.ReadAppSetting<bool>("IsPaymentEnabled");
    }
}