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
                .OrderBy(l => l.Text)
                .ToList();

            // Set the any location to a null value
            var anyLocation = list.FirstOrDefault(l => l.Text.Contains("Any Location"));
            if (anyLocation != null)
            {
                anyLocation.Value = null;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}