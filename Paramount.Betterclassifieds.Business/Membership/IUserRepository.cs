using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserByEmail(string email);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
        void CreateUserProfile(RegistrationModel registrationModel);
        void UpdateUserProfile(ApplicationUser applicationUser);
        void CreateUserNetwork(UserNetworkModel userNetworkModel);
        void CreateRegistration(RegistrationModel registrationModel);
        void UpdateRegistrationByToken(RegistrationModel registerModel);
        UserNetworkModel GetUserNetwork(int userNetworkId);
    }
}