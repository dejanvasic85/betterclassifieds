using System;

namespace Paramount.ApplicationBlock.Configuration
{
    using System.Configuration;
    using System.IO;
    using System.Web.Configuration;
    using Utility;

    public static class ConfigSettingReader
    {
        private const string DslSectionName = "paramount/dsl";
        private const string BillingStepsSectionName = "paramount/billingSteps";
        private const string DslDefaultResolutionKey = "DefaultDslResolution";
        private const string DslChunkLengthKey = "ChunkLength";
        private const string DslIsServerCachingKey = "IsServerImageCaching";
        private const string DslImageCacheDirectoryKey = "ImageCacheDirectory";
        private const string DslImageHandlerKey = "ImageHandlerUrl";
        

        private const string ParamountSectionName = "paramount/services";
        private const string WebServiceCredentialKey = "WebServiceCredential";
        private const string ClientCodeKey = "ClientCode";
        private const string ApplicationNameKey = "ApplicationName";
        private const string DomainNameKey = "Domain";

        private const string FileAppendMaxRetryKey = "FileAppendMaxRetry";
        private const int DefaultFileAppendMaxRetry = 5;


        public static string GetConfigurationContext()
        {
            var context = WebConfigurationManager.AppSettings.Get("ConfigurationContext");
            var value = !string.IsNullOrEmpty(context) ? context : string.Empty;
            return value;
        }

        public static string ApplicationName { get { return ConfigurationManager.AppSettings.Get(ApplicationNameKey); } }

        public static string Domain { get { return WebConfigurationManager.AppSettings.Get(DomainNameKey); } }

        public static string ClientCode
        {
            get { return ConfigurationManager.AppSettings[ClientCodeKey]; }
        }

        public static string ClientCodeEncrypted
        {
            get { return Paramount.Utility.CryptoHelper.Encrypt(ConfigurationManager.AppSettings[ClientCodeKey]); }
        }

        public static int FileAppendMaxRetry
        {
            get
            {
                int value;
                return int.TryParse(ConfigurationManager.AppSettings[FileAppendMaxRetryKey], out value) ? value : DefaultFileAppendMaxRetry;
            }
        }

        public static string WebServiceCredential
        {
            get
            {
                var value = ConfigManager.GetSetting(ParamountSectionName, WebServiceCredentialKey);
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }

                if (Equals(value[value.Length - 1], "="))
                {
                    return CryptoHelper.Decrypt(value);
                }

                return value;
            }
        }

        public static int DslDefaultResolution
        {
            get { return int.Parse(ConfigManager.GetSetting(DslSectionName, DslDefaultResolutionKey)); }
        }

        public static int DslChunkLength
        {
            get { return int.Parse(ConfigManager.GetSetting(DslSectionName, DslChunkLengthKey)); }
        }

        public static bool DslIsServerCaching
        {
            get { return bool.Parse(ConfigManager.GetSetting(DslSectionName, DslIsServerCachingKey)); }
        }

        public static DirectoryInfo DslImageCacheDirectory
        {
            get
            {
                // Get the image path from the Master config
                var imageCachePath = ConfigManager.GetSetting(DslSectionName, DslImageCacheDirectoryKey);
                if (string.IsNullOrEmpty(imageCachePath))
                {
                    return null;
                }

                // Create the Directory if it doesn't exist already
                var directoryInfo = new DirectoryInfo(imageCachePath);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                return directoryInfo;
            }
        }

        public static string DslImageHandler
        {
            get
            {
                return ConfigManager.GetSetting(DslSectionName, DslImageHandlerKey);
            }
        }


        public static string BillingStepAddressUrl
        {
            get { return ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepAddressUrl"); }
        }

        public static string BillingStepPaymentOptionUrl
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepPaymentOptionUrl")); }
        }

       

        public static string BillingStepFinalInvoiceUrl
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepFinalInvoiceUrl")); }
        }

        public static string BillingStepPayPalPayment
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepPayPalPayment")); }
        }

        public static string BillingStepBankPayment
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepBankPayment")); }
        }

        public static string BillingStepCancelPurchase
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepCancelPurchase")); }
        }

        public static string BillingStepFailed
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepFailed")); }
        }

        public static string BillingStepSuccessWaiting
        {
            get { return (ConfigManager.GetSetting(BillingStepsSectionName, "BillingStepSuccessWaiting")); }
        }

     
    }
}