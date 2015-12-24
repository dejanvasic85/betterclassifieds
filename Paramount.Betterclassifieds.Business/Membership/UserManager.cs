namespace Paramount.Betterclassifieds.Business
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using Broadcast;

    public interface IUserManager
    {
        ApplicationUser GetUserByEmail(string email);
        ApplicationUser GetUserByEmailOrUsername(string emailOrUsername);
        ApplicationUser GetCurrentUser(IPrincipal principal);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId);
        void CreateUserNetwork(IPrincipal user, string email, string fullName);
        RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword, bool disableTwoFactorAuth = false);
        RegistrationOrLoginResult LoginOrRegisterUser(RegistrationModel registrationModel, string password);
        RegistrationConfirmationResult ConfirmRegistration(int registrationId, string token);
        void UpdateUserProfile(ApplicationUser applicationUser);

    }

    public class UserManager : IUserManager
    {
        private const int MaxConfirmationTokenAttempts = 5;

        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IClientConfig _clientConfig;
        private readonly IConfirmationCodeGenerator _confirmationCodeGenerator;
        private readonly IDateService _dateService;

        public UserManager(IUserRepository userRepository, IAuthManager authManager, IBroadcastManager broadcastManager, IClientConfig clientConfig, IConfirmationCodeGenerator confirmationCodeGenerator, IDateService dateService)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _broadcastManager = broadcastManager;
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

        public RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword, bool disableTwoFactorAuth = false)
        {
            registrationModel
                .GenerateUniqueUsername(_authManager.CheckUsernameExists)
                .SetPasswordFromPlaintext(plaintextPassword)
                .SetConfirmationCode(_confirmationCodeGenerator.GenerateCode());

            // Create in the database
            _userRepository.CreateRegistration(registrationModel);

            if (_clientConfig.IsTwoFactorAuthEnabled && !disableTwoFactorAuth)
            {
                // Send the two factor authorisation email
                _broadcastManager.SendEmail(new NewRegistration
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    ConfirmationCode = registrationModel.Token
                }, registrationModel.Email);
            }

            return new RegistrationResult(registrationModel, _clientConfig.IsTwoFactorAuthEnabled);
        }

        /// <summary>
        /// Checks if the user exists and attempts to log them in, otherwise it creates a new account
        /// </summary>
        public RegistrationOrLoginResult LoginOrRegisterUser(RegistrationModel registrationModel, string password)
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

            // Confirm user
            if (result.RequiresConfirmation && result.Registration != null && result.Registration.RegistrationId.HasValue)
            {
                this.ConfirmRegistration(result.Registration.RegistrationId.Value, result.Registration.Token);
            }

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

            // Update
            _userRepository.UpdateUserProfile(original);
        }
    }
}