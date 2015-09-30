using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;
        private readonly HttpContextBase _httpContext;

        public EventController(ISearchService searchService, IEventManager eventManager, HttpContextBase httpContext)
        {
            _searchService = searchService;
            _eventManager = eventManager;
            _httpContext = httpContext;
        }

        public ActionResult ViewEventAd(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetAdById(id);

            if (onlineAdModel == null)
            {
                return View("~/Views/Listings/404.cshtml");
            }

            var eventModel = _eventManager.GetEventDetailsForOnlineAdId(onlineAdModel.OnlineAdId);
            var eventViewModel = this.Map<Business.Events.EventModel, EventViewDetailsModel>(eventModel);
            this.Map(onlineAdModel, eventViewModel);

            return View(eventViewModel);
        }

        [HttpPost]
        public ActionResult ReserveTickets(List<EventTicketReservervationViewModel> tickets)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, Errors = ModelState.ToErrors() });
            }

            var ticketRequests = this.MapList<EventTicketReservervationViewModel, EventTicketReservationRequest>(tickets);

            var result = _eventManager.ReserveTickets(_httpContext.With(s => s.Session).SessionID, ticketRequests);
            
            return Json(new { IsValid = true, Redirect = Url.Action("BookTickets") });
        }

        [HttpGet]
        public ActionResult BookTickets()
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
            #endregion

            #region From View model
            // From View Model
            configuration.CreateMap<EventTicketReservervationViewModel, EventTicket>();
            configuration.CreateMap<EventTicketReservervationViewModel, EventTicketReservationRequest>()
                .ConvertUsing(a => new EventTicketReservationRequest()
                {
                    Quantity = a.SelectedQuantity,
                    EventTicket = this.Map<EventTicketReservervationViewModel, EventTicket>(a)
                });
            #endregion
        }
    }
}
