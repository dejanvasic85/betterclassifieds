namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System;
    using System.Configuration;
    using System.Linq;
    using Business;

    public class ClientConfig : IClientConfig
    {
        private readonly IDbContextFactory _dbContextFactory;

        public ClientConfig(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        private string GetValueFromDatabase(string settingName, bool required = true)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var appSetting = context.AppSettings.FirstOrDefault(setting => setting.AppKey == settingName);
                if (appSetting == null && required)
                {
                    throw new ConfigurationErrorsException(string.Format("Setting [{0}] does not exist for Client", settingName));
                }

                if (appSetting == null)
                {
                    // Setting is not required - so just return the default value
                    return null;
                }
                return appSetting.SettingValue;
            }
        }

        private T GetValueFromDatabase<T>(string settingName, bool required = true)
        {
            var appSetting = GetValueFromDatabase(settingName, required);

            if (appSetting == null)
                return default(T);
            
            var converted = Convert.ChangeType(appSetting, typeof(T));

            return (T)converted;

        }

        public int RestrictedEditionCount
        {
            // Print setting
            get { return GetValueFromDatabase<int>("MaximumInsertions", false); }
        }

        public int RestrictedOnlineDaysCount
        {
            get { return GetValueFromDatabase<int>("AdDurationDays"); }
        }

        public int NumberOfDaysAfterLastEdition
        {
            // Print setting
            get { return GetValueFromDatabase<int>("NumberOfDaysAfterLastEdition", false); }
        }

        public string FacebookAppId
        {
            get { return GetValueFromDatabase("FacebookAppId", required: false); }
        }

        public int SearchResultsPerPage
        {
            get { return GetValueFromDatabase<int>("SearchResultsPerPage"); }
        }

        public int SearchMaxPagedRequests
        {
            get { return GetValueFromDatabase<int>("SearchMaxPagedRequests"); }
        }

        public Address ClientAddress
        {
            get { return Address.FromCsvString(GetValueFromDatabase("ClientAddress"), ','); }
        }

        public Tuple<string, string> ClientAddressLatLong
        {
            get
            {
                var clientAddressLatLong = GetValueFromDatabase("ClientAddressLatLong").Split(',');
                return new Tuple<string, string>(clientAddressLatLong[0], clientAddressLatLong[1]);
            }
        }

        public string ClientPhoneNumber
        {
            get { return GetValueFromDatabase("ClientPhoneNumber", false); }
        }

        public string[] SupportEmailList
        {
            get { return GetValueFromDatabase("SupportNotificationAccounts").Split(';'); }
        }

        public int? MaxOnlineImages
        {
            get { return GetValueFromDatabase<int>("MaxOnlineImages"); }
        }

        public string PublisherHomeUrl
        {
            get { return GetValueFromDatabase("PublisherHomeUrl", false); }
        }

        public bool IsTwoFactorAuthEnabled
        {
            get { return GetValueFromDatabase<bool>("EnableTwoFactorAuth"); }
        }

        public int PrintImagePixelsWidth
        {
            get { return GetValueFromDatabase<int>("PrintImagePixelsWidth"); }
        }

        public int PrintImagePixelsHeight
        {
            get { return GetValueFromDatabase<int>("PrintImagePixelsHeight"); }
        }

        public int PrintImageResolution
        {
            get { return GetValueFromDatabase<int>("PrintImageResolution"); }
        }

        public string ClientName
        {
            get { return GetValueFromDatabase<string>("ClientName"); }
        }
    }
}