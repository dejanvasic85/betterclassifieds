using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class UserHttpManager : IUserManager
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserManager _userManager;

        public UserHttpManager(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
            _userManager = DependencyResolver.Current.GetService<UserManager>();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _userManager.GetUserByEmail(email);
        }

        public ApplicationUser GetUserByEmailOrUsername(string emailOrUsername)
        {
            return _userManager.GetUserByEmailOrUsername(emailOrUsername);
        }

        public ApplicationUser GetCurrentUser()
        {
            return GetCurrentUser(_httpContext.User);
        }

        public ApplicationUser GetCurrentUser(IPrincipal principal)
        {
            return _userManager.GetCurrentUser(principal);
        }

        public IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId)
        {
            return _userManager.GetUserNetworksForUserId(userId);
        }

        public UserNetworkModel GetUserNetwork(int userNetworkId)
        {
            return _userManager.GetUserNetwork(userNetworkId);
        }

        public UserNetworkModel CreateUserNetwork(string userId, string email, string fullName)
        {
            return _userManager.CreateUserNetwork(userId, email, fullName);
        }

        public RegistrationResult RegisterUser(RegistrationModel registrationModel, string plaintextPassword,
            bool disableTwoFactorAuth = false)
        {
            return _userManager.RegisterUser(registrationModel, plaintextPassword, disableTwoFactorAuth);
        }

        public RegistrationOrLoginResult LoginOrRegister(RegistrationModel registrationModel, string password)
        {
            return _userManager.LoginOrRegister(registrationModel, password);
        }

        public RegistrationConfirmationResult ConfirmRegistration(int registrationId, string token)
        {
            return _userManager.ConfirmRegistration(registrationId, token);
        }

        public void UpdateUserProfile(ApplicationUser applicationUser)
        {
            _userManager.UpdateUserProfile(applicationUser);
        }
    }
}