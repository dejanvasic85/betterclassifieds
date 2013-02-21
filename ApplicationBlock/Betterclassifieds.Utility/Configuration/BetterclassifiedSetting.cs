using System;
namespace Paramount.Betterclassified.Utilities.Configuration
{
    public static class BetterclassifiedSetting
    {
        private const string VersionKey = "Version";
        private const string WebAdSpacesKey = "WebAdSpaces";
        private const string SupportGeneralEnquiryKey = "SupportGeneralEnquiry";
        private const string SupportTechnicalKey = "SupportTechnical";
        private const string SupportSalesBillingKey = "SupportSalesBilling";
        private const string SupportFeedbackKey = "SupportFeedback";
        private const string DslCacheDirectoryKey = "DslCacheDirectory";
        private const string DslImageUrlHandlerKey = "DslImageUrlHandler";
        private const string DslThumbWidthKey = "DslThumbWidth";
        private const string DslThumbHeightKey = "DslThumbHeight";
        private const string DslDefaultResolutionKey = "DslDefaultResolution";
        private const string LineAdImageHeightKey = "LineAdImageHeight";
        private const string LineAdImageWidthKey = "LineAdImageWidth";
        private const string LineAdImageDpiKey = "LineAdImageDpi";

        public static string Version
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(VersionKey);
            }
        }

        public static string WebAdSpaces
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(WebAdSpacesKey);
            }
        }

        public static string SupportGeneralEnquiryEmails
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(SupportGeneralEnquiryKey);
            }
        }

        public static string SupportTechnicalEmails
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(SupportTechnicalKey);
            }
        }

        public static string SupportSalesBillingEmails
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(SupportSalesBillingKey);
            }
        }

        public static string SupportFeedbackEmails
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(SupportFeedbackKey);
            }
        }

        public static string DslImageUrlHandler
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(DslImageUrlHandlerKey);
            }
        }

        public static int DslThumbWidth
        {
            get
            {
                int width;
                int.TryParse(ParamountConfigurationManager.GetAppSettingValue(DslThumbWidthKey), out width);
                if (width == 0)
                {
                    width = 90; // set the default value of 90 if the app setting doesn't exist
                }
                return width;
            }
        }

        public static int DslThumbHeight
        {
            get
            {
                int height;
                int.TryParse(ParamountConfigurationManager.GetAppSettingValue(DslThumbHeightKey), out height);
                if (height == 0)
                {
                    height = 90; // set the default value of 90 if the app setting doesn't exist
                }
                return height;
            }
        }

        public static int DslDefaultResolution
        {
            get
            {
                int res;
                int.TryParse(ParamountConfigurationManager.GetAppSettingValue(DslDefaultResolutionKey), out res);
                if (res == 0)
                {
                    res = 72; // set the default value of 72 if the app setting doesn't exist
                }
                return res;
            }
        }

        public static string DslCacheDirectory
        {
            get
            {
                return ParamountConfigurationManager.GetAppSettingValue(DslCacheDirectoryKey);
            }
        }

        public static double LineAdImageHeight
        {
            get
            {
                double height;
                double.TryParse(ParamountConfigurationManager.GetAppSettingValue(LineAdImageHeightKey), out height);
                if (height == 0)
                {
                    height = 1.10236; // Default to a value if cannot be found in config
                }
                return height;
            }
        }

        public static double LineAdImageWidth
        {
            get
            {
                double width;
                double.TryParse(ParamountConfigurationManager.GetAppSettingValue(LineAdImageWidthKey), out width);
                if (width == 0)
                {
                    width = 1.1811; // set default value
                }
                return width;
            }
        }

        public static int LineAdImageDPI
        {
            get
            {
                int dpi;
                int.TryParse(ParamountConfigurationManager.GetAppSettingValue(LineAdImageDpiKey), out dpi);
                if (dpi == 0)
                {
                    dpi = 300; // set default value
                }
                return dpi;
            }
        }

        public static bool IsAveragePriceUsedForLineAd
        {
            get
            {
                return Convert.ToBoolean(ParamountConfigurationManager.GetAppSettingValue("IsAveragePriceUsedForLineAd"));
            }
        }
    }
}