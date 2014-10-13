using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;
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
        private Mock<IClientConfig> mockClientConfig;
        private Mock<IDocumentRepository> mockDocumentRepository;
        private Mock<IUserManager> mockUserManager;
        private Mock<IRateCalculator> mockRateCalculator;
        private Mock<IBroadcastManager> mockBroadcastManager;
        
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
        }

        [Test]
        public void Step1_Get_CreatesNewBookingWithCreator_ReturnsView()
        {
            // arrange
            var mockBookingCart = MockRepository.CreateMockOf<BookingCart>();
            ContainerBuilder.RegisterInstance(typeof(BookingCart), mockBookingCart.Object);

            mockBookingManager.SetupWithVerification(call => call.GetCart(It.IsAny<Func<BookingCart>>()), mockBookingCart.Object);
            mockSearchService
                .SetupWithVerification(call => call.GetCategories(), result: new List<CategorySearchResult>{
                    new CategorySearchResult {MainCategoryId = 1, Title = "mock cat 1"}})
                .SetupWithVerification(call => call.GetPublications(), result: new List<PublicationModel> { 
                    new PublicationModel { PublicationId = 1, Title = "publication 1"}});

            // act
            var result = CreateController().Step1();

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}