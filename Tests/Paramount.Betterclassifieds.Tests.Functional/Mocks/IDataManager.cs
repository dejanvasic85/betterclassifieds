using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataManager : IDisposable
    {
        // Categories
        ITestDataManager AddOrUpdateCategory(string name, string parent);

        // Ads
        ITestDataManager AddOrUpdateOnlineAd(string adTitle, out int? id);

        // Users
        ITestDataManager DropUserIfExists(string username);
        bool UserExists(string username);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);
    }

    public class DapperDataManager : ITestDataManager
    {
        // Create IDbConnections
        private readonly IDbConnection classifiedDb;
        private readonly IDbConnection coreDb;
        private readonly IDbConnection membershipDb;

        public DapperDataManager()
        {
            classifiedDb = new SqlConnection(ConfigurationManager.ConnectionStrings["ClassifiedsDb"].ConnectionString);
            coreDb = new SqlConnection(ConfigurationManager.ConnectionStrings["MembershipDb"].ConnectionString);
            membershipDb = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDb"].ConnectionString);
        }

        public ITestDataManager AddOrUpdateOnlineAd(string adTitle, out int? id)
        {
            id = null;
            return this;
        }

        public ITestDataManager DropUserIfExists(string username)
        {
            // Drop from user table
            // membershipDb.Execute("DELETE FROM ")

            // Drop from Membership table

            // Drop from UserProfile table

            return this;
        }

        public bool UserExists(string username)
        {
            return false;
        }

        public List<Email> GetSentEmailsFor(string email)
        {
            throw new System.NotImplementedException();
        }

        public ITestDataManager AddOrUpdateCategory(string name, string parent)
        {
            // Ensure that parent exists
            classifiedDb.Open();
            var parentCategoryId = classifiedDb.Query<int>("SELECT MainCategoryId FROM MainCategory WHERE Title = @Title", new { Title = parent }).FirstOrDefault();
            if (parentCategoryId == 0)
            {
                classifiedDb.Execute("INSERT INTO MainCategory (Title) VALUES (@Title)", new { Title = parent });
                SetIdentity<int>(classifiedDb, id => parentCategoryId = id);
            }

            // Create sub category


            return this;
        }

        protected static void SetIdentity<T>(IDbConnection connection, Action<T> setId)
        {
            var identity = connection.Query("SELECT @@IDENTITY AS Id").Single();
            T newId = (T)identity.Id;
            setId(newId);
        }

        public void Dispose()
        {
            classifiedDb.Close();
            membershipDb.Close();
            coreDb.Close();
        }
    }
}
