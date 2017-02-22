using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeBookingIdentity]
    [RoutePrefix("event-dashboard/{id}/event/{eventId}")]
    public class EventOrganiserController : ApplicationController, IMappingBehaviour
    {
        private readonly IEventManager _eventManager;

        public EventOrganiserController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet, ActionName("manage-organisers")]
        [Route("organisers")]
        public ActionResult ManageOrganisers(int id, int eventId)
        {
            var vm = new ManageOrganisersViewModel()
            {
                EventId = eventId,
                AdId = id,
                Organisers = _eventManager.GetEventDetails(eventId).EventOrganisers
                .Select(this.Map<EventOrganiser, EventOrganiserViewModel>)
            };

            return View(vm);
        }

        [HttpPost]
        [Route("add-organiser")]
        public ActionResult AddOrganiser(int id, int eventId, string username)
        {
            var organiser = _eventManager.CreateEventOrganiser(eventId, username);

            return Json(this.Map<EventOrganiser, EventOrganiserViewModel>(organiser));
        }

        [HttpPost]
        [Route("remove-organiser")]
        public ActionResult RemoveOrganiser(int id, int eventId, string username)
        {
            return Json(true);
        }
        
        [HttpPost]
        [Route("invite-organiser")]
        public ActionResult InviteOrganiser(int id, int eventId, string email)
        {
            // Todo - send email to organiser and create invite record
            return Json(true);
        }

        [HttpPost]
        [Route("revoke-organiser-invite")]
        public ActionResult RevokeOrganiserInvitation(int id, int eventId, string email)
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