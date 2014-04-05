﻿namespace Paramount.Betterclassifieds.Business
{
    public interface IAuthManager
    {
        void Login(string username, bool createPersistentCookie, string role = "User");
        void Logout();
        bool ValidatePassword(string username, string password);
        void CreateMembership(string username, string password);
        bool CheckUsernameExists(string username);
        bool CheckEmailExists(string email);
        int CreateRegistration(RegistrationModel registrationModel);
    }
}