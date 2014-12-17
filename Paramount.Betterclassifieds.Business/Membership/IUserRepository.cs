﻿using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserByEmail(string email);
        IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId); 
        void CreateUser(string email, string firstName, string lastName, string postCode, string howYouFoundUs, string phone);
        void CreateUserNetwork(UserNetworkModel userNetworkModel);
        void CreateRegistration(RegistrationModel registrationModel);
        void UpdateRegistrationByToken(RegistrationModel registerModel);
    }
}