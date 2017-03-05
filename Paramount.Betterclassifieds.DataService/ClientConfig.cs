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
                    throw new ConfigurationErrorsException($"Required setting [{settingName}] does not exist for Client");
                }

                return appSetting?.SettingValue;
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

        public int RestrictedEditionCount => GetValueFromDatabase<int>("MaximumInsertions", false);

        public int RestrictedOnlineDaysCount => GetValueFromDatabase<int>("AdDurationDays");

        public int NumberOfDaysAfterLastEdition => GetValueFromDatabase<int>("NumberOfDaysAfterLastEdition", false);

        public string FacebookAppId
        {
            get
            {
#if DEBUG
                return "1277927518889183"; // Hard coded kandobay development value, otherwise we read the db!
#else

                return GetValueFromDatabase("FacebookAppId", required: false);
#endif
            }
        }

        public int SearchResultsPerPage => GetValueFromDatabase<int>("SearchResultsPerPage");

        public int SearchMaxPagedRequests => GetValueFromDatabase<int>("SearchMaxPagedRequests");

        public Address ClientAddress => Address.FromCsvString(GetValueFromDatabase("ClientAddress"), ',');

        public Tuple<string, string> ClientAddressLatLong
        {
            get
            {
                var clientAddressLatLong = GetValueFromDatabase("ClientAddressLatLong").Split(',');
                return new Tuple<string, string>(clientAddressLatLong[0], clientAddressLatLong[1]);
            }
        }

        public string ClientPhoneNumber => GetValueFromDatabase("ClientPhoneNumber", false);

        public string[] SupportEmailList => GetValueFromDatabase("SupportNotificationAccounts").Split(';');

        public int? MaxOnlineImages => GetValueFromDatabase<int>("MaxOnlineImages");

        public string PublisherHomeUrl => GetValueFromDatabase("PublisherHomeUrl", false);

        public bool EnableRegistrationEmailVerification => GetValueFromDatabase<bool>("Security.EnableRegistrationEmailVerification");

        public int PrintImagePixelsWidth => GetValueFromDatabase<int>("PrintImagePixelsWidth");

        public int PrintImagePixelsHeight => GetValueFromDatabase<int>("PrintImagePixelsHeight");

        public int PrintImageResolution => GetValueFromDatabase<int>("PrintImageResolution");

        public string ClientName => GetValueFromDatabase<string>("ClientName");

        public int EventTicketReservationExpiryMinutes => GetValueFromDatabase<int>("EventTicketReservationExpiryMinutes");

        public int EventMaxTicketsPerBooking { get { return GetValueFromDatabase<int>("EventMaxTicketsPerBooking"); } }

        /// <summary>
        /// Stored as a whole number e.g. 4% and needs to be converted to an actual decimal by dividing by 100
        /// </summary>
        public decimal EventTicketFeePercentage => GetValueFromDatabase<decimal>("EventTicketFee");

        public decimal EventTicketFeeCents => GetValueFromDatabase<decimal>("EventTicketFeeCents");

        public bool IsPrintEnabled => GetValueFromDatabase<bool>("IsPrintEnabled", false);

        public bool EnablePayPalPayments => GetValueFromDatabase<bool>("Events.EnablePayPalPayments", false);

        public bool EnableCreditCardPayments => GetValueFromDatabase<bool>("Events.EnableCreditCardPayments", false);
        public string EmailFromAddress => GetValueFromDatabase<string>("Email.FromAddress");
        public string EmailDomain => GetValueFromDatabase<string>("Email.DomainName");
    }
}