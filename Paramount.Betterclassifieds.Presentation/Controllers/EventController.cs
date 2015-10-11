using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;
        private readonly HttpContextBase _httpContext;
        private readonly IClientConfig _clientConfig;
        private readonly IUserManager _userManager;

        public EventController(ISearchService searchService, IEventManager eventManager, HttpContextBase httpContext, IClientConfig clientConfig, IUserManager userManager)
        {
            _searchService = searchService;
            _eventManager = eventManager;
            _httpContext = httpContext;
            _clientConfig = clientConfig;
            _userManager = userManager;
        }

        public ActionResult ViewEventAd(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetByAdId(id);

            if (onlineAdModel == null)
            {
                return View("~/Views/Listings/404.cshtml");
            }

            var eventModel = _eventManager.GetEventDetailsForOnlineAdId(onlineAdModel.OnlineAdId);
            var eventViewModel = this.Map<Business.Events.EventModel, EventViewDetailsModel>(eventModel);
            this.Map(onlineAdModel, eventViewModel);
            this.Map(_clientConfig, eventViewModel);

            return View(eventViewModel);
        }

        [HttpPost]
        public ActionResult ReserveTickets(List<EventTicketRequestViewModel> tickets)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, Errors = ModelState.ToErrors() });
            }

            var ticketRequests = this.MapList<EventTicketRequestViewModel, EventTicketReservationRequest>(tickets);
            _eventManager.ReserveTickets(_httpContext.With(s => s.Session).SessionID, ticketRequests);

            return Json(new { IsValid = true, Redirect = Url.Action("BookTickets") });
        }

        [HttpGet]
        public ActionResult BookTickets()
        {
            var ticketReservations = _eventManager.GetTicketReservations(_httpContext.With(s => s.Session).SessionID).ToList();
            var eventId = ticketReservations.FirstOrDefault().With(t => t.EventTicket.With(r => r.EventId));
            if (!eventId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var eventDetails = _eventManager.GetEventDetails(eventId.Value);
            var onlineAdModel = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var remainingTimeToCompleteBooking = _eventManager.GetRemainingTimeForReservationCollection(ticketReservations);

            // Construct the view model
            var viewModel = new BookTicketsViewModel
            {
                TotelReservationExpiryMinutes = _clientConfig.EventTicketReservationExpiryMinutes,
                Title = onlineAdModel.Heading,
                AdId = onlineAdModel.AdId,
                Description = onlineAdModel.Description,
                CategoryAdType = onlineAdModel.CategoryAdType,
                Reservations = this.MapList<EventTicketReservation, EventTicketReservedViewModel>(ticketReservations),
                SuccessfulReservationCount = ticketReservations.Count(r => r.Status == EventTicketReservationStatus.Reserved),
                LargeRequestCount = ticketReservations.Count(r => r.Status == EventTicketReservationStatus.RequestTooLarge)
            };

            if (remainingTimeToCompleteBooking <= TimeSpan.Zero)
            {
                viewModel.OutOfTime = true;
            }
            else
            {
                viewModel.ReservationExpiryMinutes = remainingTimeToCompleteBooking.Minutes;
                viewModel.ReservationExpirySeconds = remainingTimeToCompleteBooking.Seconds;
            }

            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsUserLoggedIn = true;
                var userDetails = _userManager.GetCurrentUser(this.User);
                viewModel.FirstName = userDetails.FirstName;
                viewModel.LastName = userDetails.LastName;
                viewModel.Phone = userDetails.Phone;
                viewModel.PostCode = userDetails.Postcode;
                viewModel.Email = userDetails.Email;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookTickets(BookTicketsViewModel bookTicketsViewModel)
        {
            return View();
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            #region To View Model

            // To View Model
            configuration.CreateMap<Business.Events.EventModel, EventViewDetailsModel>()
                .ForMember(member => member.EventStartDate, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToLongDateString()))
                .ForMember(member => member.EventStartTime, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToString("hh:mm tt")))
                .ForMember(member => member.EventEndDate, options => options.ResolveUsing(src => src.EventEndDate.GetValueOrDefault().ToLongDateString()))
                ;

            configuration.CreateMap<Business.Search.AdSearchResult, EventViewDetailsModel>()
                .ForMember(m => m.Posted, options => options.PreCondition(src => src.BookingDate.HasValue))
                .ForMember(m => m.Posted, options => options.MapFrom(src => src.BookingDate.Value.Humanize(false, null)))
                .ForMember(m => m.Title, options => options.MapFrom(src => src.Heading))
                .ForMember(m => m.OrganiserName, options => options.MapFrom(src => src.ContactName))
                .ForMember(m => m.OrganiserPhone, options => options.MapFrom(src => src.ContactPhone))
                .ForMember(m => m.Views, options => options.MapFrom(src => src.NumOfViews))
                .ForMember(m => m.EventPhoto, options => options.MapFrom(src => src.ImageUrls.FirstOrDefault()))
                ;

            configuration.CreateMap<Business.Events.EventTicket, EventTicketViewModel>().ReverseMap();
            configuration.CreateMap<Business.IClientConfig, EventViewDetailsModel>().ForMember(m => m.MaxTicketsPerBooking, options => options.MapFrom(src => src.EventMaxTicketsPerBooking));
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketRequestViewModel>();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketReservedViewModel>()
                .ForMember(m => m.Status, options => options.MapFrom(s => s.StatusAsString.Humanize()))
                .ForMember(m => m.Price, options => options.MapFrom(s => s.EventTicket.Price))
                .ForMember(m => m.TicketName, options => options.MapFrom(s => s.EventTicket.TicketName))
                ;
            #endregion

            #region From View model

            // From View Model
            configuration.CreateMap<EventTicketRequestViewModel, EventTicket>();
            configuration.CreateMap<EventTicketRequestViewModel, EventTicketReservationRequest>()
                .ConvertUsing(a => new EventTicketReservationRequest()
                {
                    Quantity = a.SelectedQuantity,
                    EventTicket = this.Map<EventTicketRequestViewModel, EventTicket>(a)
                });
            #endregion
        }
    }
}
