using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api;
using Paramount.Betterclassifieds.Presentation.Api.Models;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Api
{
    [TestFixture]
    public class EventApiControllerTests : TestContext<EventApiController>
    {
        private Mock<IEventManager> _mockEventManager;
        private Mock<ISearchService> _mockSearchService;
        private Mock<IUserManager> _mockUserManager;
        private Mock<IEventGuestService> _mockEventGuestService;
        private Mock<IEventSeatingService> _mockEventSeatingService;
        private Mock<IUrl> _mockUrl;
        private Mock<ICategoryAdFactory> _categoryAdFactory;

        [SetUp]
        public void SetupDependencies()
        {
            _mockEventManager = CreateMockOf<IEventManager>();
            _mockSearchService = CreateMockOf<ISearchService>();
            _mockUserManager = CreateMockOf<IUserManager>();
            _mockEventGuestService = CreateMockOf<IEventGuestService>();
            _mockEventSeatingService = CreateMockOf<IEventSeatingService>();
            _mockUrl = CreateMockOf<IUrl>();
            _categoryAdFactory = CreateMockOf<ICategoryAdFactory>();

            _mockUrl.Setup(call => call.WithAbsoluteUrl()).Returns(_mockUrl.Object);
        }

        [Test]
        public void GetAllEvents_ReturnsOk()
        {
            // Arrange
            var adSearchResult = new AdSearchResultMockBuilder().Default().Build();
            var mockTicketData = new EventSearchResultTicketDataMockBuilder()
                .WithCheapestTicket(0)
                .WithMostExpensiveTicket(100).Build();

            IEnumerable<EventSearchResult> mockSearchResults = new[]
            {
                new EventSearchResult(
                    adSearchResult,
                    new EventModelMockBuilder().Default().Build(),
                    new AddressMockBuilder().Default().Build(),
                    mockTicketData)
            };

            _mockSearchService.SetupWithVerification(call => call.GetEvents(null),
                mockSearchResults);

            _mockUrl.SetupWithVerification(call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()),
                "http://fake-event.url");

            var controller = BuildTargetObject();
            var events = controller.GetAllEvents();

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<IEnumerable<EventContract>>>();

            result.Content.Count().IsEqualTo(1);
            var eventContract = result.Content.Single();
            eventContract.Heading.IsEqualTo(adSearchResult.Heading);
            eventContract.HeadingSlug.IsEqualTo(adSearchResult.HeadingSlug);
            eventContract.EventUrl.IsNotNull();
        }

        [Test]
        public void GetEvent_HasEvent_ReturnsOk()
        {
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockResult = new AdSearchResultMockBuilder().Default().Build();
            var mockUser = new ApplicationUserMockBuilder().Default().WithUsername(mockResult.Username).Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _mockSearchService.SetupWithVerification(call => call.GetByAdOnlineId(It.IsAny<int>()), mockResult);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);
            _mockUrl.SetupWithVerification(call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "http://fake-event.url");

            var controller = BuildTargetObject();
            var events = controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<EventContract>>();

            result.Content.IsNotNull();
            result.Content.EventId.IsEqualTo(123);
            result.Content.Heading.IsEqualTo("heading of mock ad");

        }

        [Test]
        public void GetEvent_NoEvent_Returns_404()
        {

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), null);

            var controller = BuildTargetObject();
            var events = controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<NotFoundResult>();

        }

        [Test]
        public void GetEvent_HasNotStartedEvent_IsCurrentUser_ReturnsEvent()
        {

            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockResult = new AdSearchResultMockBuilder().Default()
                .WithStartDate(DateTime.Today.AddDays(10)).Build();
            var mockUser = new ApplicationUserMockBuilder().Default().WithUsername(mockResult.Username).Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _mockSearchService.SetupWithVerification(call => call.GetByAdOnlineId(It.IsAny<int>()), mockResult);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);
            _mockUrl.SetupWithVerification(call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "http://fake-event.url");

            var controller = BuildTargetObject();
            var events = controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<EventContract>>();

            result.Content.IsNotNull();
            result.Content.EventId.IsEqualTo(123);
            result.Content.Heading.IsEqualTo("heading of mock ad");

        }

        [Test]
        public void GetEvent_HasNotStartedEvent_NoLoggedInUser_Returns404()
        {

            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockResult = new AdSearchResultMockBuilder().Default()
                .WithStartDate(DateTime.Today.AddDays(10)).Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _mockSearchService.SetupWithVerification(call => call.GetByAdOnlineId(It.IsAny<int>()), mockResult);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), null);

            var controller = BuildTargetObject();
            var events = controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<NotFoundResult>();
        }

        [Test]
        public void GetEvent_HasNotStartedEvent_AdNotBelongToUser_Returns404()
        {

            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockResult = new AdSearchResultMockBuilder().Default()
                .WithStartDate(DateTime.Today.AddDays(10)).Build();
            var mockUser = new ApplicationUserMockBuilder().Default().WithUsername("unkown").Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _mockSearchService.SetupWithVerification(call => call.GetByAdOnlineId(It.IsAny<int>()), mockResult);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);

            var controller = BuildTargetObject();
            var events = controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<NotFoundResult>();
        }

        [Test]
        public async void GetEventGroups_HasResults_ReturnsOk()
        {
            var eventGroupMockBuilder = new EventGroupMockBuilder();
            IEnumerable<EventGroup> mockEventGroups = new[]
            {
                eventGroupMockBuilder.Default().WithEventGroupId(1).Build(),
                eventGroupMockBuilder.Default().WithEventGroupId(2).Build(),
                // disabled
                eventGroupMockBuilder.Default().WithEventGroupId(3).WithIsDisabled(true).Build(),
                // full
                eventGroupMockBuilder.Default().WithEventGroupId(4).WithMaxGuests(2).WithGuestCount(2).Build(),
            };

            _mockEventManager.SetupWithVerification(
                call => call.GetEventGroupsAsync(It.IsAny<int>(), It.IsAny<int?>()),
                Task.FromResult(mockEventGroups));

            var controller = BuildTargetObject();
            var events = await controller.GetEventGroups(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<IEnumerable<EventGroupContract>>>();

            result.Content.IsNotNull();
            result.Content.Count().IsEqualTo(2);
        }

        [Test]
        public async void GetEventGroup_HasResult_ReturnsOk()
        {
            var mockGroup = new EventGroupMockBuilder().Default().Build();

            _mockEventManager.SetupWithVerification(
                call => call.GetEventGroup(It.IsAny<int>()), Task.FromResult(mockGroup));

            var controller = BuildTargetObject();
            var eventGroups = await controller.GetEventGroup(123, 543);

            eventGroups.IsNotNull();
            var result = eventGroups
                .IsTypeOf<OkNegotiatedContentResult<EventGroupContract>>();

            result.Content.IsNotNull();
        }

        [Test]
        public void GetGuestList()
        {
            var eventGuests = new List<EventGuestDetails>()
            {
                new EventGuestDetails {GuestEmail = "hello@world.com", TicketId = 123}
            };

            _mockEventManager.SetupWithVerification(
                call => call.BuildGuestList(It.IsAny<int>()),
                eventGuests);

            var controller = BuildTargetObject();
            var result = controller.GetGuestList(111);
            var okResult = result.IsTypeOf<OkNegotiatedContentResult<IEnumerable<GuestContract>>>();
            var expected = okResult.Content.Single();

            expected.TicketId.IsEqualTo(123);
            expected.GuestEmail.IsEqualTo("hello@world.com");
        }

        [Test]
        public async void GetEventGroup_NoResults_ReturnsNotFound()
        {
            _mockEventManager.SetupWithVerification(
                call => call.GetEventGroup(It.IsAny<int>()),
                Task.FromResult<EventGroup>(null));

            var controller = BuildTargetObject();
            var results = await controller.GetEventGroup(123, 543);
            results.IsTypeOf<NotFoundResult>();
        }

        [Test]
        public void GetGuestNames_ReturnsList()
        {
            var mockEventId = 10;
            _mockEventGuestService.SetupWithVerification(call => call.GetPublicGuestNames(mockEventId),
                new List<EventGuestPublicView>
                {
                    new EventGuestPublicView("Cosmo Kramer", "Group 1")
                });

            var controller = BuildTargetObject();
            var result = controller.GetGuestNames(mockEventId);
            var okResult = result.IsTypeOf<OkNegotiatedContentResult<IEnumerable<GuestViewContract>>>();
            var expected = okResult.Content.Single();

            expected.GuestName.IsEqualTo("Cosmo Kramer");
            expected.GroupName.IsEqualTo("Group 1");

        }

        [Test]
        public void GetEventSeatingForRequest_EventHasNoSeating_ReturnsEmptyList()
        {
            var eventId = 1;
            var mockEvent = new EventModelMockBuilder()
                .WithIsSeatedEvent(false)
                .WithVenueName("Venue Gala").Default().Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(eventId), mockEvent);

            var result = BuildTargetObject().GetEventSeatingForRequest(eventId, string.Empty);

            var eventSeating = result.IsTypeOf<OkNegotiatedContentResult<EventSeatingContract>>();

            eventSeating.Content.IsNotNull();
            eventSeating.Content.VenueName.IsEqualTo("Venue Gala");
        }

        [Test]
        [Ignore]
        public void GetEventSeatingForRequest_HasSeating_ReturnsList()
        {
            var eventId = 1;
            var requestId = "requestId";

            var mockTicket = new EventTicketMockBuilder().Default().Build();

            var mockEvent = new EventModelMockBuilder()
                .WithIsSeatedEvent(true)
                .WithVenueName("Venue Gala")
                .WithTickets(new[] { mockTicket })
                .Default()
                .Build();

            var mockEventSeatBooking = new EventSeatMockBuilder()
                .WithRowNumber("A")
                .WithSeatNumber("A1")
                .WithEventTicketId(mockTicket.EventTicketId)
                .Build();

            _mockEventManager.SetupWithVerification(call => call.GetEventDetails(eventId), mockEvent);
            _mockEventSeatingService.SetupWithVerification(call => call.GetSeatsForEvent(
                It.Is<int>(e => e == eventId),
                It.Is<string>(r => r == requestId)), result: new[] { mockEventSeatBooking });

            var result = BuildTargetObject().GetEventSeatingForRequest(eventId, requestId);

            var eventSeating = result.IsTypeOf<OkNegotiatedContentResult<EventSeatingContract>>();

            eventSeating.Content.IsNotNull();
            eventSeating.Content.VenueName.IsEqualTo("Venue Gala");
            eventSeating.Content.Rows.ElementAt(0).RowName.IsEqualTo("A");
            eventSeating.Content.Rows.ElementAt(0).Seats.Single().SeatNumber.IsEqualTo("A1");
        }
    }
}
