﻿using System.Threading.Tasks;
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
        public IHttpActionResult GetAllEvents()
        {
            // Get all current 
            // var events = _searchService.GetCurrentEvents();
            return Ok("coming soon");
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
            var groups = await _eventManager.GetEventGroups(id, eventTicketId: null);
            return Ok(groups);
        }

        [Route("{id:int}/tickets/{ticketId:int}/groups")]
        public async Task<IHttpActionResult> GetEventGroupsForTicket(int id, int ticketId)
        {
            var groups = await _eventManager.GetEventGroups(id, ticketId);
            return Ok(groups);
        }
    }
}
