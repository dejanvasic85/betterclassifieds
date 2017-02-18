using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;
    using Business;
    using Business.Booking;
    using Business.Broadcast;
    using Business.Print;
    using Tests.Mocks;
    using System;
    using System.Collections.Generic;
    using Business.Payment;
    
    [TestFixture]
    public class BookingManagerTests
    {
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
        public void UpdateSchedule_WithNewStartDate_GeneratesNewEndDate_CallsRepository()
        {
            // arrange
            var startDate = DateTime.Today;
            var adId = 1;
            var onlineDayDuration = 7;
            var expectedEndDate = startDate.AddDays(onlineDayDuration);
            var mockBooking = new AdBookingModelMockBuilder().WithStartDate(DateTime.Today).Build();

            _dateServiceMock.SetupWithVerification(call => call.Today, DateTime.Today);
            _bookingRepositoryMock.SetupWithVerification(call => call.GetBooking(It.IsAny<int>(), false, false, false, false), mockBooking);
            _bookingRepositoryMock.Setup(call => call.UpdateBooking(
                It.Is<int>(v => v == adId),
                It.Is<DateTime>(v => v == startDate),
                It.Is<DateTime>(v => v == expectedEndDate),
                null));
            _clientConfigMock.Setup(call => call.RestrictedOnlineDaysCount).Returns(onlineDayDuration);

            // act
            _container.Resolve<BookingManager>().UpdateSchedule(adId, startDate);
        }

        [Test]
        public void UpdateSchedule_WithBookingStarted_ScheduleNotUpdated()
        {
            // arrange
            var startDate = DateTime.Today;
            var adId = 1;
            var onlineDayDuration = 7;
            var mockBooking = new AdBookingModelMockBuilder().WithStartDate(DateTime.Today.AddDays(-1)).Build();

            _dateServiceMock.SetupWithVerification(call => call.Today, DateTime.Today);
            _bookingRepositoryMock.SetupWithVerification(call => call.GetBooking(It.IsAny<int>(), false, false, false, false), mockBooking);

            // act
            _container.Resolve<BookingManager>().UpdateSchedule(adId, startDate);
        }

        [Test]
        public void SubmitAdEnquiry_CallRepository_And_SendsNotification()
        {
            var mockAdEnquiry = new AdEnquiryMockBuilder()
                .WithAdId(1)
                .WithEmail("mock@email.com")
                .WithFullName("John Smith")
                .WithPhone("04555555")
                .WithQuestion("Can I ask you something")
                .Build();

            var mockBooking = new AdBookingModelMockBuilder().WithUserId("user123").Build();
            var mockApplicationUser = new ApplicationUserMockBuilder().WithEmail("me@email.com").Build();

            // Setup all the calls that need to be made
            _adRepository.Setup(call => call.CreateAdEnquiry(It.IsAny<AdEnquiry>())).Callback<AdEnquiry>(a => a.EnquiryId = 100);
            _bookingRepositoryMock.Setup(call => call.GetBooking(It.Is<int>(i => i == mockAdEnquiry.AdId), false, false, false, false)).Returns(mockBooking);
            _userManagerMock.Setup(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == mockBooking.UserId))).Returns(mockApplicationUser);
            _broadcastManagerMock.Setup(call => call.SendEmail(It.IsAny<AdEnquiryTemplate>(), It.IsAny<string[]>()))
                .Returns(It.IsAny<Notification>());

            var bookingManager = _container.Resolve<BookingManager>();

            bookingManager.SubmitAdEnquiry(mockAdEnquiry);
        }

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
        private Mock<ICategoryAdFactory> _categoryAdRepositoryMock;
        private Mock<IDateService> _dateServiceMock;
        private Mock<IEventRepository> _eventRepositoryMock;

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
            _categoryAdRepositoryMock = _mockRepository.CreateMockOf<ICategoryAdFactory>(_container);
            _dateServiceMock = _mockRepository.CreateMockOf<IDateService>(_container);
            _eventRepositoryMock = _mockRepository.CreateMockOf<IEventRepository>(_container);
        }
    }
}