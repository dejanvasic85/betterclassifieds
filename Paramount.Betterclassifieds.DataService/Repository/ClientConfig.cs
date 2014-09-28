using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class ClientConfig : IClientConfig
    {
        // Todo - read from database (but default these for TheMusic)
        public int RestrictedEditionCount { get { return 10; } }
        public int RestrictedOnlineDaysCount { get { return 30; } }
        public int NumberOfDaysAfterLastEdition { get { return 6; } }
        public bool IsOnlineAdFree { get { return true; } }

        public string PublisherHomeUrl { get { return ConfigManager.ReadAppSetting<string>("PublisherHomeUrl"); } }
        public string FacebookAppId { get { return ConfigManager.ReadAppSetting<string>("FacebookAppId"); } }

        public int SearchResultsPerPage { get { return 10; } }
           
        public int SearchMaxPagedRequests { get { return 100; } }

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
            get
            {
                return ConfigManager.ReadAppSetting("SupportEmails").Split(';');
            }
        }

        public int? MaxOnlineImages
        {
            get { return 2; }
        }
    }
}