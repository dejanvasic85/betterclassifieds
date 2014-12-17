namespace Paramount.Betterclassifieds.Business
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using Broadcast;

    public interface IUserManager
    {
        ApplicationUser GetUserByEmailOrUsername(string emailOrUsername);
        ApplicationUser GetCurrentUser(IPrincipal principal);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
        void CreateUserNetwork(IPrincipal user, string email, string fullName);
        RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword);
        void ConfirmRegistration(RegistrationModel registerModel);
        void UpdateUserProfile(ApplicationUser applicationUser);
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IClientConfig _clientConfig;

        public UserManager(IUserRepository userRepository, IAuthManager authManager, IBroadcastManager broadcastManager, IClientConfig clientConfig)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _broadcastManager = broadcastManager;
            _clientConfig = clientConfig;
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
        
        public void CreateUserNetwork(IPrincipal user, string email, string fullName)
        {
            var userNetworkModel = new UserNetworkModel(user.Identity.Name, email, fullName);

            _userRepository.CreateUserNetwork(userNetworkModel);
        }

        public RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword)
        {
            registrationModel
                .GenerateUniqueUsername(_authManager.CheckUsernameExists)
                .SetPasswordFromPlaintext(plaintextPassword)
                .GenerateToken();
            
            // Create the registration in the db
            _userRepository.CreateRegistration(registrationModel);
            
            return new RegistrationResult(registrationModel, _clientConfig.IsTwoFactorAuthEnabled);
        }

        public void ConfirmRegistration(RegistrationModel registerModel)
        {
            registerModel.Confirm();
            _userRepository.UpdateRegistrationByToken(registerModel);
        }

        public void UpdateUserProfile(ApplicationUser applicationUser)
        {
            // Fetch the original and only update certain properties
            var original = GetUserByEmailOrUsername(applicationUser.Username);

            original.FirstName = applicationUser.FirstName;
            original.LastName = applicationUser.LastName;
            original.AddressLine1 = applicationUser.AddressLine1;
            original.AddressLine2 = applicationUser.AddressLine2;
            original.Postcode = applicationUser.Postcode;
            original.State = applicationUser.State;
            
            // Update
            _userRepository.UpdateUserProfile(original);
        }
    }
}