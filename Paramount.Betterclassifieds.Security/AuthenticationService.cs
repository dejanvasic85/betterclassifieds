﻿using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.Security
{
    public class AuthenticationService : IAuthManager, IMappingBehaviour
    {
        private readonly IApplicationConfig _applicationConfig;
        private const string ForceSSLCookieName = "ClassifiedsSSL";

        public AuthenticationService(IApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public void Login(string username, bool createPersistentCookie, string role = "User")
        {
            HttpContext context = HttpContext.Current;

            // Login via Forms Auth
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: username,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddYears(100),
                isPersistent: createPersistentCookie,
                userData: role,
                cookiePath: FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = _applicationConfig.UseHttps
            };

            context.Response.Cookies.Add(cookie);

            if (_applicationConfig.UseHttps)
            {
                // Drop a second cookie indicating that the user is logged in via SSL (no secret data, just tells us to redirect them to SSL)
                context.Response.Cookies.Add(new HttpCookie(ForceSSLCookieName, "true"));
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();

            // Delete the "LoggedIn" cookie
            HttpContext context = HttpContext.Current;
            var cookie = context.Request.Cookies[ForceSSLCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1d);
                context.Response.Cookies.Add(cookie);
            }
        }

        public bool ValidatePassword(string username, string password)
        {
            // Use the existing built-in membership provider for now
            return Membership.ValidateUser(username, password);
        }

        public void CreateMembership(string username, string email, string password)
        {
            Membership.CreateUser(username, password, email);
        }

        public void CreateMembershipFromRegistration(RegistrationModel registerModel)
        {
            CreateMembership(registerModel.Username, registerModel.Email, registerModel.DecryptPassword());

            registerModel.ConfirmationDate = DateTime.Now;
            registerModel.ConfirmationDateUtc = DateTime.UtcNow;

            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var registrationData = this.Map<RegistrationModel, DataService.LinqObjects.Registration>(registerModel);

                context.Registrations.Attach(registrationData, true);
                context.SubmitChanges();
            }
        }

        public bool CheckUsernameExists(string username)
        {
            return Membership.GetUser(username) != null;
        }

        public bool CheckEmailExists(string email)
        {
            return Membership.GetUserNameByEmail(email) != null;
        }

        public int CreateRegistration(RegistrationModel registrationModel)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var registrationData = this.Map<RegistrationModel, DataService.LinqObjects.Registration>(registrationModel);

                context.Registrations.InsertOnSubmit(registrationData);
                context.SubmitChanges();

                registrationModel.RegistrationId = registrationData.RegistrationId;
                return registrationModel.RegistrationId;
            }
        }

        public RegistrationModel GetRegistration(int registrationId, string token, string username)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var registrationData = context.Registrations.FirstOrDefault(r =>
                    r.RegistrationId == registrationId &&
                    r.Token == token &&
                    r.Username == username);

                return this.Map<DataService.LinqObjects.Registration, RegistrationModel>(registrationData);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("AuthServiceMappings");

            // From Model to database
            configuration.CreateMap<RegistrationModel, DataService.LinqObjects.Registration>()
                .ForMember(member => member.Password, options => options.MapFrom(source => source.EncryptedPassword));

            // From database to Model
            configuration.CreateMap<DataService.LinqObjects.Registration, RegistrationModel>()
                .ForMember(member => member.EncryptedPassword, options => options.MapFrom(source => source.Password));
        }
    }
}
