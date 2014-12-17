using System;
using System.Security.Principal;

namespace Paramount.Betterclassifieds.Business
{
    public interface IAuthManager
    {
        bool IsUserIdentityLoggedIn(IPrincipal user);
        void Login(string username, bool createPersistentCookie, string role = "User");
        void Logout();
        bool ValidatePassword(string username, string password);
        void CreateMembership(string username, string email, string password);
        bool CheckUsernameExists(string username);
        bool CheckEmailExists(string email);
        RegistrationModel GetRegistration(int registrationId, string token, string username);
        string SetRandomPassword(string email);
    }
}