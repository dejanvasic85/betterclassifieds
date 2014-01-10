namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Web.Security;
    using Common.DataTransferObjects.MembershipService;
    using LinqObjects;

    internal static class Utils
    {

        internal static string GetUsername(string username, string domain)
        {
            return string.Format("{0}{1}{2}", domain, char.ConvertFromUtf32(254), username);
        }

        internal static string GetUserEmail(string email, string domain)
        {
            return string.Format("{0}{1}{2}", domain, char.ConvertFromUtf32(254), email);
        }

        internal static string  GetUsername(this MembershipUser membershipUser)
        {
            var fullUsername = membershipUser.UserName.Split(new[] {Convert.ToChar(char.ConvertFromUtf32(254))});
            return fullUsername.Length == 2 ? fullUsername[1] : fullUsername[0];
        }



        internal static string GetUsername(this aspnet_User membershipUser)
        {
            var fullUsername = membershipUser.UserName.Split(new[] { Convert.ToChar(char.ConvertFromUtf32(254)) });
            return fullUsername.Length == 2 ? fullUsername[1] : fullUsername[0];
        }


        internal static string GetEmail(this MembershipUser membershipUser)
        {
            var fullEmail = membershipUser.Email.Split(new[] { Convert.ToChar(char.ConvertFromUtf32(254)) });
            return fullEmail.Length == 2 ? fullEmail[1] : fullEmail[0];
        }

        internal static MembershipUser ConvertToExternal(this MembershipUser membershipUser, string clientProviderName)
        {
            if(membershipUser== null)
            {
                return null;
            }
            var userName = membershipUser.GetUsername();
            var email = membershipUser.GetEmail();
            return new MembershipUser(
                membershipUser.ProviderName,
                userName,
                membershipUser.ProviderUserKey,
                email,
                membershipUser.PasswordQuestion,
                membershipUser.Comment,
                membershipUser.IsApproved,
                membershipUser.IsLockedOut,
                membershipUser.CreationDate,
                membershipUser.LastLoginDate,
                membershipUser.LastActivityDate,
                membershipUser.LastPasswordChangedDate,
                membershipUser.LastLockoutDate
                );
        }

        internal static MembershipUser ConvertToInternal(this MembershipUser membershipUser, string clientCode)
        {
            return new MembershipUser(
                membershipUser.ProviderName,
                GetUsername(membershipUser.UserName, clientCode),
                membershipUser.ProviderUserKey,
                GetUserEmail(membershipUser.Email, clientCode ),
                membershipUser.PasswordQuestion,
                membershipUser.Comment,
                membershipUser.IsApproved,
                membershipUser.IsLockedOut,
                membershipUser.CreationDate,
                membershipUser.LastLoginDate,
                membershipUser.LastActivityDate,
                membershipUser.LastPasswordChangedDate,
                membershipUser.LastLockoutDate
                );
        }

        internal static bool  UsernameContainsDomain(this MembershipUser membershipUser)
        {
            var fullUsername = membershipUser.UserName.Split(new[] { Convert.ToChar(char.ConvertFromUtf32(254)) });
            return fullUsername.Length == 2;
        }

        internal static bool EmailContainsDomain(this MembershipUser membershipUser)
        {
            var fullEmail = membershipUser.Email.Split(new[] { Convert.ToChar(char.ConvertFromUtf32(254)) });
            return fullEmail.Length == 2;
        }

        internal static ProfileInfo ConvertProfile(this UserProfile profile)
        {
            var profileInfo = new ProfileInfo {Abn = profile.ABN, Address1 = profile.Address1};
            profileInfo.Address2 = profileInfo.Address2;
            profileInfo.AccountCategory = profile.BusinessCategory;
            profileInfo.BusinessName = profile.BusinessName;
            profileInfo.City = profile.City;
            profileInfo.SecondaryEmail = profile.Email;
            profileInfo.FirstName = profile.FirstName;
            profileInfo.LastName = profile.LastName;
            profileInfo.Industry = profile.Industry;
            profileInfo.NewsletterSubscription = profile.NewsletterSubscription;
            profileInfo.Phone = profile.Phone;
            profileInfo.Postcode = profile.PostCode;
            profileInfo.AccountId = profile.RefNumber;
            return profileInfo;
        }
    }
}