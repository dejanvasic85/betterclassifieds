using System.Linq;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string BetterclassifiedsAppId = "betterclassified";
        private const string AdminAppId = "betterclassifiedadmin";

        public ApplicationUser GetClassifiedUser(string username)
        {
            using (BetterclassifiedsCore.DataModel.AppUserDataContext context = BetterclassifiedsCore.DataModel.AppUserDataContext.NewContext())
            {
                var application = context.aspnet_Applications.First(a => a.LoweredApplicationName == BetterclassifiedsAppId);
                
                var user = context.aspnet_Users.First(u => 
                    u.UserName == username &&
                    u.ApplicationId == application.ApplicationId);

                var profile = context.UserProfiles.First(p => p.UserID == user.UserId);

                return new ApplicationUser { Email = profile.Email, Username = user.UserName };
            }
        } 
    }
}