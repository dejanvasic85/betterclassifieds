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

        public EventPromoController(IEventPromoService eventPromoService, ISearchService searchService)
        {
            _eventPromoService = eventPromoService;
            _searchService = searchService;
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
            var factory = new EventPromoViewModelFactory();
            var vm = factory.CreateManageViewModel(ad.AdId, eventId, promoCodes);

            return View(vm);
        }
    }
}