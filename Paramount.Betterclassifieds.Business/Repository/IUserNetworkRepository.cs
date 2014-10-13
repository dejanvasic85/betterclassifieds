using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IUserNetworkRepository
    {
        IEnumerable<UserNetworkModel> GetUserNetworkForUserId(string userId);
    }
}
