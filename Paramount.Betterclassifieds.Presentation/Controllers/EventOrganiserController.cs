using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
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
        private readonly IUserManager _userManager;

        public EventOrganiserController(IEventManager eventManager, ISearchService searchService, IUserManager userManager)
        {
            _eventManager = eventManager;
            _searchService = searchService;
            _userManager = userManager;
        }

        [HttpGet, ActionName("manage-organisers")]
        [Route("")]
        public ActionResult ManageOrganisers(int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var owner = _userManager.GetUserByUsername(ad.Username).Email;

            var vm = new ManageOrganisersViewModel()
            {
                AdId = ad.AdId,
                EventId = eventId,
                OwnerEmail = owner,
                Organisers = eventDetails.EventOrganisers
                    .Where(org => org.IsActive)
                    .Select(this.Map<EventOrganiser, EventOrganiserViewModel>)
            };

            return View(vm);
        }

        [HttpPost]
        [Route("invite")]
        public ActionResult InviteOrganiser(int eventId, string email)
        {
            // Ensure that the email is not the owner
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var owner = _userManager.GetUserByUsername(ad.Username).Email;

            if (owner.Equals(email, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Email", "The email you requested already belongs to the event owner");
                return JsonModelErrors();
            }

            if (eventDetails.EventOrganisers.Any(org => org.Email == email && org.IsActive))
            {
                ModelState.AddModelError("Email", "An event organiser with email " + email + " already exists.");
                return JsonModelErrors();
            }

            var organiser = _eventManager.CreateEventOrganiser(eventId, email);

            return Json(organiser);
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult RemoveOrganiser(int eventId, int eventOrganiserId)
        {
            _eventManager.RevokeOrganiserAccess(eventOrganiserId);
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