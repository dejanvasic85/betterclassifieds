using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Moq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Broadcast
{
    [TestFixture]
    public class BroadcastManagerTests
    {
        private IUnityContainer _container;
        private MockRepository _mockRepository;
        private List<Action> _verifications;

        [TestFixtureSetUp]
        public void InitialiseTest()
        {
            _container = new UnityContainer();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifications = new List<Action>();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            _verifications.ForEach(verify => verify());
            _verifications.Clear();
            _container.Dispose();
        }

        [Test]
        public void ProcessUnsent_OneProcessorReturnsTrueForTwoNotifications_CompletesSuccessfully()
        {
            // Arrange
            var broadcast1 = Guid.NewGuid();
            var broadcast2 = Guid.NewGuid();

            List<Notification> incompleteNotifications = new List<Notification> { new Notification(broadcast1), new Notification(broadcast2) };

            var applicationConfig = _mockRepository.CreateMockOf<IApplicationConfig>();
            var broadcastMockRepository = _mockRepository.CreateMockOf<IBroadcastRepository>(_container, _verifications)
                .SetupWithVerification(call => call.GetIncompleteNotifications(100), incompleteNotifications)
                .SetupWithVerification(call => call.CreateOrUpdateNotification(It.IsAny<Notification>()));

            var notificationMockProcessor = _mockRepository.CreateMockOf<INotificationProcessor>(_container, _verifications)
                .SetupWithVerification(call => call.Retry(It.IsAny<Guid>()), true);


            // Act
            BroadcastManager broadcastManager = new BroadcastManager(broadcastMockRepository.Object,
                new[] { notificationMockProcessor.Object },
                applicationConfig.Object);

            broadcastManager.ProcessUnsent(100);

            // Assert ( happens on clean up)
        }

        [Test]
        public void ProcessUnsent_BadProcessorAndGoodProcessor_CompletesSuccessfully()
        {
            // Arrange
            var broadcast1 = Guid.NewGuid();
            var broadcast2 = Guid.NewGuid();

            List<Notification> incompleteNotifications = new List<Notification> { new Notification(broadcast1), new Notification(broadcast2) };

            var broadcastMockRepository = _mockRepository.CreateMockOf<IBroadcastRepository>(_container, _verifications)
                .SetupWithVerification(call => call.GetIncompleteNotifications(100), incompleteNotifications)
                .SetupWithVerification(call => call.CreateOrUpdateNotification(It.IsAny<Notification>()));

            var notificationMockProcessor = _mockRepository.CreateMockOf<INotificationProcessor>(_container, _verifications)
                .SetupWithVerification(call => call.Retry(It.IsAny<Guid>()), true);

            var badMockProcessor = _mockRepository.CreateMockOf<INotificationProcessor>(_container, _verifications)
                .SetupWithVerification(call => call.Retry(It.IsAny<Guid>()), false);
            var applicationConfig = _mockRepository.CreateMockOf<IApplicationConfig>();

            // Act
            BroadcastManager broadcastManager = new BroadcastManager(broadcastMockRepository.Object,
                new[] { notificationMockProcessor.Object, badMockProcessor.Object },
                applicationConfig.Object);

            broadcastManager.ProcessUnsent(100);

            // Assert ( happens on clean up)
        }

        [Test]
        public void Queue_CreatesNotification_SavesToRepository()
        {
            // arrange
            var mockNotificationDocType = new NewBooking();
            var mockEmailTemplate = new EmailTemplate();

            var mockRepository = _mockRepository.CreateMockOf<IBroadcastRepository>()
                .SetupWithVerification(call => call.CreateOrUpdateNotification(It.IsAny<Notification>()))
                .SetupWithVerification(call => call.GetTemplateByName(It.Is<string>(p => p == "NewBooking"), It.IsAny<string>()), mockEmailTemplate)
                .SetupWithVerification(call => call.CreateOrUpdateEmail(It.IsAny<Email>()), 1);

            var applicationConfig = _mockRepository.CreateMockOf<IApplicationConfig>()
                .SetupWithVerification(call => call.Brand, "TheMusic");
            var mockProcessor = _mockRepository.CreateMockOf<INotificationProcessor>();

            // act
            var broadcastManager = new BroadcastManager(mockRepository.Object, new[] { mockProcessor.Object }, applicationConfig.Object);
            var notification = broadcastManager.Queue(mockNotificationDocType, "foo@bar.com");

            // assert
            notification.IsNotNull();
            notification.BroadcastId.IsNotNull();
        }

        [Test]
        public void Queue_WithNo_ToList()
        {
            var mockRepository = _mockRepository.CreateMockOf<IBroadcastRepository>()
                .SetupWithVerification(call => call.CreateOrUpdateNotification(It.IsAny<Notification>()));

            var mockProcessor = _mockRepository.CreateMockOf<INotificationProcessor>();
            var applicationConfig = _mockRepository.CreateMockOf<IApplicationConfig>();

            var broadcastManager = new BroadcastManager(mockRepository.Object, new[] { mockProcessor.Object }, applicationConfig.Object);
            Assert.Throws<ArgumentException>(() => broadcastManager.Queue(new NewBooking()));
        }
    }
}