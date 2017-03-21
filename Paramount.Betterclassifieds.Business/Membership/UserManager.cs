using System;

namespace Paramount.Betterclassifieds.Business
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using Broadcast;

    public interface IUserManager
    {
        ApplicationUser GetUserByEmail(string email);
        ApplicationUser GetUserByEmailOrUsername(string emailOrUsername);
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetCurrentUser();
        ApplicationUser GetCurrentUser(IPrincipal principal);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
        UserNetworkModel GetUserNetwork(int userNetworkId);
        UserNetworkModel CreateUserNetwork(string userId, string email, string fullName);
        RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword, bool disableTwoFactorAuth = false);
        RegistrationOrLoginResult LoginOrRegister(RegistrationModel registrationModel, string password);
        RegistrationConfirmationResult ConfirmRegistration(int registrationId, string token);
        void UpdateUserProfile(ApplicationUser applicationUser);
    }

    public class UserManager : IUserManager
    {
        private const int MaxConfirmationTokenAttempts = 5;

        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IClientConfig _clientConfig;
        private readonly IConfirmationCodeGenerator _confirmationCodeGenerator;
        private readonly IDateService _dateService;

        public UserManager(IUserRepository userRepository, IAuthManager authManager, IClientConfig clientConfig, IConfirmationCodeGenerator confirmationCodeGenerator, IDateService dateService)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _clientConfig = clientConfig;
            _confirmationCodeGenerator = confirmationCodeGenerator;
            _dateService = dateService;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public ApplicationUser GetUserByEmailOrUsername(string emailOrUsername)
        {
            // Fetch by email first ( new users ) 
            var userByEmail = _userRepository.GetUserByEmail(emailOrUsername);
            if (userByEmail != null)
                return userByEmail;

            return _userRepository.GetUserByUsername(emailOrUsername);
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public ApplicationUser GetCurrentUser()
        {
            throw new NotImplementedException("Cannot user this method here. Call the UserHttpManager instead");
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

        public UserNetworkModel GetUserNetwork(int userNetworkId)
        {
            return _userRepository.GetUserNetwork(userNetworkId);
        }

        public UserNetworkModel CreateUserNetwork(string userId, string email, string fullName)
        {
            var userNetworkModel = new UserNetworkModel(userId, email, fullName);
            _userRepository.CreateUserNetwork(userNetworkModel);

            return userNetworkModel;
        }

        public RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword, bool disableTwoFactorAuth = false)
        {
            var isConfirmationRequired = _clientConfig.EnableRegistrationEmailVerification && !disableTwoFactorAuth;

            registrationModel
                .GenerateUniqueUsername(_authManager.CheckUsernameExists)
                .SetPasswordFromPlaintext(plaintextPassword)
                .SetConfirmationCode(_confirmationCodeGenerator.GenerateCode())
                ;

            // CreateRepository in the database
            _userRepository.CreateRegistration(registrationModel);

            if (!isConfirmationRequired)
            {
                ConfirmRegistration(registrationModel.RegistrationId.GetValueOrDefault(),
                    registrationModel.Token);
            }

            return new RegistrationResult(registrationModel, _clientConfig.EnableRegistrationEmailVerification);
        }

        /// <summary>
        /// Checks if the user exists and attempts to log them in, otherwise it creates a new account
        /// </summary>
        public RegistrationOrLoginResult LoginOrRegister(RegistrationModel registrationModel, string password)
        {
            var applicationUser = GetUserByEmail(registrationModel.Email);
            if (applicationUser != null)
            {
                // Attempt to login the user
                if (_authManager.ValidatePassword(registrationModel.Email, password))
                {
                    _authManager.Login(applicationUser.Username);
                    return new RegistrationOrLoginResult(LoginResult.Success, applicationUser);
                }
                return new RegistrationOrLoginResult(LoginResult.BadUsernameOrPassword);
            }

            // Register the new user
            var result = RegisterUser(registrationModel, password, disableTwoFactorAuth: true);
            ConfirmRegistration(result.Registration.RegistrationId.GetValueOrDefault(), result.Registration.Token);

            _authManager.Login(result.Registration.Username);
            applicationUser = GetUserByEmail(registrationModel.Email);

            return new RegistrationOrLoginResult(LoginResult.Success, applicationUser);
        }

        public RegistrationConfirmationResult ConfirmRegistration(int registrationId, string token)
        {
            // Fetch the original registration
            var registrationModel = _authManager.GetRegistration(registrationId);

            if (registrationModel == null)
            {
                return RegistrationConfirmationResult.RegistrationDoesNotExist;
            }

            if (registrationModel.ExpirationDateUtc < _dateService.UtcNow)
            {
                return RegistrationConfirmationResult.RegistrationExpired;
            }

            if (registrationModel.ConfirmationAttempts >= MaxConfirmationTokenAttempts)
            {
                return RegistrationConfirmationResult.TokenExpired;
            }

            if (!registrationModel.Confirm(token))
            {
                // Save the registration that may increment the attempts
                _userRepository.UpdateRegistrationByToken(registrationModel);
                return RegistrationConfirmationResult.TokenNotCorrect;
            }

            _authManager.CreateMembership(registrationModel.Username, registrationModel.Email, registrationModel.DecryptPassword(), login: true);
            _userRepository.CreateUserProfile(registrationModel);
            _userRepository.UpdateRegistrationByToken(registrationModel);

            return RegistrationConfirmationResult.Successful;
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
            original.PreferredPaymentMethod = applicationUser.PreferredPaymentMethod;
            original.PayPalEmail = applicationUser.PayPalEmail;
            original.BankName = applicationUser.BankName;
            original.BankAccountName = applicationUser.BankAccountName;
            original.BankAccountNumber = applicationUser.BankAccountNumber;
            original.BankBsbNumber = applicationUser.BankBsbNumber;
            original.Phone = applicationUser.Phone;

            // Update
            _userRepository.UpdateUserProfile(original);
        }
    }
}