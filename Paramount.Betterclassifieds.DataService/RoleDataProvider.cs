namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Data.SqlClient;
    using System.Web.Security;
    using ApplicationBlock.Configuration;
    using ApplicationBlock.Data;
    using Common.DataTransferObjects.MembershipService;
    using LinqObjects;
    using System.Linq;

    public class RoleDataProvider:IDataProvider 
    {
        private readonly SqlRoleProvider _roleProvider;
        private const string SourceKey = "AppUserConnection";
        private const string DefaultFunction = "/";
        private const string DefaultFunctionDescription = "Default Function";
        private readonly string _clientCode;
        private UserMembershipDataContext _context;

        private readonly string _applicationName;
        public RoleDataProvider(string clientCode, string applicationName)
        {
            _clientCode = clientCode;
            _roleProvider = new SqlRoleProvider();
            _context = new UserMembershipDataContext(ConnectionString);
            _applicationName = applicationName;

            if(!RoleExists(_applicationName))
            {
                CreateRole(_applicationName, _applicationName);
                Commit();
            }
        }

        public bool IsUserInFunction(string username, string roleName)
        {
            return _context.aspnet_UsersInFunctions.Select(
                a =>
                    a.aspnet_User.LoweredUserName == Utils.GetUsername(username.ToLower(), ClientCode) 
                    && a.aspnet_Function.aspnet_Role.LoweredRoleName == roleName.ToLower()).Count() 
                    > 0;
        }

        public string[] GetFunctionsForUseRole(string username, string roleName)
        {
            return _context.aspnet_UsersInFunctions.Where(a => a.aspnet_User.LoweredUserName == Utils.GetUsername(username.ToLower(), ClientCode) 
                                                        && a.aspnet_Function.aspnet_Role.LoweredRoleName == roleName.ToLower())
                                                        .Select(b=>b.aspnet_Function.FunctionName)
                                                        .ToArray();
        }

        public string[] GetRolesForUser(string username)
        {
            return (
                       from a in _context.aspnet_Roles
                       join role in _context.aspnet_UsersInFunctions
                           on a.RoleId equals role.aspnet_Function.RoleId
                       where role.aspnet_User.LoweredUserName == Utils.GetUsername(username.ToLower(), ClientCode)
                       select a.RoleName)
                .ToArray();
        }

        public Guid CreateApplication(string name)
        {
            var id = Guid.NewGuid();
            _context.aspnet_Applications.InsertOnSubmit(new aspnet_Application
                                                            {ApplicationName = name, ApplicationId = id, LoweredApplicationName = name.ToLower()});
            return id;
        }

        public void CreateRole(string roleName, string description)
        {
            var applicationEntity =
                _context.aspnet_Applications.Where(a => a.LoweredApplicationName == ConfigSettingReader.ApplicationName.ToLower());

            var appId = applicationEntity.Count() == 0 ? CreateApplication(ConfigSettingReader.ApplicationName) : applicationEntity.Single().ApplicationId;

            var role = new aspnet_Role
                                   {
                                       ApplicationId = appId, 
                                       RoleName = roleName, LoweredRoleName = roleName.ToLower(),
                                       Description = description,
                                       RoleId = Guid.NewGuid()
                                   };
            role.aspnet_Functions.Add(new aspnet_Function
                                          {
                                              FunctionName = DefaultFunction,
                                              Description = description,
                                              LoweredFunctionName = DefaultFunction.ToLower(),
                                              FunctionId = Guid.NewGuid()
                                          });
            _context.aspnet_Roles.InsertOnSubmit(role);
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public bool FunctionExists(string functionName)
        {
            return _context.aspnet_Functions
                .Where( a => a.LoweredFunctionName == functionName.ToLower() 
                    && a.aspnet_Role.LoweredRoleName == _applicationName.ToLower())
                    .Count() > 0;
        }

        public bool RoleExists(string roleName)
        {
            var roles= _context.aspnet_Roles.Where( a=>a.LoweredRoleName == roleName.ToLower());
            return roles.Count() > 0;
        }

        public void AddUserToFunction(string username, string functionName)
        {
            _context.aspnet_UsersInFunctions.InsertOnSubmit(new aspnet_UsersInFunction
                                                                {
                                                                    FunctionId  = (_context.aspnet_Functions.Where(a=> a.LoweredFunctionName == functionName.ToLower()).Single().FunctionId),
                                                                    UserId = _context.aspnet_Users.Where(a => a.LoweredUserName == Utils.GetUsername(username.ToLower(), ClientCode)).Single().UserId
                                                                });
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public string[] GetFunctionsInRole(string roleName)
        {
            return _context.aspnet_Functions.Where( a=> a.aspnet_Role.LoweredRoleName == roleName.ToLower()).Select(b=> b.FunctionName).ToArray();
        }

        public string[] GetAllRoles()
        {
            return _context.aspnet_Roles.Select( a=> a.RoleName).ToArray();
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfileInfo(string username, ProfileInfo profileInfo)
        {
            if(profileInfo == null)
            {
                return;
            }
            var user = (from a in  _context.aspnet_Users where a.LoweredUserName  == Utils.GetUsername(username, ClientCode).ToLower() select a).First();

            var currentProfile = from a in _context.UserProfiles where a.UserID == user.UserId select a;
            UserProfile profile;
            if(currentProfile.Count() > 0)
            {
                profile = currentProfile.First();

                profile.ABN = profileInfo.Abn;
                profile.Address1 = profileInfo.Address1;
                profile.Address2 = profileInfo.Address2;
                profile.BusinessCategory = profileInfo.AccountCategory;
                profile.BusinessName = profileInfo.BusinessName;
                profile.City = profileInfo.City;
                profile.Email = profileInfo.SecondaryEmail;
                profile.FirstName = profileInfo.FirstName;
                profile.LastName = profileInfo.LastName;
                profile.Industry = profileInfo.Industry;
                profile.LastUpdatedDate = DateTime.Now;
                profile.NewsletterSubscription = profileInfo.NewsletterSubscription;
                profile.Phone = profileInfo.Phone;
                profile.PostCode = profileInfo.Postcode;
                profile.RefNumber = profileInfo.AccountId;
                profile.SecondaryPhone = profile.SecondaryPhone;
                profile.State = profileInfo.State;
            }
            else
            {
                profile = new UserProfile
                {
                    ABN = profileInfo.Abn,
                    Address1 = profileInfo.Address1,
                    Address2 = profileInfo.Address2,
                    BusinessCategory = profileInfo.AccountCategory,
                    BusinessName = profileInfo.BusinessName,
                    City = profileInfo.City,
                    Email = profileInfo.SecondaryEmail,
                    FirstName = profileInfo.FirstName,
                    LastName = profileInfo.LastName,
                    Industry = profileInfo.Industry,
                    LastUpdatedDate = DateTime.Now,
                    NewsletterSubscription = profileInfo.NewsletterSubscription,
                    Phone = profileInfo.Phone,
                    PostCode = profileInfo.Postcode,
                    RefNumber = profileInfo.AccountId,
                    UserID = user.UserId 
                };
                profile.SecondaryPhone = profile.SecondaryPhone;
                profile.State = profileInfo.State;
                _context.UserProfiles.InsertOnSubmit(profile);
            }

           
        }

        public ProfileInfo GetProfile(string username)
        {
            var user = _context.aspnet_Users.Where(a => a.LoweredUserName == Utils.GetUsername(username, ClientCode).ToLower()).First();
            var currentProfile = from a in _context.UserProfiles where a.UserID == user.UserId select a;
            if(currentProfile.Count() == 0)
            {
                var newProfile = new ProfileInfo {NewsletterSubscription = true, FirstName = string.Empty , LastName = string.Empty, SecondaryEmail = string.Empty }; 
                UpdateProfileInfo(user.GetUsername(), newProfile);
                Commit();
                return newProfile;
            }
            return currentProfile.First().ConvertProfile();
        }
        

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); ; }
        }

        public string ClientCode
        {
            get { return _clientCode; }
        }

        public string ConfigSection
        {
            get { return "paramount/services"; }
        }


    }
}