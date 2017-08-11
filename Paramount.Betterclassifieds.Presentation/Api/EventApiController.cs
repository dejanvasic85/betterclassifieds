using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api.Models;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    [RoutePrefix("api/events")]
    public class EventApiController : ApiController
    {
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;
        private readonly IEventGuestService _eventGuestService;
        private readonly IEventSeatingService _eventSeatingService;

        public EventApiController(IEventManager eventManager, ISearchService searchService, IUserManager userManager, IEventGuestService eventGuestService, IEventSeatingService eventSeatingService)
        {
            _eventManager = eventManager;
            _searchService = searchService;
            _userManager = userManager;
            _eventGuestService = eventGuestService;
            _eventSeatingService = eventSeatingService;
        }

        [Route("")]
        public IHttpActionResult GetAllEvents()
        {
            // Get all current 
            var eventContractFactory = new EventContractFactory();
            var contracts = _searchService.GetEvents()
                .Select(eventContractFactory.FromModel);

            return Ok(contracts);
        }

        [Route("search")]
        public IHttpActionResult GetEventsByQuery([FromUri]EventSearchQuery query)
        {
            // Get all current 
            var eventContractFactory = new EventContractFactory();
            var results = _searchService.GetEvents();

            if (query.TakeMax.HasValue)
            {
                results = results.Take(query.TakeMax.Value);
            }

            if (query.User.HasValue())
            {
                results = results.Where(r => r.AdSearchResult.Username.Equals(query.User.Trim()));
            }

            var contracts = results
                .Select(eventContractFactory.FromModel);

            return Ok(contracts);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetEvent(int id)
        {
            var eventDetails = _eventManager.GetEventDetails(id);
            if (eventDetails == null)
                return NotFound();

            var onlineAdModel = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var isCurrentUserTheOwner = onlineAdModel.Username == _userManager.GetCurrentUser()?.Username;

            if (onlineAdModel.HasNotStarted() && !isCurrentUserTheOwner)
                return NotFound();

            var searchResult = new EventSearchResult(onlineAdModel, eventDetails, eventDetails.Address);
            var contract = new EventContractFactory().FromModel(searchResult);
            return Ok(contract);
        }

        [Route("{id:int}/groups")]
        public async Task<IHttpActionResult> GetEventGroups(int id)
        {
            var result = await _eventManager.GetEventGroupsAsync(id);
            var groups = result
                .Where(r => r.IsAvailable())
                .Select(new EventGroupContractFactory().FromModel)
                .OrderBy(g => g.GroupName)
                .AsEnumerable();

            return Ok(groups);
        }

        [Route("{id:int}/groups/{eventGroupId:int}")]
        public async Task<IHttpActionResult> GetEventGroup(int id, int eventGroupId)
        {
            var group = await _eventManager.GetEventGroup(eventGroupId);
            if (group == null)
                return NotFound();

            var contract = new EventGroupContractFactory().FromModel(group);
            return Ok(contract);
        }

        [Route("{id:int}/tickets/{ticketId:int}/groups")]
        public async Task<IHttpActionResult> GetEventGroupsForTicket(int id, int ticketId)
        {
            var result = await _eventManager.GetEventGroupsAsync(id, ticketId);
            var groups = result
                .Where(r => r.IsAvailable())
                .Select(g => new EventGroupContractFactory().FromModel(g))
                .OrderBy(g => g.GroupName)
                ;

            return Ok(groups);
        }

        [Route("{id:int}/ticket/{ticketId}/guests")]
        public IHttpActionResult GetEventTicketGuests(int id, int ticketId)
        {
            var guests = _eventManager.BuildGuestList(id).Where(g => g.TicketId == ticketId);
            return Ok(guests);
        }

        [Route("{id:int}/tickets")]
        public IHttpActionResult GetEventTicketTypes(int id)
        {
            var eventDetails = _eventManager.GetEventDetails(id);
            if (eventDetails == null)
                return NotFound();

            var tickets = eventDetails.Tickets.Select(t => new EventTicketContractFactory().FromModel(t));

            return Ok(tickets);
        }

        /// <summary>
        /// Returns ID's only for the tickets that are available for a group!
        /// </summary>
        [Route("{id:int}/groups/{eventGroupId:int}/tickets")]
        public async Task<IHttpActionResult> GetTicketsForGroup(int id, int eventGroupId)
        {
            var eventTickets = await _eventManager.GetEventTicketsForGroup(eventGroupId);
            if (eventTickets == null)
                return NotFound();

            return Ok(eventTickets);
        }

        [Route("{id:int}/guests")]
        public IHttpActionResult GetGuestList(int id)
        {
            var guestList = _eventManager.BuildGuestList(id);
            return Ok(new GuestContractFactory().FromEventGuestDetails(guestList));
        }

        [Route("{id:int}/guest-names")]
        public IHttpActionResult GetGuestNames(int id)
        {
            var guests = _eventGuestService.GetPublicGuestNames(id);
            return Ok(new GuestContractFactory().FromEventPublicGuestDetails(guests));
        }

        [Route("{id:int}/seating")]
        public IHttpActionResult GetEventSeating(int id)
        {
            return GetEventSeatingForRequest(id, string.Empty);
        }

        [Route("{id:int}/seating/{requestId}")]
        public IHttpActionResult GetEventSeatingForRequest(int id, string requestId)
        {
            var eventDetails = _eventManager.GetEventDetails(id);
            if (!eventDetails.IsSeatedEvent.GetValueOrDefault())
            {
                return Ok(new EventSeatingContract { VenueName = eventDetails.VenueName });
            }

            var tickets = eventDetails.Tickets.Where(t => t.IsActive);
            var seats = _eventSeatingService.GetSeatsForEvent(id, requestId);

            var factory = new EventSeatingContractFactory();

            return Ok(factory.FromModels(eventDetails, tickets, seats));
        }
    }
}