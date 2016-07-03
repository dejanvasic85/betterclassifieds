using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api;
using Paramount.Betterclassifieds.Presentation.Api.Models.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Api
{
    [TestFixture]
    public class EventApiControllerTests : TestContext<EventApiController>
    {
        [Test]
        public async void GetAllEvents_ReturnsData()
        {
            // Arrange
            IEnumerable<EventSearchResult> mockSearchResults = new[]
            {
                new EventSearchResult(
                    new AdSearchResultMockBuilder().Default().Build(),
                    new EventModelMockBuilder().Default().Build(),
                    new AddressMockBuilder().Default().Build())
            };

            _mockSearchService.SetupWithVerification(call => call.GetCurrentEvents(),
                mockSearchResults);

            var controller = BuildTargetObject();
            var events = await controller.GetAllEvents();

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<IEnumerable<EventContract>>>();

            result.Content.Count().IsEqualTo(1);
        }

        [Test]
        public async void GetEvent_HasEvent_ReturnsData()
        {
            IEnumerable<EventSearchResult> mockSearchResults = new[]
            {
                new EventSearchResult(
                    new AdSearchResultMockBuilder().Default().Build(),
                    new EventModelMockBuilder().Default().Build(),
                    new AddressMockBuilder().Default().Build())
            };

            _mockSearchService.SetupWithVerification(call => call.GetCurrentEvents(),
                mockSearchResults);

            var controller = BuildTargetObject();
            var events = await controller.GetEvent(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<EventContract>>();

            result.Content.IsNotNull();
            result.Content.EventId.IsEqualTo(123);
            result.Content.Heading.IsEqualTo("heading of mock ad");

        }



        [Test]
        public async void GetEventGroups_HasResults_ReturnsContracts()
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
                call => call.GetEventGroups(It.IsAny<int>(), It.IsAny<int?>()),
                Task.FromResult(mockEventGroups));

            var controller = BuildTargetObject();
            var events = await controller.GetEventGroups(123);

            events.IsNotNull();
            var result = events
                .IsTypeOf<OkNegotiatedContentResult<IEnumerable<EventGroupContract>>>();

            result.Content.IsNotNull();
            result.Content.Count().IsEqualTo(2);
        }

        private Mock<IEventManager> _mockEventManager;
        private Mock<ISearchService> _mockSearchService;

        [SetUp]
        public void SetupDependencies()
        {
            _mockEventManager = CreateMockOf<IEventManager>();
            _mockSearchService = CreateMockOf<ISearchService>();
        }
    }
}
