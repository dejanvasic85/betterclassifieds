using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Dapper;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository : ITestDataRepository
    {
        private readonly ConnectionFactory _connectionFactory;


        // Used for membership database
        private Dictionary<RoleType, string> RoleProviderDictionary = new Dictionary<RoleType, string>
        {
            { RoleType.Advertiser, "AppUserProvider"}
        };

        public DapperDataRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void SetClientConfig(string settingName, string settingValue)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                db.ExecuteSql("UPDATE AppSetting SET SettingValue = @settingValue WHERE AppKey = @settingName", new { settingValue, settingName });
            }
        }

        public int AddPublicationIfNotExists(string publicationName, string publicationType = Constants.PublicationType.Newspaper, string frequency = Constants.FrequencyType.Weekly, int? frequencyValue = 3)
        {
            using (var scope = new TransactionScope())
            using (var db = _connectionFactory.CreateClassifieds())
            {
                var publicationTypeId = db.Single(Constants.Table.PublicationType, publicationType, findBy: "Code");

                var publication = new
                {
                    Title = publicationName,
                    Description = "Selenium Paper",
                    PublicationTypeId = publicationTypeId,
                    FrequencyType = frequency,
                    FrequencyValue = frequencyValue,
                    Active = true
                };

                var publicationId = db.AddIfNotExists(Constants.Table.Publication, publication, filterByValue: publicationName);

                scope.Complete();

                return publicationId.GetValueOrDefault();
            }
        }

        public int AddPublicationAdTypeIfNotExists(string publicationName, string adTypeCode)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                var publicationId = db.SingleOrDefault(Constants.Table.Publication, publicationName);
                if (!publicationId.HasValue)
                    throw new ArgumentNullException("publicationName", "PublicationName " + publicationName + " does not exist and cannot create publication ad type");

                var adTypeId = db.SingleOrDefault(Constants.Table.AdType, adTypeCode, findByColumnName: "Code");
                if (!adTypeId.HasValue)
                    throw new ArgumentNullException("adTypeCode", "AdType " + adTypeCode + " does not exist and cannot create publication ad type");

                var publicationAdTypeId = db.Query<int?>("SELECT PublicationAdTypeId FROM PublicationAdType WHERE AdTypeId = @adTypeId AND PublicationId = @publicationId",
                    new { adTypeId, publicationId })
                    .FirstOrDefault();

                if (publicationAdTypeId.HasValue)
                    return publicationAdTypeId.Value;

                publicationAdTypeId = db.Add<int>(Constants.Table.PublicationAdType, new { PublicationId = publicationId, AdTypeId = adTypeId });
                return publicationAdTypeId.GetValueOrDefault();
            }
        }

        public void AddEditionsToPublication(string publicationName, int numberOfEditions)
        {
            var nextEdition = DateTimeHelper.DateForNext(DayOfWeek.Wednesday);

            using (var db = _connectionFactory.CreateClassifieds())
            using (var scope = new TransactionScope())
            {
                var publicationId = db.SingleOrDefault(Constants.Table.Publication, publicationName);

                // Create a whole bunch of editions
                for (int i = 0; i < numberOfEditions; i++)
                {
                    var editionDate = nextEdition.AddDays(i * 7);

                    // Find by multiple criteria
                    var editionId = db
                        .Query<int?>(
                            " SELECT EditionId FROM " + Constants.Table.Edition +
                            " WHERE PublicationId = @publicationId AND EditionDate = @editionDate", new { publicationId, editionDate })
                        .FirstOrDefault();

                    if (editionId.HasValue)
                        continue;

                    // Create the edition
                    db.Add(Constants.Table.Edition, new { publicationId, editionDate, deadline = editionDate.AddHours(-18), Active = true });
                }

                scope.Complete();
            }
        }

        public AdBookingContext GetAdBookingContextByReference(string bookReference)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Query<AdBookingContext>("select * from AdBooking where BookReference = @bookReference", new { bookReference }).SingleOrDefault();

            }
        }

        public int GetOnlineAdForBookingId(int adId)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return
                    db.Query<int>(
                        "SELECT o.OnlineAdId FROM AdBooking bk JOIN AdDesign ds on ds.AdId = bk.AdId JOIN OnlineAd o ON o.AdDesignId = ds.AdDesignId WHERE bk.AdBookingId = @adId",
                        new { adId }).Single();
            }
        }

        public int DropCreateOnlineAd(string adTitle, string categoryName, string subCategoryName, string username)
        {
            DropOnlineAdIfExists(adTitle);

            return AddOnlineAd(adTitle, subCategoryName, username);
        }

        public int AddOnlineAd(string adTitle, string categoryName, string username)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            using (var scope = new TransactionScope())
            {
                var adId = db.Add(Constants.Table.Ad, new { title = adTitle });

                var mainCategoryId = GetCategoryIdForTitle(categoryName);

                if (mainCategoryId == null)
                    throw new NullReferenceException("Cannot find the category. Check the StartUpHooks to see if the data setup is run successfully");

                int? bookingId = db.Add(Constants.Table.AdBooking, new
                {
                    @StartDate = DateTime.Now.AddDays(-1).Date,
                    @EndDate = DateTime.Now.AddDays(30).Date,
                    @TotalPrice = 0,
                    @BookReference = Guid.NewGuid().ToString().Substring(0, 5),
                    @AdId = adId,
                    @UserId = username.Default(TestData.DefaultUsername),
                    @BookingStatus = 1,
                    @MainCategoryId = mainCategoryId,
                    @BookingDate = DateTime.Now,
                    @Insertions = 1,
                    @BookingType = "Regular"
                });

                var adDesignId = db.Add(Constants.Table.AdDesign, new { adId, @adTypeId = 2 });

                var locationId = db.SingleOrDefault(Constants.Table.Location, TestData.Location_Australia);
                var areaId = db.SingleOrDefault(Constants.Table.LocationArea, TestData.Location_Victoria);

                var onlineAdid = db.Add(Constants.Table.OnlineAd, new
                {
                    @AdDesignId = adDesignId,
                    @Heading = adTitle,
                    @Description = adTitle,
                    @HtmlText = adTitle,
                    @NumOfViews = 100,
                    @Price = 1500,
                    @ContactName = "Sample Contact",
                    @ContactPhone = "0455555555",
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

            using (var db = _connectionFactory.CreateClassifieds())
            {
                var adsToDelete = db.Query<int?>("SELECT ds.AdDesignId FROM AdDesign ds JOIN OnlineAd o ON o.AdDesignId = ds.AdDesignId AND o.Heading = @title", new { @title = adTitle }).ToList();

                foreach (var adDesignId in adsToDelete)
                {
                    using (var scope = new TransactionScope())
                    {
                        if (!adDesignId.HasValue)
                            return;

                        var adId = db.Query<int?>("SELECT a.AdId FROM Ad a JOIN AdDesign ds ON ds.AdId = a.AdId WHERE ds.AdDesignId = @adDesignId", new { adDesignId }).FirstOrDefault();
                        var onlineAdId = db.Query<int?>("SELECT o.OnlineAdId FROM OnlineAd o WHERE o.AdDesignId = @adDesignId", new { adDesignId }).FirstOrDefault();

                        // Let's drop everything ! Starting from the online ad
                        db.ExecuteSql("DELETE from OnlineAd WHERE AdDesignId = @adDesignId", new { adDesignId });
                        db.ExecuteSql("DELETE from AdDesign WHERE AdDesignId = @adDesignId", new { adDesignId });

                        if (adId.HasValue)
                        {
                            db.ExecuteSql("DELETE FROM AdBooking WHERE AdId = @adId", new { adId });
                            db.ExecuteSql("DELETE FROM Ad WHERE AdId = @adId", new { adId });
                        }

                        if (onlineAdId.HasValue)
                        {
                            var eventId = db.Query<int?>("SELECT EventId from [Event] where OnlineAdId = @onlineAdId", new { onlineAdId }).FirstOrDefault();
                            if (eventId.HasValue)
                                db.ExecuteSql(GetSqlToDropEvent(), new { eventId });
                        }

                        scope.Complete();
                    }
                }
            }
        }

        public string GetSqlToDropEvent()
        {
            return @"
DELETE FROM EventBookingTicketValidation
WHERE EventBookingTicketId IN 
(
	SELECT EventBookingTicketId	FROM EventBookingTicket ebt
	JOIN EventBooking t on t.EventBookingId = ebt.EventBookingId
	WHERE t.EventId = @eventId
);

DELETE FROM EventBookingTicketField
WHERE EventBookingTicketId IN 
(
	SELECT EventBookingTicketId FROM EventBookingTicket ebt	
    JOIN EventBooking t on t.EventBookingId = ebt.EventBookingId WHERE t.EventId = @eventId
);

DELETE FROM EventBookingTicket
WHERE EventBookingTicketId IN
(
	SELECT EventBookingTicketId FROM EventBookingTicket ebt	
    JOIN EventBooking t on t.EventBookingId = ebt.EventBookingId WHERE t.EventId = @eventId
);

DELETE FROM EventTicketReservation WHERE EventTicketId IN
(
	SELECT t.EventTicketId FROM EventTicket t WHERE t.EventId = @eventId
);

DELETE FROM EventTicketField
WHERE EventTicketId IN
(
	SELECT EventTicketId FROM EventTicket t WHERE t.EventId = @eventId
)

DELETE FROM EventGroupTicket
WHERE EventGroupId IN
(
	SELECT EventGroupId FROM EventGroup gr WHERE gr.EventId = @eventId
);


DELETE FROM EventGroup WHERE EventId = @eventId;
DELETE FROM EventBooking WHERE EventId = @eventId;
DELETE FROM EventPaymentRequest WHERE EventId = @eventId;
DELETE FROM EventInvitation WHERE EventId = @eventId;
DELETE FROM EventTicket WHERE EventId = @eventId;
DELETE FROM [Event] WHERE EventId = @eventId;
";


        }

        public List<Email> GetSentEmailsFor(string email)
        {
            using (var db = _connectionFactory.CreateBroadcast())
            {
                return db.Query<Email>("SELECT [To], DocType, ModifiedDate FROM EmailDelivery WHERE [To] = @email", new { email }).ToList();

            }
        }

        public void AddRatecardIfNotExists(string ratecardName, decimal minCharge, decimal maxCharge, string category = "", bool autoAssign = true)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            using (var scope = new TransactionScope())
            {
                var baseRateId = db.AddIfNotExists(Constants.Table.BaseRate, new { Title = ratecardName, Description = ratecardName }, ratecardName);

                var ratecardId = db.AddIfNotExists(Constants.Table.Ratecard, new { BaseRateId = baseRateId, MinCharge = minCharge, MaxCharge = maxCharge }, baseRateId.ToString(), findByColumnName: "BaseRateId");

                if (category.HasValue() && autoAssign)
                {
                    // Fetch categoryId ( reference tables )
                    var categoryId = db.Single(Constants.Table.MainCategory, category);

                    List<int> publications = db.Query<int>("SELECT PublicationId FROM Publication").ToList();

                    foreach (var publicationId in publications)
                    {
                        var publicationAdTypeId = db.Single(Constants.Table.PublicationAdType, publicationId.ToString(), findBy: "PublicationId");
                        var publicationCategoryId = db.Query<int?>("SELECT PublicationCategoryId FROM PublicationCategory WHERE MainCategoryId = @categoryId AND PublicationId = @publicationId", new { categoryId, publicationId }).Single();

                        var publicationRateId =
                            db.Query<int?>(
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
                        db.Add(Constants.Table.PublicationRate, new
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
            using (var db = _connectionFactory.CreateClassifieds())
            using (var scope = new TransactionScope())
            {
                var locationId = db.AddIfNotExists(Constants.Table.Location, new { Title = parentLocation }, filterByValue: parentLocation);

                foreach (var locationArea in areas)
                {
                    db.AddIfNotExists(Constants.Table.LocationArea, new { LocationId = locationId, Title = locationArea }, locationArea);
                }

                scope.Complete();
            }
        }

        public void AddOnlineRateForCategoryIfNotExists(decimal price, string categoryName)
        {
            var categoryId = GetCategoryIdForTitle(categoryName);
            using (var db = _connectionFactory.CreateClassifieds())
            {
                db.AddIfNotExists(Constants.Table.OnlineAdRate, new { MainCategoryId = categoryId, MinimumCharge = price }, categoryId, "MainCategoryId");

            }
        }

        public void AddPrintRateForCategoryIfNotExists(string categoryName)
        {
            // We need to setup a lot here...
            // Get out if there's anything in the 


        }


        public int AddCategoryIfNotExists(string subCategory, string parentCategory, string categoryAdType = "")
        {
            using (var db = _connectionFactory.CreateClassifieds())
            using (var scope = new TransactionScope())
            {
                var parentCategoryId = db.AddIfNotExists(Constants.Table.MainCategory, new { Title = parentCategory }, parentCategory);
                var subCategoryId = db.AddIfNotExists(Constants.Table.MainCategory, new
                {
                    Title = subCategory,
                    ParentId = parentCategoryId,
                    CategoryAdType = categoryAdType.IsNullOrEmpty() ? null : categoryAdType
                }, subCategory);

                // Add for each publication in the system
                var addToPublications = db.Query<int>("SELECT PublicationId FROM Publication").ToList();

                foreach (var publicationId in addToPublications)
                {
                    var publicationParentCategoryId = db.AddIfNotExists(Constants.Table.PublicationCategory,
                        new
                        {
                            Title = parentCategory + publicationId,
                            MainCategoryId = parentCategoryId,
                            PublicationId = publicationId
                        }, parentCategory + publicationId);

                    var publicationChildCategoryId = db.AddIfNotExists(Constants.Table.PublicationCategory,
                        new
                        {
                            Title = subCategory + publicationId,
                            MainCategoryId = subCategoryId,
                            PublicationId = publicationId,
                            ParentId = publicationParentCategoryId
                        }, subCategory + publicationId);
                }

                scope.Complete();
                return subCategoryId.GetValueOrDefault();
            }
        }

        public int? GetCategoryIdForTitle(string categoryName)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Query<int?>(
                    "SELECT MainCategoryId FROM MainCategory WHERE Title = @Title", new { Title = categoryName })
                    .FirstOrDefault();
            }
        }
    }
}
