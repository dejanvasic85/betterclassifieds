using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeEventAccess]
    [RoutePrefix("event-dashboard/{eventId}/organisers")]
    public class EventOrganiserController : ApplicationController, IMappingBehaviour
    {
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;

        public EventOrganiserController(IEventManager eventManager, ISearchService searchService)
        {
            _eventManager = eventManager;
            _searchService = searchService;
        }

        [HttpGet, ActionName("manage-organisers")]
        [Route("")]
        public ActionResult ManageOrganisers(int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);

            var vm = new ManageOrganisersViewModel()
            {
                AdId = ad.AdId,
                EventId = eventId,
                Organisers = eventDetails.EventOrganisers
                .Select(this.Map<EventOrganiser, EventOrganiserViewModel>)
            };

            return View(vm);
        }

        [HttpPost]
        [Route("invite")]
        public ActionResult InviteOrganiser(int eventId, string email)
        {
            // Ensure the organiser 
            var eventDetails = _eventManager.GetEventDetails(eventId);
            if (eventDetails.EventOrganisers.Any(org => org.Email == email && org.IsActive))
            {
                ModelState.AddModelError("Email", "An event organiser with email " + email + " already exists.");
            }

            var organiser = _eventManager.CreateEventOrganiser(eventId, email);
            
            return Json(organiser);
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult RemoveOrganiser(int eventId, string username)
        {
            return Json(true);
        }

        [HttpPost]
        [Route("revoke")]
        public ActionResult RevokeInvite(int eventId, string email)
        {
            // Todo - remove the invitation
            return Json(true);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventOrganiser, EventOrganiserViewModel>().ReverseMap();
        }
    }
}