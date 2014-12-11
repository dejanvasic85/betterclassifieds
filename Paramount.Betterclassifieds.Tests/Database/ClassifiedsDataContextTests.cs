using System.Transactions;
using NUnit.Framework;
using Paramount.Betterclassifieds.DataService.Classifieds;
using System;
using System.Configuration;

namespace Paramount.Betterclassifieds.Tests.Database
{
    [Ignore]
    [Category("IntegrationTests"), Category("ClassifiedsDataContext")]
    [TestFixture]
    public class ClassifiedsDataContextTests
    {
        private string _connectionString;

        [SetUp]
        public void SetupConnection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;
        }

        [Test(Description = "Tests the Booking_Create stored procedure")]
        public void SubmitBooking_ValidFields_ReturnsAdId()
        {
            using (var context = new ClassifiedsDataContext(_connectionString))
            using (TransactionScope scope = new TransactionScope())
            {
                int? adBookingId = null;
                int? onlineDesignId = null;

                var booking = context.Booking_Create(
                    startDate: DateTime.Today,
                    endDate: DateTime.Today.AddDays(30),
                    totalPrice: 10,
                    bookReference: "123",
                    userId: "dvasic",
                    mainCategoryId: 8,
                    insertions: 1, 
                    adBookingId: ref adBookingId,
                    onlineAdHeading: "Test only",
                    onlineAdDescription:"test only",
                    onlineAdHtml: "<h2>test Only</h2>",
                    onlineAdPrice: null,
                    locationId: 8, // Any Location
                    locationAreaId: 27, // Any Area
                    contactName: "dejan",
                    contactEmail: "dejanvasic@outlook.com",
                    contactPhone: "0433095822",
                    onlineDesignId: ref onlineDesignId,
                    transactionType: 2,
                    onlineAdMarkdownText: "##Test Only"
                    );
               
                context.AdGraphics.InsertOnSubmit(new AdGraphic
                {
                    AdDesignId = onlineDesignId, DocumentID = Guid.NewGuid().ToString(),
                });

                context.SubmitChanges();

                scope.Complete();
                Assert.That(adBookingId, Is.Not.Null);
            }
        }
    }
}
