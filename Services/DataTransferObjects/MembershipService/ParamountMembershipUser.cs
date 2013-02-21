namespace Paramount.Common.DataTransferObjects.MembershipService
{
    using System;
    using System.Web.Security;

    [Serializable]
    public class ParamountMembershipUser:MembershipUser
    {
        public ParamountMembershipUser( string providerName, string userName, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
            : base(providerName,
                userName,
                providerUserKey,
                email,
                passwordQuestion,
                comment,
                isApproved,
                isLockedOut,
                creationDate,
                lastLoginDate,
                lastActivityDate,
                lastPasswordChangedDate,
                lastLockoutDate)
        {
        }

        public ProfileInfo ProfileInformation { get; set; }
    }
}