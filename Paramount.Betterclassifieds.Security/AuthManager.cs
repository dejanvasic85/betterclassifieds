﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.Security
{
    public class AuthManager : IAuthManager, IMappingBehaviour
    {
        private readonly IApplicationConfig _applicationConfig;
        private readonly IDbContextFactory _dbContextFactory;

        private const string ForceSSLCookieName = "ClassifiedsSSL";

        public AuthManager(IApplicationConfig applicationConfig, IDbContextFactory dbContextFactory)
        {
            _applicationConfig = applicationConfig;
            _dbContextFactory = dbContextFactory;
        }

        public bool IsUserIdentityLoggedIn(IPrincipal user)
        {
            return user != null && user.Identity.IsAuthenticated;
        }

        public void Login(string username, bool createPersistentCookie = false, string role = "User")
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

        public void CreateMembership(string username, string email, string password, bool login = false)
        {
            Membership.CreateUser(username, password, email);

            if (login)
            {
                Login(username);
            }
        }

        public bool CheckUsernameExists(string username)
        {
            using (var context = _dbContextFactory.CreateMembershipContext())
            {
                return context.aspnet_Users.Any(u => u.LoweredUserName.Equals(username.ToLower()));
            }
        }

        public bool CheckEmailExists(string email)
        {
            using (var context = _dbContextFactory.CreateMembershipContext())
            {
                return context.aspnet_Memberships.Any(m => m.LoweredEmail.Equals(email.ToLower()));
            }
        }

        public RegistrationModel GetRegistration(int registrationId, string token, string username)
        {
            return QueryRegistration(r =>
                    r.RegistrationId == registrationId &&
                    r.Token == token &&
                    r.Username == username).FirstOrDefault();
        }

        public RegistrationModel GetRegistration(int registrationId)
        {
            return QueryRegistration(r => r.RegistrationId == registrationId).FirstOrDefault();
        }

        private IEnumerable<RegistrationModel> QueryRegistration(Expression<Func<DataService.LinqObjects.Registration, bool>> expression)
        {
            using (var context = _dbContextFactory.CreateMembershipContext())
            {
                var registrationData = context.Registrations.Where(expression).ToList();

                return this.MapList<DataService.LinqObjects.Registration, RegistrationModel>(registrationData);
            }
        }

        public string SetRandomPassword(string email)
        {
            // Use the membership provider
            var password = Membership.GeneratePassword(length: 6, numberOfNonAlphanumericCharacters: 2);

            var username = Membership.GetUserNameByEmail(email);

            ChangePassword(username, password);

            return password;
        }

        public void SetPassword(string username, string newPassword)
        {
            ChangePassword(username, newPassword);
        }

        private void ChangePassword(string username, string newPassword)
        {
            var user = Membership.GetUser(username);

            if (user == null)
                throw new NullReferenceException($"Unable to change password for {username}. The user does not exist.");

            if (user.IsLockedOut)
                user.UnlockUser();

            Guard.NotNull(user);

            user.ChangePassword(user.GetPassword(), newPassword);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("AuthServiceMappings");

            // From Model to database
            configuration.CreateMap<RegistrationModel, DataService.LinqObjects.Registration>()
                .ForMember(member => member.Password, options => options.MapFrom(source => source.EncryptedPassword));

            // From database to Model
            configuration.CreateMap<DataService.LinqObjects.Registration, RegistrationModel>()
                .ForMember(member => member.EncryptedPassword, options => options.MapFrom(source => source.Password))
                ;
        }
    }
}
