namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;
    using Business;
    using Business.Booking;
    using Business.Broadcast;
    using Business.Print;
    using Business.Repository;
    using Tests.Mocks;
    using System;
    using System.Collections.Generic;
    using Business.Payment;


    [TestFixture]
    public class BookingManagerTests
    {
        private MockRepository _mockRepository;
        private List<Action> _verifyList;
        private IUnityContainer _container;

        private Mock<IBookingRepository> _bookingRepositoryMock;
        private Mock<IAdRepository> _adRepository;
        private Mock<IPublicationRepository> _publicationRepositoryMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IPaymentsRepository> _paymentRepositoryMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IBroadcastManager> _broadcastManagerMock;
        private Mock<IBookingContext> _bookingContext;
        private Mock<IBookCartRepository> _cartRepositoryMock;


        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _container = new UnityContainer().RegisterType<IBookingManager, BookingManager>();

            // Register all the mocks with the container
            _bookingRepositoryMock = _mockRepository.CreateMockOf<IBookingRepository>(_container);
            _adRepository = _mockRepository.CreateMockOf<IAdRepository>(_container, _verifyList);
            _publicationRepositoryMock = _mockRepository.CreateMockOf<IPublicationRepository>(_container);
            _clientConfigMock = _mockRepository.CreateMockOf<IClientConfig>(_container);
            _paymentRepositoryMock = _mockRepository.CreateMockOf<IPaymentsRepository>(_container);
            _userManagerMock = _mockRepository.CreateMockOf<IUserManager>(_container);
            _broadcastManagerMock = _mockRepository.CreateMockOf<IBroadcastManager>(_container);
            _bookingContext = _mockRepository.CreateMockOf<IBookingContext>(_container);
            _cartRepositoryMock = _mockRepository.CreateMockOf<IBookCartRepository>(_container);
        }

        [TearDown]
        public void CleanTest()
        {
            _verifyList.ForEach(verifyMe => verifyMe());
        }

        [Test]
        public void IncrementHits_AdIsNull_ReturnsNullAndDontUpdateOnlineAd()
        {
            // Arrange
            _adRepository.SetupWithVerification(call => call.GetOnlineAd(It.Is<int>(v => v == 100)), null);

            // Act
            _container.Resolve<IBookingManager>().IncrementHits(id: 100);

            // Assert
            _adRepository.Verify(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()), Times.Never);
        }

        [Test]
        public void IncrementHits_AdHitsIncremented_OnlineAdIsUpdated()
        {
            // Arrange
            var onlineAdMock = new OnlineAdModel();

            var adRepositoryMock = _mockRepository.CreateMockOf<IAdRepository>(_container, _verifyList);
            adRepositoryMock.SetupWithVerification(call => call.GetOnlineAd(It.Is<int>(v => v == 100)), onlineAdMock);
            adRepositoryMock.SetupWithVerification(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()));

            // Act
            _container.Resolve<IBookingManager>().IncrementHits(id: 100);

            // Assert
            Assert.That(onlineAdMock.NumOfViews, Is.EqualTo(1));
            adRepositoryMock.Verify(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()), Times.Once);
        }

        [Test]
        public void CreateBooking_WithNoLineAd_CallsBookingRepositoryOnce()
        {
            // arrange
            _bookingRepositoryMock.Setup(call => call.CreateBooking(It.IsAny<BookingCart>()));

            // act
            var bookingCart = new BookingCart("Session-123", "dvasic");
            

            // assert

        }
    }
}