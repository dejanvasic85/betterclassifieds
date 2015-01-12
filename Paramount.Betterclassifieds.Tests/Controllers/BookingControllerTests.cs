using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Framework;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class BookingControllerTests : ControllerTest<BookingController>
    {
        private Mock<ISearchService> mockSearchService;
        private Mock<IBookingManager> mockBookingManager;
        private Mock<IClientConfig> mockClientConfig;
        private Mock<IDocumentRepository> mockDocumentRepository;
        private Mock<IUserManager> mockUserManager;
        private Mock<IRateCalculator> mockRateCalculator;
        private Mock<IBroadcastManager> mockBroadcastManager;
        private Mock<IApplicationConfig> mockApplicationConfig;
        private Mock<IPaymentService> mockPaymentService;

        [SetUp]
        public void SetupDependencies()
        {
            mockSearchService = CreateMockOf<ISearchService>();
            mockBookingManager = CreateMockOf<IBookingManager>();
            mockClientConfig = CreateMockOf<IClientConfig>();
            mockDocumentRepository = CreateMockOf<IDocumentRepository>();
            mockUserManager = CreateMockOf<IUserManager>();
            mockRateCalculator = CreateMockOf<IRateCalculator>();
            mockBroadcastManager = CreateMockOf<IBroadcastManager>();
            mockApplicationConfig = CreateMockOf<IApplicationConfig>();
            mockPaymentService = CreateMockOf<IPaymentService>();
        }

        [Test]
        public void Step1_Get_CreatesNewBookingWithCreator_ReturnsView()
        {
            // arrange
            var mockBookingCart = MockRepository.CreateMockOf<BookingCart>();
            ContainerBuilder.RegisterInstance(typeof(BookingCart), mockBookingCart.Object);

            mockBookingManager.SetupWithVerification(call => call.GetCart(It.IsAny<Func<BookingCart>>()), mockBookingCart.Object);
            mockSearchService
                .SetupWithVerification(call => call.GetCategories(), result: new List<CategorySearchResult>
                {
                    new CategorySearchResult { MainCategoryId = 1, Title = "mock cat 1" },
                    new CategorySearchResult { MainCategoryId = 2, Title = "mock sub 1", ParentId = 1},
                    new CategorySearchResult { MainCategoryId = 3, Title = "mock sub 2", ParentId = 1}
                })
                //.SetupWithVerification(call => call.GetPublications(), result: new List<PublicationModel> { new PublicationModel { PublicationId = 1, Title = "publication 1" } })
                ;

            // act
            var result = CreateController().Step1();

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Step1_Get_CategorySelected_LoadsSubCategoriesForViewModel_ReturnsView()
        {
            // arrange
            var mockBookingCart = new BookingCart { CategoryId = 1, SubCategoryId = 2 };
            ContainerBuilder.RegisterInstance(typeof(BookingCart), mockBookingCart);

            mockBookingManager.SetupWithVerification(call => call.GetCart(It.IsAny<Func<BookingCart>>()), mockBookingCart);
            mockSearchService
                .SetupWithVerification(call => call.GetCategories(), result: new List<CategorySearchResult>
                {
                    new CategorySearchResult { MainCategoryId = 1, Title = "mock cat 1" },
                    new CategorySearchResult { MainCategoryId = 2, Title = "mock sub 1", ParentId = 1},
                    new CategorySearchResult { MainCategoryId = 3, Title = "mock sub 2", ParentId = 1}
                })
                //.SetupWithVerification(call => call.GetPublications(), result: new List<PublicationModel>{new PublicationModel { PublicationId = 1, Title = "publication 1" }})
                ;

            // act
            var result = CreateController().Step1();

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as Step1View;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.SubCategoryOptions.Count(), Is.EqualTo(2));
        }

        [Test]
        [Ignore]
        public void Step1_Get_PublicationSelected_SetSelectedPublications_ReturnsView()
        {
            // arrange
            var mockBookingCart = new BookingCart { Publications = new[] { 1 } };
            ContainerBuilder.RegisterInstance(typeof(BookingCart), mockBookingCart);

            mockBookingManager.SetupWithVerification(call => call.GetCart(It.IsAny<Func<BookingCart>>()), mockBookingCart);
            mockSearchService
                .SetupWithVerification(call => call.GetCategories(), result: new List<CategorySearchResult>
                {
                    new CategorySearchResult { MainCategoryId = 1, Title = "mock cat 1" },
                    new CategorySearchResult { MainCategoryId = 2, Title = "mock sub 1", ParentId = 1},
                    new CategorySearchResult { MainCategoryId = 3, Title = "mock sub 2", ParentId = 1}
                })
                .SetupWithVerification(call => call.GetPublications(), result: new List<PublicationModel>
                {
                    new PublicationModel { PublicationId = 1, Title = "publication 1" }
                })
                ;

            // act
            var result = CreateController().Step1();

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as Step1View;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.Publications.Any(p => p.IsSelected), Is.True);
        }

        [Test]
        public void GetPlaintextFromMarkdown_ConvertsWithMkDeep_ReturnsString()
        {
            var controller = CreateController();
            var result = controller.GetPlaintextFromMarkdown("**one bold value**");

            Assert.That(result, Is.TypeOf<JsonResult>());
            var json = (JsonResult) result;
            Assert.That(json.Data, Has.Property("plaintext"));
            Assert.That(json.Data.ToJsonString(), Is.StringContaining("one bold value"));
        }
    }
}