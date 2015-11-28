using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Paramount.ApplicationBlock.Membership
{
    public class ParamountMembershipUser : MembershipUser
    {
        public string ClientCode{ get; set; }
        public ParamountMembershipUser(string clientCode, MembershipUser user)
            : base(
            user.ProviderName,user.UserName,user.ProviderUserKey,user.Email,
            user.PasswordQuestion, user.Comment, user.IsApproved, user.IsLockedOut,
            user.CreationDate, user.LastLoginDate,user.LastActivityDate,user.LastPasswordChangedDate,
            user.LastLockoutDate)
        {
            ClientCode = clientCode;
        }
    }
}
