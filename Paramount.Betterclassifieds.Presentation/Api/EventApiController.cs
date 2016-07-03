using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    [RoutePrefix("api/events")]
    public class EventApiController : ApiController
    {
        private readonly IEventManager _eventManager;

        public EventApiController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAllEvents()
        {
            // Get all current 
            // var events = _searchService.GetCurrentEvents();
            return Ok(await Task.FromResult("Coming soon"));
        }

        [Route("{id:int}")]
        public IHttpActionResult GetEvent(int id)
        {
            // var eventModel = _eventManager.GetEventDetails(id);
            return Ok("coming soon");
        }

        [Route("{id:int}/groups")]
        public async Task<IHttpActionResult> GetEventGroups(int id)
        {
            var groups = await _eventManager.GetEventGroups(id);
            return Ok(groups.Where(g => g.IsAvailable()));
        }

        [Route("{id:int}/groups/{eventGroupId:int}")]
        public async Task<IHttpActionResult> GetEventGroup(int id, int eventGroupId)
        {
            var group = await _eventManager.GetEventGroup(eventGroupId);
            return Ok(group);
        }

        [Route("{id:int}/tickets/{ticketId:int}/groups")]
        public async Task<IHttpActionResult> GetEventGroupsForTicket(int id, int ticketId)
        {
            var groups = await _eventManager.GetEventGroups(id, ticketId);
            return Ok(groups.Where(g => g.IsAvailable()));
        }

        [Route("{id:int}/groups/{eventGroupId:int}/tickets")]
        public async Task<IHttpActionResult> GetAvailableTicketsForGroup(int id, int eventGroupId)
        {
            var eventTickets = await _eventManager.GetEventTicketsForGroup(eventGroupId);
            return Ok(eventTickets);
        }
    }
}
