using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IUserManager
    {
        ApplicationUser GetUserByEmailOrUsername(string emailOrUsername);
        void CreateUserProfile(string email, string firstName, string lastName, string postCode);
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApplicationUser GetUserByEmailOrUsername(string emailOrUsername)
        {
            // Fetch by email first ( new users ) 
            var userByEmail = _userRepository.GetUserByEmail(emailOrUsername);
            if (userByEmail != null)
                return userByEmail;

            return _userRepository.GetUserByUsername(emailOrUsername);
        }

        public void CreateUserProfile(string email, string firstName, string lastName, string postCode)
        {
            // Simply persist directly to the repository
            _userRepository.CreateUser(email, firstName, lastName, postCode);
        }
    }
}