using System.Linq;
using System.Web.Http;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    [RoutePrefix("api/events")]
    public class EventController : ApiController
    {
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;

        public EventController(IEventManager eventManager, ISearchService searchService)
        {
            _eventManager = eventManager;
            _searchService = searchService;
        }

        [Route("")]
        public IHttpActionResult GetAllEvents()
        {
            // Get all current 
            // var events = _searchService.GetCurrentEvents();
            return Ok("coming soon");
        }

        [Route("{id}")]
        public IHttpActionResult GetEvent(int id)
        {
            // var eventModel = _eventManager.GetEventDetails(id);
            return Ok("coming soon");
        }
    }
}
