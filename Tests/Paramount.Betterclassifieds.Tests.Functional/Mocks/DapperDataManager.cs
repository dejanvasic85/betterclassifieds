using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public class DapperDataManager : ITestDataManager
    {
        // Create IDbConnections
        private readonly IDbConnection classifiedDb;
        private readonly IDbConnection coreDb;
        private readonly IDbConnection membershipDb;

        public DapperDataManager()
        {
            classifiedDb = new SqlConnection(ConfigurationManager.ConnectionStrings["ClassifiedsDb"].ConnectionString);
            coreDb = new SqlConnection(ConfigurationManager.ConnectionStrings["CoreDb"].ConnectionString);
            membershipDb = new SqlConnection(ConfigurationManager.ConnectionStrings["MembershipDb"].ConnectionString);
        }

        public int AddOrUpdateOnlineAd(string adTitle, string categoryName)
        {
            var bookingId = GetBookingByOnlineTitle(adTitle);

            if (bookingId.HasValue)
                return bookingId.Value;

            using (var scope = new TransactionScope())
            {
                var adId = classifiedDb.InsertTable("Ad", new { adTitle });

                var mainCategoryId = GetCategoryIdForTitle(categoryName);

                bookingId = classifiedDb.InsertTable("AdBooking", new { @StartDate = DateTime.Now.AddDays(-1), @EndDate = DateTime.Now.AddDays(30), @TotalPrice = 0, @BookingReference = "SEL-001", adId, username = "bdduser", @BookingStatus = 1, mainCategoryId, @BookingDate = DateTime.Now, @Insertions = 1 });

                var adDesignId = classifiedDb.InsertTable("AdDesign", new { adId, @adTypeId = 2 });

                var onlineAdid = classifiedDb.InsertTable("OnlineAd", new { adDesignId, adTitle, @description = adTitle, @htmlText = adTitle, @numOfViews = 100 });

                // Commit transaction
                scope.Complete();

                return bookingId.Value;
            }
        }

        private int? GetBookingByOnlineTitle(string adTitle)
        {
            return classifiedDb.Query<int?>(
                "SELECT  bk.AdBookingId FROM AdBooking bk JOIN " +
                "AdDesign ds ON ds.AdId = bk.AdId JOIN " +
                "OnlineAd o ON o.AdDesignId = ds.AdDesignId AND o.Heading = @title", new { @title = adTitle })
                .FirstOrDefault();
        }

        public ITestDataManager DropUserIfExists(string username)
        {
            // Drop from user table
            var userId = membershipDb.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).FirstOrDefault();
            if (userId.HasValue)
            {
                membershipDb.Execute("DELETE FROM aspnet_Membership WHERE UserId = @userId", new { userId });
                membershipDb.Execute("DELETE FROM aspnet_Users WHERE UserId = @userId", new { userId });
                membershipDb.Execute("DELETE FROM UserProfile WHERE UserID = @userId", new { userId });
            }

            return this;
        }

        public bool UserExists(string username)
        {
            return membershipDb.Query("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).Any();
        }

        public List<Email> GetSentEmailsFor(string email)
        {
            return coreDb.Query<Email>("SELECT Subject, CreateDateTime FROM EmailBroadcastEntry WHERE Email = @email", new { email }).ToList();
        }

        public int AddOrUpdateCategory(string name, string parent)
        {
            using (var scope = new TransactionScope())
            {
                // Ensure that parent exists
                var parentCategoryId = GetCategoryIdForTitle(parent);
                if (!parentCategoryId.HasValue)
                {
                    parentCategoryId = classifiedDb.InsertTable("MainCategory", new { Title = parent });
                }

                // Create sub category
                var categoryId = GetCategoryIdForTitle(name);
                if (!categoryId.HasValue)
                {
                    classifiedDb.InsertTable("MainCategory", new { Title = name, ParentId = parentCategoryId });
                }
                scope.Complete();
                return categoryId.GetValueOrDefault();
            }
        }

        private int? GetCategoryIdForTitle(string categoryName)
        {
            return classifiedDb.Query<int?>(
                "SELECT MainCategoryId FROM MainCategory WHERE Title = @Title", new { Title = categoryName })
                .FirstOrDefault();
        }

        public void Dispose()
        {
            classifiedDb.Close();
            membershipDb.Close();
            coreDb.Close();
        }
    }
}
