using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Organisers;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventOrganiserServiceTests : TestContext<EventOrganiserService>
    {

        private Mock<IDateService> _dateServiceMock;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IUserManager> _userManager;
        private Mock<ILogService> _logService;
        private Mock<IBookingManager> _bookingManager;

        [SetUp]
        public void Setup()
        {
            _dateServiceMock = CreateMockOf<IDateService>();
            _eventRepositoryMock = CreateMockOf<IEventRepository>();
            _userManager = CreateMockOf<IUserManager>();
            _logService = CreateMockOf<ILogService>();
            _bookingManager = CreateMockOf<IBookingManager>();
        }

        [Test]
        public void RevokeOrganiserAccess_GetsObjects_UpdatesActiveFlag()
        {
            var eventOrganiserMock = new EventOrganiserMockBuilder()
                .WithEventOrganiserId(88)
                .WithIsActive(true)
                .Build();

            var userMock = new ApplicationUserMockBuilder().Default().Build();

            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(
                call => call.GetEventOrganiser(It.IsAny<int>()), eventOrganiserMock);
            _eventRepositoryMock.SetupWithVerification(
                call => call.UpdateEventOrganiser(It.Is<EventOrganiser>(org => org == eventOrganiserMock)));
            _userManager.SetupWithVerification(
                call => call.GetCurrentUser(), userMock);

            var target = BuildTargetObject();
            target.RevokeOrganiserAccess(88);

            // assert
            eventOrganiserMock.IsActive.IsFalse();
            eventOrganiserMock.LastModifiedBy.IsEqualTo(userMock.Username);
            eventOrganiserMock.LastModifiedDate.IsNotNull();
            eventOrganiserMock.LastModifiedDateUtc.IsNotNull();

        }

        [Test]
        public void RevokeOrganiserAccess_NoOrganiserFound_ThrowsNullException()
        {
            _eventRepositoryMock.SetupWithVerification(
                call => call.GetEventOrganiser(It.IsAny<int>()), null);

            Assert.Throws<NullReferenceException>(() => BuildTargetObject().RevokeOrganiserAccess(1));
        }

        [Test]
        public void CreateEventOrganiser_NewObjectCreated_CallsRepository()
        {
            var userMock = new ApplicationUserMockBuilder().Default().Build();

            _userManager.SetupWithVerification(
                call => call.GetCurrentUser(), userMock);

            _eventRepositoryMock.SetupWithVerification(
                call => call.CreateEventOrganiser(It.IsAny<EventOrganiser>()));

            _dateServiceMock.SetupNowUtc().SetupNow();

            var result = BuildTargetObject().CreateEventOrganiser(1, "foo@bar.com");

            result.EventId.IsEqualTo(1);
            result.Email.IsEqualTo("foo@bar.com");
            result.InviteToken.IsNotNull();
            result.IsActive.IsTrue();
            result.LastModifiedDate.IsNotNull();
            result.LastModifiedDateUtc.IsNotNull();
            result.LastModifiedBy.IsEqualTo(userMock.Username);
            result.SubscribeToPurchaseNotifications.IsEqualTo(true);
            result.SubscribeToDailyNotifications.IsEqualTo(true);
        }

        [Test]
        public void ConfirmOrganiserInvite_OrganiserNotFound()
        {
            _eventRepositoryMock.SetupWithVerification(call =>
                call.GetEventOrganisersForEvent(It.IsAny<int>()),
                null
                );

            _logService.Setup(call => call.Info(It.Is<string>(str => str == "Organiser not found for token token123, event 123, email foo@bar.com")));

            var manager = BuildTargetObject();
            var result = manager.ConfirmOrganiserInvite(123, "token123", "foo@bar.com");

            result.IsEqualTo(OrganiserConfirmationResult.NotFound);
        }

        [Test]
        public void ConfirmOrganiserInvite_UserIdAlreadyExists()
        {
            var eventOrganiserMock = new EventOrganiserMockBuilder().Default()
                .WithUserId("user-1234")
                .WithIsActive(true)
                .WithEmail("foo@bar.com")
                .WithInviteToken(Guid.NewGuid())
                .Build();

            _eventRepositoryMock.SetupWithVerification(call =>
                call.GetEventOrganisersForEvent(It.IsAny<int>()),
                new List<EventOrganiser> { eventOrganiserMock });

            _logService.Setup(call => call.Info(It.Is<string>(str => str == "Organiser foo@bar.com is already activated for event 123")));

            var manager = BuildTargetObject();
            var result = manager.ConfirmOrganiserInvite(123, eventOrganiserMock.InviteToken.ToString(), "foo@bar.com");

            result.IsEqualTo(OrganiserConfirmationResult.AlreadyActivated);
        }

        [Test]
        public void ConfirmOrganiserInvite_UserLoggedIn_MisMatchedEmail()
        {
            var eventOrganiserMock = new EventOrganiserMockBuilder().Default()
                .WithUserId(null)
                .WithIsActive(true)
                .WithEmail("foo@bar.com")
                .WithInviteToken(Guid.NewGuid())
                .Build();

            // Currently logged in user doesn't match what was in the recipient!
            var mockUser = new ApplicationUserMockBuilder().Default().WithEmail("someone@else.com").Build();

            _userManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);

            _eventRepositoryMock.SetupWithVerification(call =>
                call.GetEventOrganisersForEvent(It.IsAny<int>()),
                new List<EventOrganiser> { eventOrganiserMock });

            _logService.SetupWithVerification(call => call.Info(It.Is<string>(str => str == "Mistatched email. Current: someone@else.com. Recipient: foo@bar.com")));

            var manager = BuildTargetObject();
            var result = manager.ConfirmOrganiserInvite(123, eventOrganiserMock.InviteToken.ToString(), "foo@bar.com");

            result.IsEqualTo(OrganiserConfirmationResult.MismatchedEmail);
        }

        [Test]
        public void ConfirmOrganiserInvite_UpdatesObject_SavesToRepository()
        {
            // Mocks
            var eventOrganiserMock = new EventOrganiserMockBuilder().Default()
                .WithUserId(null)
                .WithIsActive(true)
                .WithEmail("foo@bar.com")
                .WithInviteToken(Guid.NewGuid())
                .WithLastModifiedBy(null)
                .Build();

            var applicationUser = new ApplicationUserMockBuilder().Default().Build();

            // Setup services
            _eventRepositoryMock.SetupWithVerification(call =>
                call.GetEventOrganisersForEvent(It.IsAny<int>()),
                new List<EventOrganiser> { eventOrganiserMock });

            _eventRepositoryMock.SetupWithVerification(call =>
                call.UpdateEventOrganiser(It.Is<EventOrganiser>(o => o == eventOrganiserMock)));

            _logService.SetupWithVerification(call => call.Info(It.IsAny<string>()));
            _dateServiceMock.SetupNowUtc().SetupNow();

            _userManager.SetupWithVerification(call => call.GetCurrentUser(), applicationUser);

            var manager = BuildTargetObject();
            var result = manager.ConfirmOrganiserInvite(123, eventOrganiserMock.InviteToken.ToString(), "foo@bar.com");

            result.IsEqualTo(OrganiserConfirmationResult.Success);
            eventOrganiserMock.LastModifiedDate.IsNotNull();
            eventOrganiserMock.UserId.IsEqualTo(applicationUser.Username);
        }
    }
}
