using Paramount.Betterclassifieds.Business.Models;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IUserNetworkRepository
    {
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
    }
}
