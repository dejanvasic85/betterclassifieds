namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    using DataAccess.Classifieds;
    using Domain;
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;

    public class EntityDataManager : ITestDataManager
    {
        public ITestDataManager Initialise()
        {
            using (BetterclassifiedsDbContext context = new BetterclassifiedsDbContext())
            {
                // Setup main test seed data here
                var seleniumCategory = new Repository<MainCategory>(context).AddOrUpdate(c => c.Title == "Selenium Category", new MainCategory { Title = "Selenium Category", ParentId = null });
                var seleniumSubCategory = new Repository<MainCategory>(context).AddOrUpdate(c => c.Title == "Selenium Sub Category", new MainCategory { Title = "Selenium Sub Category", ParentCategory = seleniumCategory });
                context.SaveChanges();
            }

            return this;
        }

        public ITestDataManager AddOrUpdateOnlineAd(string adTitle, out int? adId)
        {
            using (BetterclassifiedsDbContext context = new BetterclassifiedsDbContext())
            {
                // Check if already exists
                var existingAd = context.OnlineAds.FirstOrDefault(o => o.Heading == adTitle);
                if (existingAd != null)
                {
                    adId = existingAd.AdDesign.Ad.AdBookings.First().AdBookingId;
                    return this;
                }

                var booking = new AdBooking
                    {
                        BookReference = "SEL-001",
                        BookingDate = DateTime.Now,
                        BookingStatus = 1,
                        StartDate = DateTime.Now.AddDays(-1),
                        EndDate = DateTime.Now.AddMonths(1),
                        UserId = "Selenium User"
                    };

                var onlineAd = new OnlineAd
                    {
                        Heading = adTitle,
                        ContactName = "Sample Contact",
                        ContactType = "Email",
                        ContactValue = "samplecontact@email.com",
                        Description = "This is a sample description for an ad",
                        HtmlText = "<string>This is a sample description for an ad</strong>",
                        NumOfViews = 1001,
                        Price = 500
                    };

                var adDesign = new AdDesign { OnlineAds = new[] { onlineAd }, AdType = context.AdTypes.First(t => t.Code == "ONLINE") };
                booking.Ad = new Ad { AdDesigns = new[] { adDesign } };
                booking.MainCategory = context.MainCategories.First(m => m.Title == "Selenium Sub Category");
                context.AdBookings.Add(booking);
                try
                {
                    context.SaveChanges();
                    adId = booking.AdBookingId;
                }
                catch (DbEntityValidationException dbEntityValidationException)
                {
                    Console.WriteLine(dbEntityValidationException);
                    throw;
                }
                return this; // This is the new Ad ID!!
            }
        }

        public ITestDataManager DropUserIfExists(string username)
        {
            using (var context = new BetterclassifiedsDbContext())
            {
                // todo - 
                return this;
            }
        }
    }
}