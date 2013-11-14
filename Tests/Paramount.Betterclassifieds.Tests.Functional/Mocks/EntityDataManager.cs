using Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public class EntityDataManager : IDataManager
    {
        public IDataManager Initialise()
        {
            using (BetterclassifiedsDbContext context = new BetterclassifiedsDbContext())
            {
                // Force the initialisation - Create if not exists
                context.Database.Initialize(true);

                // Explicitly call the seed!
                BetterclassifiedsDbInitialiser initialiser = new BetterclassifiedsDbInitialiser();
                initialiser.Seed(context);
            }

            return this;
        }

        public int AddOrUpdateOnlineAd(string adTitle)
        {
            using (BetterclassifiedsDbContext context = new BetterclassifiedsDbContext())
            {
                // Check if already exists
                var existingAd = context.OnlineAds.FirstOrDefault(o => o.Heading == adTitle);
                if (existingAd != null)
                    return existingAd.AdDesign.Ad.AdBookings.First().AdBookingId;

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
                }
                catch (DbEntityValidationException dbEntityValidationException)
                {
                    Console.WriteLine(dbEntityValidationException);
                    throw;
                }
                return booking.AdBookingId; // This is the new Ad ID!!
            }
        }
    }
}