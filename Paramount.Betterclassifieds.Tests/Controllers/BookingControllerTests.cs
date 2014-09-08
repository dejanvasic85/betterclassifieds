using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class BookingControllerTests : ControllerTest<BookingController>
    {
        private Mock<ISearchService> mockSearchService;
        private Mock<IBookingManager> mockBookingManager;

        [SetUp]
        public void SetupDependencies()
        {
            mockSearchService = CreateMockOf<ISearchService>();
            mockBookingManager = CreateMockOf<IBookingManager>();
        }

        [Test]
        [Ignore]
        public void Step1_Get_ReturnsViewModel()
        {
            // arrange
            var mockCategories = new List<CategorySearchResult>
            {
                new CategorySearchResult{ Title = "MockCategory"}
            };
            var mockPublications = new List<PublicationModel>
            {
                new PublicationModel{ Title = "MockPublication"}
            };
            mockSearchService
                .SetupWithVerification(call => call.GetTopLevelCategories(), mockCategories)
                .SetupWithVerification(call => call.GetPublications(), mockPublications)
                ;
            
            // act
            var result = CreateController().Step1();

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var expectedModel = result.CastTo<ViewResult>().Model.CastTo<Step1View>();
            Assert.That(expectedModel, Is.Not.Null);
            Assert.That(expectedModel.Publications.Count(), Is.EqualTo(1));
            Assert.That(expectedModel.ParentCategories.Count(), Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Step1_Post_ReturnsRedirectResult()
        {
            // arrange
            

            // act

            // assert
        }
    }
}