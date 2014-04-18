﻿using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Broadcast
{
    [TestClass]
    public class BroadcastManagerTests
    {
        private IUnityContainer _container;
        private MockRepository _mockRepository;
        private List<Action> _verifications;

        [TestInitialize]
        public void InitialiseTest()
        {
            _container = new UnityContainer();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifications = new List<Action>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _verifications.ForEach(verify => verify());
            _verifications.Clear();
            _container.Dispose();
        }

        [TestMethod]
        public void ProcessUnsent_OneProcessorReturnsTrueForTwoNotifications_CompletesSuccessfully()
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

            // Act
            BroadcastManager broadcastManager = new BroadcastManager(broadcastMockRepository.Object,
                new[] { notificationMockProcessor.Object });
            broadcastManager.ProcessUnsent(100);

            // Assert ( happens on clean up)
        }

        [TestMethod]
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

            // Act
            BroadcastManager broadcastManager = new BroadcastManager(broadcastMockRepository.Object,
                new[] { notificationMockProcessor.Object, badMockProcessor.Object });

            broadcastManager.ProcessUnsent(100);

            // Assert ( happens on clean up)
        }
    }
}