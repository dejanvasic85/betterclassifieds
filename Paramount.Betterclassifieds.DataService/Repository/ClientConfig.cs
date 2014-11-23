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
            get { return ConfigManager.ReadAppSetting<string>("FacebookAppId"); }
        }

        public int SearchResultsPerPage
        {
            get { return GetValueFromDatabase<int>("SearchResultsPerPage"); }
        }

        public int SearchMaxPagedRequests
        {
            get { return GetValueFromDatabase<int>("SearchMaxPagedRequests"); }
        }

        // Todo
        public Address ClientAddress
        {
            get
            {
                // Hard code this just for now
                return new Address
                {
                    AddressLine1 = "Street Press Australia Pty Ltd",
                    AddressLine2 = "Level 1, 221 Kerr Street",
                    Suburb = "Fitzroy",
                    State = "VIC",
                    Postcode = "3068",
                    Country = "Australia",
                    PhoneNumber = "61 3 9421 4499"
                };
            }
        }

        public string[] SupportEmailList
        {
            get { return GetValueFromDatabase<string>("SupportNotificationAccounts").Split(';'); }
        }

        public int? MaxOnlineImages
        {
            get { return 2; }
        }
    }
}