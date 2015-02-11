using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class LocationController : Controller
    {
        private readonly ISearchService _searchService;

        public LocationController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult GetLocationOptions()
        {
            var list = _searchService.GetLocations()
                .Select(l => new SelectListItem { Text = l.Title, Value = l.LocationId.ToString() })
                .OrderBy(l => l.Text);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocationAreas(int? locationId)
        {
            if (!locationId.HasValue)
            {
                return Json(new[] { new SelectListItem { Value = null, Text = "Any Area" } }, JsonRequestBehavior.AllowGet);
            }

            var list = _searchService.GetLocationAreas(locationId.Value)
                .Select(l => new SelectListItem { Text = l.Title, Value = l.LocationAreaId.ToString() })
                .OrderBy(l => l.Text);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}