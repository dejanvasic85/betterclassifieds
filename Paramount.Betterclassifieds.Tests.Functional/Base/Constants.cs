namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class Constants
    {

        /// <summary>
        /// Values stored in the publication type table as reference data
        /// </summary>
        public struct PublicationType
        {
            public const string Online = "ONLINE";
            public const string Newspaper = "NEWS";
        }


        /// <summary>
        /// Values stored in the AdType table as reference data
        /// </summary>
        public struct AdType
        {
            public const string LineAd = "LINE";
            public const string OnlineAd = "ONLINE";
        }

        public struct FrequencyType
        {
            public const string Weekly = "WEEKLY";
            public const string Daily = "DAILY";
            public const string Yearly = "YEARLY";
        }

        /// <summary>
        /// List of table names from the classified database
        /// </summary>
        /// <remarks>
        /// SELECT 'public const string ' + TABLE_NAME + ' = "' + TABLE_NAME + '";'
        /// FROM INFORMATION_SCHEMA.TABLES
        /// WHERE TABLE_TYPE = 'BASE TABLE' 
        /// </remarks>
        public struct Table
        {
            public const string Ad = "Ad";
            public const string AdBooking = "AdBooking";
            public const string AdBookingExtension = "AdBookingExtension";
            public const string AdDesign = "AdDesign";
            public const string AdGraphic = "AdGraphic";
            public const string AdType = "AdType";
            public const string AppSetting = "AppSetting";
            public const string BaseRate = "BaseRate";
            public const string BookEntry = "BookEntry";
            public const string Edition = "Edition";
            public const string EnquiryDocument = "EnquiryDocument";
            public const string EnquiryType = "EnquiryType";
            public const string LineAd = "LineAd";
            public const string LineAdColourCode = "LineAdColourCode";
            public const string LineAdTheme = "LineAdTheme";
            public const string Location = "Location";
            public const string LocationArea = "LocationArea";
            public const string Lookup = "Lookup";
            public const string MainCategory = "MainCategory";
            public const string NonPublicationDate = "NonPublicationDate";
            public const string OnlineAd = "OnlineAd";
            public const string OnlineAdRate = "OnlineAdRate";
            public const string OnlineAdEnquiry = "OnlineAdEnquiry";
            public const string Publication = "Publication";
            public const string PublicationAdType = "PublicationAdType";
            public const string PublicationCategory = "PublicationCategory";
            public const string PublicationRate = "PublicationRate";
            public const string PublicationSpecialRate = "PublicationSpecialRate";
            public const string PublicationType = "PublicationType";
            public const string Ratecard = "Ratecard";
            public const string SchemaVersions = "SchemaVersions";
            public const string SpecialRate = "SpecialRate";
            public const string SupportEnquiry = "SupportEnquiry";
            public const string TempBookingRecord = "TempBookingRecord";
            public const string Transaction = "Transaction";
            public const string TutorAd = "TutorAd";
            public const string WebAdSpace = "WebAdSpace";
            public const string WebAdSpaceSetting = "WebAdSpaceSetting";
            public const string WebContent = "WebContent";
        }

        /// <summary>
        /// List of table names from the membership (appuser) database
        /// </summary>
        /// <remarks>
        /// SELECT 'public const string ' + TABLE_NAME + ' = "' + TABLE_NAME + '";'
        /// FROM INFORMATION_SCHEMA.TABLES
        /// WHERE TABLE_TYPE = 'BASE TABLE' 
        /// </remarks>
        public struct MembershipTable
        {
            public const string EmailTemplate = "EmailTemplate";
            public const string UserProfile = "UserProfile";
            public const string aspnet_WebEvent_Events = "aspnet_WebEvent_Events";
            public const string aspnet_Applications = "aspnet_Applications";
            public const string ASPStateTempSessions = "ASPStateTempSessions";
            public const string Industry = "Industry";
            public const string aspnet_PersonalizationPerUser = "aspnet_PersonalizationPerUser";
            public const string BusinessCategory = "BusinessCategory";
            public const string aspnet_UsersInRoles = "aspnet_UsersInRoles";
            public const string EmailFrom = "EmailFrom";
            public const string aspnet_Membership = "aspnet_Membership";
            public const string smtpClient = "smtpClient";
            public const string Transaction = "Transaction";
            public const string aspnet_Function = "aspnet_Function";
            public const string aspnet_Profile = "aspnet_Profile";
            public const string aspnet_UsersInFunction = "aspnet_UsersInFunction";
            public const string aspnet_PersonalizationAllUsers = "aspnet_PersonalizationAllUsers";
            public const string Correspondence = "Correspondence";
            public const string SchemaVersions = "SchemaVersions";
            public const string aspnet_Paths = "aspnet_Paths";
            public const string Registration = "Registration";
            public const string aspnet_Roles = "aspnet_Roles";
            public const string aspnet_Users = "aspnet_Users";
            public const string ASPStateTempApplications = "ASPStateTempApplications";
            public const string aspnet_SchemaVersions = "aspnet_SchemaVersions";
            public const string ContentResource = "ContentResource";
        }
    }
}