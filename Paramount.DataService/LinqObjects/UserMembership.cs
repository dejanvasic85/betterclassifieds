namespace Paramount.DataService.LinqObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using Paramount.ApplicationBlock.Data;

    partial class UserMembershipDataContext
    {
        private const string ConfigSection = "paramount/services";
        private const string SorceKey = "AppUserConnection";

        internal static UserMembershipDataContext Database
        {
            get
            {
                return new UserMembershipDataContext(ConfigReader.GetConnectionString(ConfigSection, SorceKey));
            }
        }

        public static PagedSource<aspnet_Membership> GetAllUsers(string domain, int pageIndex, int pageSize)
        {
            var list = (from member in Database.aspnet_Memberships
                        where
                            member.aspnet_User.UserName.Substring(0, 10).ToUpper(CultureInfo.InvariantCulture) ==
                            domain.ToUpper(CultureInfo.InvariantCulture) select member )
            ;
            return list.Paginate(pageIndex, pageSize, list.Count());
        }

        public static PagedSource<aspnet_Membership> GetSubscriptionUser(string applicationName, string userNameMatcch, bool includeAll, int pageIndex, int pageSize)
        {
            var list = (from profile in Database.UserProfiles
                        where
                            (profile.NewsletterSubscription || includeAll) &&
                            (Equals(profile.aspnet_User.aspnet_Application.ApplicationName.ToUpper(), applicationName.ToUpper()))
                            && profile.aspnet_User.UserName.StartsWith(userNameMatcch)
                        select profile.aspnet_User.aspnet_Membership);

            return list.Paginate(pageIndex, pageSize, list.Count());
        }

        public static void UnSubscribeUser(string username, string applicationName)
        {
            using (var db = Database)
            {
                var profile =
                    db.UserProfiles.Where(
                        user =>
                        user.aspnet_User.UserName.Equals(username) &&
                        user.aspnet_User.aspnet_Application.ApplicationName.Equals(applicationName)).FirstOrDefault();
                profile.NewsletterSubscription = false;
                db.SubmitChanges();

            }
        }
    }
}
