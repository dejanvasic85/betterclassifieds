using System;
using System.Configuration;
using System.Linq;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class ClientConfig : IClientConfig
    {
        private T GetValueFromDatabase<T>(string settingName, bool required = true)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var appSetting = context.AppSettings.FirstOrDefault(setting => setting.AppKey == settingName);
                if (appSetting == null && required)
                {
                    throw new ConfigurationErrorsException("Setting does not exist for Client");
                }

                if (appSetting == null)
                {
                    // Setting is not required - so just return the default value
                    return default(T);
                }

                var converted = Convert.ChangeType(appSetting.SettingValue, typeof(T));

                return (T)converted;
            }
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
            get { return GetValueFromDatabase<string>("FacebookAppId"); }
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
            get { return Address.FromCsvString(GetValueFromDatabase<string>("ClientAddress"), ','); }
        }

        public Tuple<string,string> ClientAddressLatLong
        {
            get
            {
                var clientAddressLatLong = GetValueFromDatabase<string>("ClientAddressLatLong").Split(',');
                return new Tuple<string, string>(clientAddressLatLong[0], clientAddressLatLong[1]);
            }
        }

        public string ClientPhoneNumber
        {
            get { return GetValueFromDatabase<string>("ClientPhoneNumber", false); }
        }

        public string[] SupportEmailList
        {
            get { return GetValueFromDatabase<string>("SupportNotificationAccounts").Split(';'); }
        }

        public int? MaxOnlineImages
        {
            get { return GetValueFromDatabase<int>("MaxOnlineImages"); }
        }

        public string PublisherHomeUrl
        {
            get { return GetValueFromDatabase<string>("PublisherHomeUrl", false); }
        }
    }
}