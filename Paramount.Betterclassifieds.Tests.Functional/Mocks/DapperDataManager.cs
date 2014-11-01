using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web.Security;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public class DapperDataRepository : ITestDataRepository
    {
        // Create IDbConnections
        private readonly IDbConnection classifiedDb;
        private readonly IDbConnection broadcastDb;
        private readonly IDbConnection membershipDb;

        // Used for membership database
        private Dictionary<RoleType, string> RoleProviderDictionary = new Dictionary<RoleType, string>
        {
            { RoleType.Administrator, "UserAdminMembershipProvider"},
            { RoleType.Advertiser, "AppUserProvider"}
        };

        public DapperDataRepository()
        {
            // Connections
            classifiedDb = new SqlConnection(ConfigurationManager.ConnectionStrings["ClassifiedsDb"].ConnectionString);
            broadcastDb = new SqlConnection(ConfigurationManager.ConnectionStrings["BroadcastDb"].ConnectionString);
            membershipDb = new SqlConnection(ConfigurationManager.ConnectionStrings["AppUserConnection"].ConnectionString);
        }

        public int AddPublicationIfNotExists(string publicationName, string publicationType = Constants.PublicationType.Newspaper, string frequency = Constants.FrequencyType.Weekly, int? frequencyValue = 3)
        {
            using (var scope = new TransactionScope())
            {
                var publicationTypeId = classifiedDb.Single(Constants.Table.PublicationType, publicationType, findBy: "Code");

                var publication = new
                {
                    Title = publicationName,
                    Description = "Selenium Paper",
                    PublicationTypeId = publicationTypeId,
                    FrequencyType = frequency,
                    FrequencyValue = frequencyValue,
                    Active = true
                };

                var publicationId = classifiedDb.AddIfNotExists(Constants.Table.Publication, publication, filterByValue: publicationName);

                scope.Complete();

                return publicationId.GetValueOrDefault();
            }
        }

        public int AddPublicationAdTypeIfNotExists(string publicationName, string adTypeCode)
        {
            var publicationId = classifiedDb.SingleOrDefault(Constants.Table.Publication, publicationName);
            if (!publicationId.HasValue)
                throw new ArgumentNullException("publicationName", "PublicationName " + publicationName + " does not exist and cannot create publication ad type");

            var adTypeId = classifiedDb.SingleOrDefault(Constants.Table.AdType, adTypeCode, findByColumnName: "Code");
            if (!adTypeId.HasValue)
                throw new ArgumentNullException("adTypeCode", "AdType " + adTypeCode + " does not exist and cannot create publication ad type");

            var publicationAdTypeId = classifiedDb.Query<int?>("SELECT PublicationAdTypeId FROM PublicationAdType WHERE AdTypeId = @adTypeId AND PublicationId = @publicationId",
                new { adTypeId, publicationId })
                .FirstOrDefault();

            if (publicationAdTypeId.HasValue)
                return publicationAdTypeId.Value;

            publicationAdTypeId = classifiedDb.Add<int>(Constants.Table.PublicationAdType, new { PublicationId = publicationId, AdTypeId = adTypeId });
            return publicationAdTypeId.GetValueOrDefault();
        }

        public void AddEditionsToPublication(string publicationName, int numberOfEditions)
        {
            var nextEdition = DateTimeHelper.DateForNext(DayOfWeek.Wednesday);

            using (var scope = new TransactionScope())
            {
                var publicationId = classifiedDb.SingleOrDefault(Constants.Table.Publication, publicationName);

                // Create a whole bunch of editions
                for (int i = 0; i < numberOfEditions; i++)
                {
                    var editionDate = nextEdition.AddDays(i * 7);

                    // Find by multiple criteria
                    var editionId = classifiedDb
                        .Query<int?>(
                            " SELECT EditionId FROM " + Constants.Table.Edition +
                            " WHERE PublicationId = @publicationId AND EditionDate = @editionDate", new { publicationId, editionDate })
                        .FirstOrDefault();

                    if (editionId.HasValue)
                        continue;

                    // Create the edition
                    classifiedDb.Add(Constants.Table.Edition, new { publicationId, editionDate, deadline = editionDate.AddHours(-18), Active = true });
                }

                scope.Complete();
            }
        }

        public int DropCreateOnlineAd(string adTitle, string categoryName, string subCategoryName)
        {
            DropOnlineAdIfExists(adTitle);

            return AddOnlineAd(adTitle, subCategoryName);
        }

        public int AddOnlineAd(string adTitle, string categoryName)
        {
            using (var scope = new TransactionScope())
            {
                var adId = classifiedDb.Add(Constants.Table.Ad, new { title = adTitle });

                var mainCategoryId = GetCategoryIdForTitle(categoryName);

                int? bookingId = classifiedDb.Add(Constants.Table.AdBooking, new
                {
                    @StartDate = DateTime.Now.AddDays(-1).Date,
                    @EndDate = DateTime.Now.AddDays(30).Date,
                    @TotalPrice = 0,
                    @BookReference = "SEL-001",
                    @AdId = adId,
                    @UserId = "bdduser",
                    @BookingStatus = 1,
                    @MainCategoryId = mainCategoryId,
                    @BookingDate = DateTime.Now,
                    @Insertions = 1,
                    @BookingType = "Regular"
                });

                var adDesignId = classifiedDb.Add(Constants.Table.AdDesign, new { adId, @adTypeId = 2 });

                var locationId = classifiedDb.SingleOrDefault(Constants.Table.Location, TestData.Location_Australia);
                var areaId = classifiedDb.SingleOrDefault(Constants.Table.LocationArea, TestData.Location_Victoria);

                var onlineAdid = classifiedDb.Add(Constants.Table.OnlineAd, new
                {
                    @AdDesignId = adDesignId,
                    @Heading = adTitle,
                    @Description = adTitle,
                    @HtmlText = adTitle,
                    @NumOfViews = 100,
                    @Price = 1500,
                    @ContactName = "Sample Contact",
                    @ContactPhone = "111 222 333",
                    @ContactEmail = "sample@fake.com",
                    @LocationId = locationId,
                    @LocationAreaId = areaId
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
                var adDesignId = classifiedDb.Query<int?>(
                        "SELECT ds.AdDesignId FROM AdDesign ds JOIN OnlineAd o ON o.AdDesignId = ds.AdDesignId AND o.Heading = @title", new { @title = adTitle }).FirstOrDefault();

                if (!adDesignId.HasValue)
                    return;

                var adId =
                    classifiedDb.Query<int?>(
                        "SELECT a.AdId FROM Ad a JOIN AdDesign ds ON ds.AdId = a.AdId WHERE ds.AdDesignId = @adDesignId", new { adDesignId }).FirstOrDefault();

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

        public void DropUserIfExists(string username)
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

                membershipDb.Execute("DELETE FROM Registration WHERE Email = @username", new { username });
                scope.Complete();
            }
        }

        public bool RegistrationExistsForEmail(string email)
        {
            return membershipDb.Query("SELECT Username FROM Registration WHERE Email = @email", new { email }).Any();
        }

        public Guid? AddUserIfNotExists(string username, string password, string email, RoleType roleType)
        {
            using (var scope = new TransactionScope())
            {
                var membershipProvider = Membership.Providers[RoleProviderDictionary[roleType]];
                if (membershipProvider == null)
                    throw new NullReferenceException();

                var applicationName = membershipProvider.ApplicationName;
                var applicationId = membershipDb.Query<Guid?>("SELECT ApplicationId FROM aspnet_Applications WHERE ApplicationName = @applicationName", new { applicationName }).FirstOrDefault();
                Guid? userId;
                if (applicationId.HasValue)
                {
                    userId = membershipDb.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username AND ApplicationId = @applicationId", new { username, applicationId }).FirstOrDefault();

                    if (userId.HasValue)
                        return userId;
                }

                MembershipCreateStatus createStatus;
                membershipProvider.CreateUser(username, password, username, null, null, true, Guid.NewGuid(), out createStatus);

                userId = membershipDb.Query<Guid?>("SELECT UserId FROM aspnet_Users WHERE UserName = @username", new { username }).First();

                string sql = string.Format(
                    "INSERT INTO {0} (UserID, FirstName, LastName, Email, PostCode, ProfileVersion, LastUpdatedDate) VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    Constants.MembershipTable.UserProfile, userId, username, username, email, 1000, 1, DateTime.Now);

                membershipDb.Execute(sql);
                scope.Complete();
                return userId;
            }
        }

        public void DropUserNetwork(string userId)
        {
            classifiedDb.ExecuteSql("DELETE FROM UserNetwork WHERE UserId = @userId", new {userId});
        }

        public List<Email> GetSentEmailsFor(string email)
        {
            return broadcastDb.Query<Email>("SELECT [To], DocType, ModifiedDate FROM EmailDelivery WHERE [To] = @email", new { email }).ToList();
        }

        public void AddRatecardIfNotExists(string ratecardName, decimal minCharge, decimal maxCharge, string category = "", bool autoAssign = true)
        {
            using (var scope = new TransactionScope())
            {
                var baseRateId = classifiedDb.AddIfNotExists(Constants.Table.BaseRate, new { Title = ratecardName, Description = ratecardName }, ratecardName);

                var ratecardId = classifiedDb.AddIfNotExists(Constants.Table.Ratecard, new { BaseRateId = baseRateId, MinCharge = minCharge, MaxCharge = maxCharge }, baseRateId.ToString(), findByColumnName: "BaseRateId");

                if (category.HasValue() && autoAssign)
                {
                    // Fetch categoryId ( reference tables )
                    var categoryId = classifiedDb.Single(Constants.Table.MainCategory, category);

                    List<int> publications = classifiedDb.Query<int>("SELECT PublicationId FROM Publication").ToList();

                    foreach (var publicationId in publications)
                    {
                        var publicationAdTypeId = classifiedDb.Single(Constants.Table.PublicationAdType, publicationId.ToString(), findBy: "PublicationId");
                        var publicationCategoryId = classifiedDb.Query<int?>("SELECT PublicationCategoryId FROM PublicationCategory WHERE MainCategoryId = @categoryId AND PublicationId = @publicationId", new { categoryId, publicationId }).Single();

                        var publicationRateId =
                            classifiedDb.Query<int?>(
                                "SELECT PublicationRateId FROM PublicationRate WHERE PublicationAdTypeId = @publicationAdTypeId AND PublicationCategoryId = @publicationCategoryId AND RatecardId = @ratecardId",
                                new
                                    {
                                        publicationAdTypeId,
                                        publicationCategoryId,
                                        ratecardId
                                    })
                                .FirstOrDefault();

                        if (publicationRateId.HasValue)
                            continue;

                        // Create a ratecard for each publication category
                        classifiedDb.Add(Constants.Table.PublicationRate, new
                            {
                                PublicationAdTypeId = publicationAdTypeId,
                                PublicationCategoryId = publicationCategoryId,
                                RatecardId = ratecardId
                            });
                    }
                }
                scope.Complete();
            }
        }

        public void AddLocationIfNotExists(string parentLocation, params string[] areas)
        {
            using (var scope = new TransactionScope())
            {
                var locationId = classifiedDb.AddIfNotExists(Constants.Table.Location, new { Title = parentLocation }, filterByValue: parentLocation);

                foreach (var locationArea in areas)
                {
                    classifiedDb.AddIfNotExists(Constants.Table.LocationArea, new { LocationId = locationId, Title = locationArea }, locationArea);
                }

                scope.Complete();
            }
        }

        public bool IsAdBookingCreated(string bookingReference)
        {
            var count = classifiedDb.Query<int>("SELECT COUNT(*) FROM AdBooking WHERE BookReference = @bookingReference", new { bookingReference }).Single();
            return count > 0;
        }

        public void AddOnlineRateForCategoryIfNotExists(decimal price, string categoryName)
        {
            var categoryId = GetCategoryIdForTitle(categoryName);

            classifiedDb.AddIfNotExists(Constants.Table.OnlineAdRate, new { MainCategoryId = categoryId, MinimumCharge = price }, categoryId, "MainCategoryId");
        }

        public int AddCategoryIfNotExists(string subCategory, string parentCategory)
        {
            using (var scope = new TransactionScope())
            {
                var parentCategoryId = classifiedDb.AddIfNotExists(Constants.Table.MainCategory, new { Title = parentCategory }, parentCategory);
                var subCategoryId = classifiedDb.AddIfNotExists(Constants.Table.MainCategory, new { Title = subCategory, ParentId = parentCategoryId }, subCategory);

                // Add for each publication in the system
                var addToPublications = classifiedDb.Query<int>("SELECT PublicationId FROM Publication").ToList();

                foreach (var publicationId in addToPublications)
                {
                    var publicationParentCategoryId = classifiedDb.AddIfNotExists(Constants.Table.PublicationCategory,
                        new
                        {
                            Title = parentCategory + publicationId,
                            MainCategoryId = parentCategoryId,
                            PublicationId = publicationId
                        }, parentCategory + publicationId);

                    var publicationChildCategoryId = classifiedDb.AddIfNotExists(Constants.Table.PublicationCategory,
                        new
                        {
                            Title = subCategory + publicationId,
                            MainCategoryId = subCategoryId,
                            PublicationId = publicationId,
                            ParentId = publicationParentCategoryId,
                        }, subCategory + publicationId);
                }

                scope.Complete();
                return subCategoryId.GetValueOrDefault();
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
            broadcastDb.Close();
        }


    }
}
