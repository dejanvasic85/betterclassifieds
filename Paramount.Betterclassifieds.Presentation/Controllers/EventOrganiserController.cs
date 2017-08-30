using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Organisers;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeEventAccess]
    [RoutePrefix("event-dashboard/{eventId}/organisers")]
    public class EventOrganiserController : ApplicationController, IMappingBehaviour
    {
        private readonly IEventManager _eventManager;
        private readonly IEventOrganiserService _eventOrganiserService;
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;
        private readonly IMailService _mailService;

        public EventOrganiserController(IEventManager eventManager, ISearchService searchService, IUserManager userManager, IMailService mailService, IEventOrganiserService eventOrganiserService)
        {
            _eventManager = eventManager;
            _searchService = searchService;
            _userManager = userManager;
            _mailService = mailService;
            _eventOrganiserService = eventOrganiserService;
            _mailService.Initialise(this);
        }

        #region View Endpoints

        [HttpGet, ActionName("manage-organisers")]
        [Route("")]
        public ActionResult ManageOrganisers(int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var owner = _userManager.GetUserByUsername(ad.Username);
            var ownerEmail = owner.Email;

            var vm = new ManageOrganisersViewModel()
            {
                AdId = ad.AdId,
                EventId = eventId,
                OwnerEmail = ownerEmail,
                Organisers = eventDetails.EventOrganisers
                    .Where(org => org.IsActive)
                    .Where(org => !org.Email.Equals(ownerEmail))
                    .Select(this.Map<EventOrganiser, EventOrganiserViewModel>)
            };

            return View(vm);
        }

        [HttpGet]
        [Route("notifications")]
        [ActionName("manage-notifications")]
        public ActionResult ManageEventNotifications(int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var currentUser = _userManager.GetCurrentUser();

            // Defaults to true for all subscriptions
            var vm = new EventOrganiserNotificationsViewModel(ad.AdId, eventId);

            var organiser = eventDetails.EventOrganisers.FirstOrDefault(o => o.Email == currentUser.Email);
            if (organiser != null)
            {
                vm.SubscribeToPurchaseNotifications = organiser.SubscribeToPurchaseNotifications;
                vm.SubscribeToDailyNotifications = organiser.SubscribeToDailyNotifications;
            }

            return View(vm);
        }

        #endregion View Endpoints

        #region Json Endpoints

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

            var organiser = _eventOrganiserService.CreateEventOrganiser(eventId, email);
            _mailService.SendEventOrganiserInvite(email, ad, eventId, organiser.InviteToken.ToString());

            return Json(organiser);
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult RemoveOrganiser(int eventId, int eventOrganiserId)
        {
            _eventOrganiserService.RevokeOrganiserAccess(eventOrganiserId);
            return Json(true);
        }
        
        [HttpPost]
        [Route("notifications")]
        public ActionResult Notifications(EventOrganiserNotificationsViewModel vm)
        {
            var user = _userManager.GetCurrentUser();

            _eventOrganiserService.UpdateOrganiserNotifications(vm.EventId, user, 
                vm.SubscribeToPurchaseNotifications.GetValueOrDefault(),
                vm.SubscribeToDailyNotifications.GetValueOrDefault());

            return Json(true);
        }

        #endregion

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventOrganiser, EventOrganiserViewModel>().ReverseMap();
        }
    }

}