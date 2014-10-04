using System;
using System.Collections.Generic;
using System.Web.UI;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
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
        private Mock<IBookingSessionIdentifier> _sessionIdentifierMock;
        private Mock<IBookingCartRepository> _cartRepositoryMock;


        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _container = new UnityContainer().RegisterType<IBookingManager, BookingManager>();

            _bookingRepositoryMock = _mockRepository.CreateMockOf<IBookingRepository>(_container);
            _adRepository = _mockRepository.CreateMockOf<IAdRepository>(_container, _verifyList);
            _publicationRepositoryMock = _mockRepository.CreateMockOf<IPublicationRepository>(_container);
            _clientConfigMock = _mockRepository.CreateMockOf<IClientConfig>(_container);
            _paymentRepositoryMock = _mockRepository.CreateMockOf<IPaymentsRepository>(_container);
            _userManagerMock = _mockRepository.CreateMockOf<IUserManager>(_container);
            _broadcastManagerMock = _mockRepository.CreateMockOf<IBroadcastManager>(_container);
            _sessionIdentifierMock = _mockRepository.CreateMockOf<IBookingSessionIdentifier>(_container);
            _cartRepositoryMock = _mockRepository.CreateMockOf<IBookingCartRepository>(_container);
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
        public void GetCart_NoCartNoCreator_ThrowsNullReferenceException()
        {
            // Arrange
            _sessionIdentifierMock.SetupWithVerification(call => call.Id, string.Empty);
            _cartRepositoryMock.SetupWithVerification(call => call.GetBookingCart(It.IsAny<string>()), null);

            // Act
            // Assert
            var bookingManager = _container.Resolve<IBookingManager>();
            Assert.Throws<NullReferenceException>(() => bookingManager.GetCart());
        }

        [Test]
        public void GetCart_NoCartUseCreatorInstead_ReturnsBookingCart()
        {
            // Arrange
            var mockCart = new BookingCart
            {
                Id = "123",
                CategoryId = 1,
                SubCategoryId = 2
            };

            _sessionIdentifierMock.SetupWithVerification(call => call.Id, "123");
            _cartRepositoryMock.SetupWithVerification(call => call.GetBookingCart(It.IsAny<string>()), null);
            _cartRepositoryMock.SetupWithVerification(call => call.SaveBookingCart(It.Is<BookingCart>(b => b == mockCart)), null);

            // Act
            var bookingManager = _container.Resolve<IBookingManager>();
            var cart = bookingManager.GetCart(() => mockCart);

            // Assert
            Assert.That(cart, Is.EqualTo(mockCart));
        }

        [Test]
        public void GetCart_CartExists_ReturnsBookingCart()
        {
            // Arrange
            var mockCart = new BookingCart
            {
                Id = "123",
                CategoryId = 1,
                SubCategoryId = 2
            };

            _sessionIdentifierMock.SetupWithVerification(call => call.Id, "123");
            _cartRepositoryMock.SetupWithVerification(call => call.GetBookingCart(It.Is<string>(str=> str == "123")), mockCart);

            // Act
            var bookingManager = _container.Resolve<IBookingManager>();
            var cart = bookingManager.GetCart(() => new BookingCart
            {
                Id = "321",
                CategoryId = 1999,
                SubCategoryId = 9000
            });

            // Assert
            Assert.That(cart, Is.EqualTo(mockCart));
        }
    }
}