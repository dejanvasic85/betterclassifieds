using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IUserNetworkManager
    {
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
    }

    public class UserNetworkManager : IUserNetworkManager
    {
        private readonly IUserNetworkRepository _userNetworkRepository;

        public UserNetworkManager(IUserNetworkRepository userNetworkRepository)
        {
            _userNetworkRepository = userNetworkRepository;
        }

        public IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId)
        {
            return _userNetworkRepository.GetUserNetworksForUserId(userId);
        }
    }
}