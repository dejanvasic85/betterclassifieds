﻿namespace Paramount.Betterclassifieds.Business
{
    public class ApplicationUser
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }

        public virtual bool AuthenticateUser(IAuthManager authManager, string password, bool persistAuthCookie = true)
        {
            if (!authManager.ValidatePassword(Username, password))
                return false;

            authManager.Login(Username, persistAuthCookie);

            return true;
        }
    }
}