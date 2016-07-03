using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.Api;

namespace Paramount.Betterclassifieds.Tests.Api
{
    [TestFixture]
    public class EventApiControllerTests : TestContext<EventApiController>
    {
        [Test]
        public async void GetAllEvents_ReturnsData()
        {
            var controller = BuildTargetObject();
            var events = await controller.GetAllEvents();
            
            events.IsNotNull();
            var result = events.IsTypeOf<OkNegotiatedContentResult<string>>();          
        }

        private Mock<IEventManager> _mockEventManager;

        [SetUp]
        public void SetupDependencies()
        {
            _mockEventManager = CreateMockOf<IEventManager>();
        }
    }
}
