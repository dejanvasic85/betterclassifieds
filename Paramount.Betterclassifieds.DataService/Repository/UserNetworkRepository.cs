using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    using Business;

    public class UserNetworkRepository : IUserNetworkRepository
    {
        public IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId)
        {
            using (var context = new ClassifiedsEntityContext())
            {
                return context.UserNetworks.Where(b => b.UserId.Equals(userId)).ToList();
            }
        }
    }
}
