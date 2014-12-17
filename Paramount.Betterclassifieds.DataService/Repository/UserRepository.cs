namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Business;
    //using Paramount.Betterclassifieds.DataService.LinqObjects;


    public class UserRepository : IUserRepository, IMappingBehaviour
    {
        private const string BetterclassifiedsAppId = "betterclassified";
        private const string AdminAppId = "betterclassifiedadmin";

        public ApplicationUser GetUserByUsername(string username)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var application = context.aspnet_Applications.Single(a => a.LoweredApplicationName == BetterclassifiedsAppId);

                var user = context.aspnet_Users.FirstOrDefault(u =>
                    u.UserName == username &&
                    u.ApplicationId == application.ApplicationId);

                if (user == null)
                {
                    return null;
                }

                var profile = context.UserProfiles.First(p => p.UserID == user.UserId);

                return CreateApplicationUser(profile, user.UserName);
            }
        }

        private ApplicationUser CreateApplicationUser(LinqObjects.UserProfile profile, string username)
        {
            var applicationUser = this.Map<LinqObjects.UserProfile, ApplicationUser>(profile);
            applicationUser.Username = username;
            return applicationUser;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var profile = context.UserProfiles
                    .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

                if (profile == null)
                    return null;

                // Need another read to fetch the username!
                var username = context.aspnet_Users.First(u => u.UserId == profile.UserID).UserName;

                return CreateApplicationUser(profile, username);
            }
        }

        public IEnumerable<UserNetworkModel> GetUserNetworksForUserId(string userId)
        {
            using (var context = DataContextFactory.CreateClassifiedEntitiesContext())
            {
                return context.UserNetworks.Where(u => u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public void CreateUser(string email, string firstName, string lastName, string postCode, string howYouFoundUs, string phone)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                // Need to fetch the new membership user
                // This is not nice 
                // Should be addressed when moving authentication later
                // So, this is tight coupling for ID instead of more appropriately, a username!

                var member = context.aspnet_Memberships.Single(m => m.Email == email);

                context.UserProfiles.InsertOnSubmit(new LinqObjects.UserProfile
                {
                    UserID = member.UserId,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PostCode = postCode,
                    LastUpdatedDate = DateTime.UtcNow,
                    HowYouFoundUs = howYouFoundUs,
                    Phone = phone,
                    ProfileVersion = 1
                });
                context.SubmitChanges();
            }
        }

        public void CreateRegistration(RegistrationModel registrationModel)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var registrationData = this.Map<RegistrationModel, LinqObjects.Registration>(registrationModel);

                context.Registrations.InsertOnSubmit(registrationData);
                context.SubmitChanges();

                registrationModel.RegistrationId = registrationData.RegistrationId;
            }
        }

        public void UpdateRegistrationByToken(RegistrationModel registerModel)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var dataModel = context.Registrations.Single(r => r.Token == registerModel.Token);

                this.Map(registerModel, dataModel);

                context.SubmitChanges();
            }
        }

        public void UpdateUserProfile(ApplicationUser applicationUser)
        {
            using (var context = DataContextFactory.CreateMembershipContext())
            {
                var application = context.aspnet_Applications.Single(a => a.LoweredApplicationName == BetterclassifiedsAppId);

                var user = context.aspnet_Users.Single(u => u.UserName == applicationUser.Username && u.ApplicationId == application.ApplicationId);
                
                var profile = context.UserProfiles.First(p => p.UserID == user.UserId);

                this.Map(applicationUser, profile);

                context.SubmitChanges();
            }
        }

        public void CreateUserNetwork(UserNetworkModel userNetworkModel)
        {
            using (var context = DataContextFactory.CreateClassifiedEntitiesContext())
            {
                context.UserNetworks.Add(userNetworkModel);
                context.SaveChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("UserRepositoryProfile");

            // To Db
            configuration.CreateMap<RegistrationModel, LinqObjects.Registration>()
                .ForMember(member => member.Password, options => options.MapFrom(source => source.EncryptedPassword))
                .ForMember(member => member.RegistrationId, options => options.Ignore())
                .ForMember(member => member.Version, options => options.Ignore())
                ;

            configuration.CreateMap<ApplicationUser, LinqObjects.UserProfile>()
                .ForMember(member => member.Address1, options => options.MapFrom(source => source.AddressLine1))
                .ForMember(member => member.Address2, options => options.MapFrom(source => source.AddressLine2))
                .ForMember(member => member.PostCode, options => options.MapFrom(source => source.Postcode))
                ;

            // From Db
            configuration.CreateMap<LinqObjects.UserProfile, ApplicationUser>()
                .ForMember(member => member.AddressLine1, options => options.MapFrom(source => source.Address1))
                .ForMember(member => member.AddressLine2, options => options.MapFrom(source => source.Address2))
                .ForMember(member => member.Postcode, options => options.MapFrom(source => source.PostCode));

            configuration.CreateMap<LinqObjects.Registration, RegistrationModel>()
                .ForMember(member => member.EncryptedPassword, options => options.MapFrom(source => source.Password))
                ;
        }
    }
}