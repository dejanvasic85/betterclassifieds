using System;
using System.Linq;
using System.Transactions;
using System.Web.Security;
using Dapper;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public void DropUserIfExists(string username, string email)
        {
            // Drop from user table
            using (var db = _connectionFactory.CreateMembership())
            using (var scope = new TransactionScope())
            {
                var userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).SingleOrDefault();

                if (!userId.HasValue)
                {
                    // Also try to query the aspnet_Membership table (in case data got corrupt)
                    userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Membership WHERE Email = @email", new { email }).SingleOrDefault();
                }

                if (userId.HasValue)
                {
                    Console.WriteLine("Dropping User " + userId);
                    var param = new { userId };

                    db.Execute("DELETE FROM aspnet_Membership WHERE UserId = @userId", param);
                    db.Execute("DELETE FROM aspnet_Users WHERE UserId = @userId", param);
                    db.Execute("DELETE FROM UserProfile WHERE UserID = @userId", param);
                }
                else
                {
                    Console.WriteLine("User " + email + " not found.");
                }

                var registrations = db.Query<int?>("SELECT RegistrationId from [Registration] where Email = @email", new { email }).ToList();
                foreach (var registrationId in registrations)
                {
                    db.Execute("DELETE FROM Registration WHERE RegistrationId = @registrationId", new { registrationId });
                }

                scope.Complete();
            }
        }

        public bool RegistrationExistsForEmail(string email)
        {
            using (var db = _connectionFactory.CreateMembership())
            {
                return db.Query("SELECT Username FROM Registration WHERE Email = @email", new { email }).Any();
            }
        }

        public Guid? DropCreateUser(string username, string password, string email, RoleType roleType)
        {
            DropUserIfExists(username, email);

            using (var db = _connectionFactory.CreateMembership())
            using (var scope = new TransactionScope())
            {
                var membershipProvider = Membership.Providers[RoleProviderDictionary[roleType]];
                if (membershipProvider == null)
                    throw new NullReferenceException();

                //var applicationName = membershipProvider.ApplicationName;
                //var applicationId = db.Query<Guid?>("SELECT ApplicationId FROM aspnet_Applications WHERE ApplicationName = @applicationName", new { applicationName }).FirstOrDefault();

                //if (!applicationId.HasValue)
                //    throw new Exception("The membership is not setup and application Id is NULL");

                MembershipCreateStatus createStatus;
                membershipProvider.CreateUser(username, password, email, null, null, true, Guid.NewGuid(), out createStatus);

                var userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).First();

                if (createStatus != MembershipCreateStatus.Success)
                    throw new MembershipCreateUserException("unable to create the user. Status is " + createStatus);

                string sql = string.Format(
                    "INSERT INTO {0} (UserID, FirstName, LastName, Email, PostCode, ProfileVersion, LastUpdatedDate) VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    Constants.MembershipTable.UserProfile, userId, username, username, email, 1000, 1, DateTime.Now);

                db.Execute(sql);
                scope.Complete();
                return userId;
            }
        }

        public void DropUserNetwork(string userId, string userNetworkEmail)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                db.Execute("DELETE FROM UserNetwork WHERE UserId = @userId and UserNetworkEmail = @userNetworkEmail",
                    new { userId, userNetworkEmail });
            }
        }

        public int AddUserNetworkIfNotExists(string username, string email, string fullName)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                var results = db.Query<int?>("SELECT UserNetworkId FROM UserNetwork WHERE UserId = @username AND UserNetworkEmail = @email",
                    new { username, email }).SingleOrDefault();

                if (results != null)
                    return results.Value;

                return db.Add(Constants.Table.UserNetwork,
                    new
                    {
                        UserId = username,
                        UserNetworkEmail = email,
                        FullName = fullName,
                        LastModifiedDate = DateTime.Now,
                        LastModifiedDateUtc = DateTime.UtcNow,
                        IsUserNetworkActive = true,
                    });
            }
        }
    }
}
