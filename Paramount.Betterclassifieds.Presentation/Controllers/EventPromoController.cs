using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeEventAccess]
    [RoutePrefix("event-dashboard/{eventId}/promos")]

    public class EventPromoController : ApplicationController
    {
        private readonly IEventPromoService _eventPromoService;
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;

        public EventPromoController(IEventPromoService eventPromoService, ISearchService searchService, IEventManager eventManager)
        {
            _eventPromoService = eventPromoService;
            _searchService = searchService;
            _eventManager = eventManager;
        }

        [HttpGet]
        [ActionName("manage-promo-codes")]
        [Route("")]
        public ActionResult ManagePromoCodes(int eventId)
        {
            var searchResult = _searchService.GetEvent(eventId);
            if (searchResult == null)
                return Url.NotFound().ToRedirectResult();

            var ad = searchResult.AdSearchResult;
            var promoCodes = _eventPromoService.GetEventPromoCodes(eventId);
            var eventBookings = _eventManager.GetEventBookingsForEvent(eventId);
            var factory = new EventPromoViewModelFactory();
            var vm = factory.CreateManageViewModel(ad.AdId, eventId, promoCodes, eventBookings);

            return View(vm);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreatePromoCode(int eventId, EventPromoViewModel eventPromo)
        {
            if (!ModelState.IsValid)
            {
                return JsonModelErrors();
            }

            var promoCodeModel = _eventPromoService.CreateEventPromoCode(eventId, eventPromo.PromoCode, eventPromo.DiscountPercent);

            var factory = new EventPromoViewModelFactory();
            var vm = factory.CreatePromoCode(promoCodeModel);

            return Json(vm);
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult RemovePromoCode(int eventId, int eventPromoCodeId)
        {
            _eventPromoService.DisablePromoCode(eventPromoCodeId);

            return Json(true);
        }

    }
}