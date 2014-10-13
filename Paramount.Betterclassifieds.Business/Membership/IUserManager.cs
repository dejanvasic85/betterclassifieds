using System.Collections.Generic;
using System.Security.Principal;

namespace Paramount.Betterclassifieds.Business
{
    public interface IUserManager
    {
        ApplicationUser GetUserByEmailOrUsername(string emailOrUsername);
        ApplicationUser GetCurrentUser(IPrincipal principal);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
        void CreateUserProfile(string email, string firstName, string lastName, string postCode);
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;

        public UserManager(IUserRepository userRepository, IAuthManager authManager)
        {
            _userRepository = userRepository;
            _authManager = authManager;
        }

        public ApplicationUser GetUserByEmailOrUsername(string emailOrUsername)
        {
            // Fetch by email first ( new users ) 
            var userByEmail = _userRepository.GetUserByEmail(emailOrUsername);
            if (userByEmail != null)
                return userByEmail;

            return _userRepository.GetUserByUsername(emailOrUsername);
        }

        public ApplicationUser GetCurrentUser(IPrincipal principal)
        {
            if (!_authManager.IsUserIdentityLoggedIn(principal))
                return null;

            return GetUserByEmailOrUsername(principal.Identity.Name);
        }

        public IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId)
        {
            return _userRepository.GetUserNetworksForUserId(userId);
        }

        public void CreateUserProfile(string email, string firstName, string lastName, string postCode)
        {
            // Simply persist directly to the repository
            _userRepository.CreateUser(email, firstName, lastName, postCode);
        }
    }
}