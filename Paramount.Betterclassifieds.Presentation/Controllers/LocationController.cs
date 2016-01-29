using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Location;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class LocationController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly ILocationService _locationService;

        public LocationController(ISearchService searchService, ILocationService locationService)
        {
            _searchService = searchService;
            _locationService = locationService;
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
            var list = _searchService.GetLocationAreas(locationId)
                .Select(l => new SelectListItem { Text = l.Title, Value = l.LocationAreaId.ToString() })
                .OrderBy(l => l.Text);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimezoneInfo(decimal latitude, decimal longitude)
        {
            // var request = WebRequest.Create("https://maps.googleapis.com/maps/api/timezone/json?location=39.6034810,-119.6822510&timestamp=1331161200&key=AIzaSyBKrlWK9lGZBmkeHoPHAXHa1YjY_pd_z3I");
            var zoneData = _locationService.GetTimezone(latitude, longitude);
            return Json(zoneData, JsonRequestBehavior.AllowGet);
        }
    }
}