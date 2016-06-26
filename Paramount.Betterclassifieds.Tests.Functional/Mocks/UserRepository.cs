﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Security;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public void DropUserIfExists(string username)
        {
            // Drop from user table
            using (var db = _connectionFactory.CreateMembership())
            using (var scope = new TransactionScope())
            {
                var userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).FirstOrDefault();
                if (userId.HasValue)
                {
                    db.Execute("DELETE FROM aspnet_Membership WHERE UserId = @userId", new { userId });
                    db.Execute("DELETE FROM aspnet_Users WHERE UserId = @userId", new { userId });
                    db.Execute("DELETE FROM UserProfile WHERE UserID = @userId", new { userId });
                }

                db.Execute("DELETE FROM Registration WHERE Email = @username", new { username });
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

        public Guid? AddUserIfNotExists(string username, string password, string email, RoleType roleType)
        {
            using (var db = _connectionFactory.CreateMembership())
            using (var scope = new TransactionScope())
            {
                var membershipProvider = Membership.Providers[RoleProviderDictionary[roleType]];
                if (membershipProvider == null)
                    throw new NullReferenceException();

                var applicationName = membershipProvider.ApplicationName;
                var applicationId = db.Query<Guid?>("SELECT ApplicationId FROM aspnet_Applications WHERE ApplicationName = @applicationName", new { applicationName }).FirstOrDefault();
                Guid? userId;
                if (applicationId.HasValue)
                {
                    userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username AND ApplicationId = @applicationId", new { username, applicationId }).FirstOrDefault();

                    if (userId.HasValue)
                        return userId;
                }

                MembershipCreateStatus createStatus;
                membershipProvider.CreateUser(username, password, username, null, null, true, Guid.NewGuid(), out createStatus);

                userId = db.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).First();

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