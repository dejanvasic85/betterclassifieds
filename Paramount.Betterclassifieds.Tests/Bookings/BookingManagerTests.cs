using System;
using System.Collections.Generic;
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

        [TestFixtureSetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _container = new UnityContainer().RegisterType<IBookingManager, BookingManager>();
        }

        [TestFixtureTearDown]
        public void CleanTest()
        {
            _verifyList.ForEach(verifyMe => verifyMe());
        }

        [Test]
        public void IncrementHits_AdIsNull_ReturnsNullAndDontUpdateOnlineAd()
        {
            // Arrange
            var adRepositoryMock = _mockRepository.CreateMockOf<IAdRepository>(_container, _verifyList);
            adRepositoryMock.SetupWithVerification(call => call.GetOnlineAd(It.Is<int>(v => v == 100)), null);

            _mockRepository.CreateMockOf<IBookingRepository>(_container);
            _mockRepository.CreateMockOf<IPublicationRepository>(_container);
            _mockRepository.CreateMockOf<IClientConfig>(_container);
            _mockRepository.CreateMockOf<IPaymentsRepository>(_container);
            _mockRepository.CreateMockOf<IUserManager>(_container);
            _mockRepository.CreateMockOf<IBroadcastManager>(_container);

            // Act
            _container.Resolve<IBookingManager>().IncrementHits(id: 100);

            // Assert
            adRepositoryMock.Verify(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()), Times.Never);
        }

        [Test]
        public void IncrementHits_AdHitsIncremented_OnlineAdIsUpdated()
        {
            // Arrange
            var onlineAdMock = new OnlineAdModel();
            
            var adRepositoryMock = _mockRepository.CreateMockOf<IAdRepository>(_container, _verifyList);
            adRepositoryMock.SetupWithVerification(call => call.GetOnlineAd(It.Is<int>(v => v == 100)), onlineAdMock);
            adRepositoryMock.SetupWithVerification(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()));

            _mockRepository.CreateMockOf<IBookingRepository>(_container);
            _mockRepository.CreateMockOf<IPublicationRepository>(_container);
            _mockRepository.CreateMockOf<IClientConfig>(_container);
            _mockRepository.CreateMockOf<IPaymentsRepository>(_container);
            _mockRepository.CreateMockOf<IUserManager>(_container);
            _mockRepository.CreateMockOf<IBroadcastManager>(_container);

            // Act
            _container.Resolve<IBookingManager>().IncrementHits(id: 100);

            // Assert
            Assert.That(onlineAdMock.NumOfViews, Is.EqualTo(1));
            adRepositoryMock.Verify(call => call.UpdateOnlineAd(It.IsAny<OnlineAdModel>()), Times.Once);
        }
    }
}