using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class UserNetworkRepository : IUserNetworkRepository
    {
        public IEnumerable<UserNetworkModel> GetUserNetworkForUserId(string userId)
        {
            using (var context = new ClassifiedsEntityContext())
            {
                return context.UserNetworks.Where(b => b.UserId.Equals(userId)).ToList();
            }
        }
    }
}
