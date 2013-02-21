namespace Paramount.Membership.UIController
{
    using System;
    using System.Web;
    using System.Web.Security;
    using ApplicationBlock.Configuration;
    using Common.DataTransferObjects;
    using Common.DataTransferObjects.MembershipService;
    using Common.DataTransferObjects.MembershipService.Messages;

    public static  class Extensions
    {
        public static void SetBaseRequest(this MembershipProviderBaseRequest request, string group)
        {
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;
            request.ProviderName = Membership.Provider.Name;
            request.AuditData = new AuditData()
            {
                BrowserType = HttpContext.Current.Request.UserAgent + HttpContext.Current.Request.Browser,
                ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                GroupingId = group,
                HostName = HttpContext.Current.Request.UserHostName,
                SessionId = HttpContext.Current.Session.SessionID,
                Username = HttpContext.Current.User.Identity.Name,
                CreatedDate = DateTime.Now
            };
        }

        public static void SetBaseRequest(this BaseRequest request, string group)
        {
            request.ApplicationName = ConfigSettingReader.ApplicationName;
            request.ClientCode = ConfigSettingReader.ClientCode;
            request.Domain = ConfigSettingReader.Domain;
            request.Initialise = true;
            request.AuditData = new AuditData()
            {
                BrowserType = HttpContext.Current.Request.UserAgent + HttpContext.Current.Request.Browser,
                ClientIpAddress = HttpContext.Current.Request.UserHostAddress,
                GroupingId = group,
                HostName = HttpContext.Current.Request.UserHostName,
                SessionId = HttpContext.Current.Session == null ? "" : HttpContext.Current.Session.SessionID,
                Username = HttpContext.Current.User.Identity.Name,
                CreatedDate = DateTime.Now
            };
        }

        public static MembershipUser ConvertToExternal(this MembershipUser membershipUser)
        {
            return new ParamountMembershipUser(
                Membership.Provider.Name,
                membershipUser.UserName,
                membershipUser.ProviderUserKey,
                membershipUser.Email,
                membershipUser.PasswordQuestion,
                membershipUser.Comment,
                membershipUser.IsApproved,
                membershipUser.IsLockedOut,
                membershipUser.CreationDate,
                membershipUser.LastLoginDate,
                membershipUser.LastActivityDate,
                membershipUser.LastPasswordChangedDate,
                membershipUser.LastLockoutDate
                )
                       {
                           ProfileInformation = ProfileServiceController.GetProfile(membershipUser.UserName)
                       };
        }
    }
}