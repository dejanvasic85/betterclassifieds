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

        public void AddPublicationIfNotExists(string publicationName)
        {
            using (var scope = new TransactionScope())
            {
                var publication = new
                    {
                        Title = publicationName,
                        Description = "Selenium Paper",
                        PublicationTypeId = 1
                    };

                var publicationId = classifiedDb.GetIdForTable("Publication", publicationName);
                if (publicationId.HasValue)
                    return;

                // Create new publication
                classifiedDb.InsertIntoTable("Publication", new { });


                scope.Complete();
            }
        }



        public int DropAndAddOnlineAd(string adTitle, string categoryName, string subCategoryName)
        {
            DropOnlineAdIfExists(adTitle);

            using (var scope = new TransactionScope())
            {
                var adId = classifiedDb.InsertIntoTable("Ad", new { title = adTitle });

                var mainCategoryId = GetCategoryIdForTitle(categoryName);

                int? bookingId = classifiedDb.InsertIntoTable("AdBooking", new
                    {
                        @StartDate = DateTime.Now.AddDays(-1),
                        @EndDate = DateTime.Now.AddDays(30),
                        @TotalPrice = 0,
                        @BookReference = "SEL-001",
                        @AdId = adId,
                        @UserId = "bdduser",
                        @BookingStatus = 1,
                        @MainCategoryId = mainCategoryId,
                        @BookingDate = DateTime.Now,
                        @Insertions = 1
                    });

                var adDesignId = classifiedDb.InsertIntoTable("AdDesign", new { adId, @adTypeId = 2 });

                var onlineAdid = classifiedDb.InsertIntoTable("OnlineAd", new
                    {
                        @AdDesignId = adDesignId,
                        @Heading = adTitle,
                        @Description = adTitle,
                        @HtmlText = adTitle,
                        @NumOfViews = 100,
                        @Price = 1500,
                        @ContactName = "Sample Contact"
                    });

                // Commit transaction
                scope.Complete();

                return bookingId.Value;
            }
        }

        public void DropOnlineAdIfExists(string adTitle)
        {
            // Fetch the AdDesign

            using (var scope = new TransactionScope())
            {
                var adDesignId =
                    classifiedDb.Query<int?>(
                        "SELECT ds.AdDesignId FROM AdDesign ds JOIN OnlineAd o ON o.AdDesignId = ds.AdDesignId AND o.Heading = @title",
                        new { @title = adTitle }).FirstOrDefault();

                if (!adDesignId.HasValue)
                    return;

                var adId =
                    classifiedDb.Query<int?>(
                        "SELECT a.AdId FROM Ad a JOIN AdDesign ds ON ds.AdId = a.AdId WHERE ds.AdDesignId = @adDesignId",
                        new { adDesignId }).FirstOrDefault();

                // Let's drop everything ! Starting from the online ad
                classifiedDb.ExecuteSql("DELETE from OnlineAd WHERE AdDesignId = @adDesignId", new { adDesignId });
                classifiedDb.ExecuteSql("DELETE from AdDesign WHERE AdDesignId = @adDesignId", new { adDesignId });

                if (adId.HasValue)
                {
                    classifiedDb.ExecuteSql("DELETE FROM AdBooking WHERE AdId = @adId", new { adId });
                    classifiedDb.ExecuteSql("DELETE FROM Ad WHERE AdId = @adId", new { adId });
                }

                scope.Complete();
            }
        }

        public void AddAdTypeIfNotExists(string lineAdCode)
        {
            classifiedDb.AddIfNotExists("AdType", new
                {
                    Code = lineAdCode,
                    Title = "Line Ad",
                    Description = "Line Ad",
                    PaperBased = true,
                    Active = true
                }, findBy: lineAdCode, findByColumnName: "Code");
        }

        public ITestDataManager DropUserIfExists(string username)
        {
            // Drop from user table
            using (var scope = new TransactionScope())
            {
                var userId =
                    membershipDb.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username",
                                              new { username }).FirstOrDefault();
                if (userId.HasValue)
                {
                    membershipDb.Execute("DELETE FROM aspnet_Membership WHERE UserId = @userId", new { userId });
                    membershipDb.Execute("DELETE FROM aspnet_Users WHERE UserId = @userId", new { userId });
                    membershipDb.Execute("DELETE FROM UserProfile WHERE UserID = @userId", new { userId });
                }
                scope.Complete();
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

        public int AddCategoryIfNotExists(string name, string parent)
        {
            using (var scope = new TransactionScope())
            {
                // Ensure that parent exists
                var parentCategoryId = GetCategoryIdForTitle(parent);
                if (!parentCategoryId.HasValue)
                {
                    parentCategoryId = classifiedDb.InsertIntoTable("MainCategory", new { Title = parent });
                }

                // Create sub category
                var categoryId = GetCategoryIdForTitle(name);
                if (!categoryId.HasValue)
                {
                    classifiedDb.InsertIntoTable("MainCategory", new { Title = name, ParentId = parentCategoryId });
                }
                scope.Complete();
                return categoryId.GetValueOrDefault();
            }
        }

        public int? GetCategoryIdForTitle(string categoryName)
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
