using System.Linq;
using Paramount.Betterclassifieds.Business.Search;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ISearchService _searchService;

        public CategoriesController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult GetCategoryOptions(bool includeAllOption = true)
        {
            var list = _searchService.GetTopLevelCategories()
                .Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() })
                .OrderBy(c => c.Text)
                .ToList();

            if (includeAllOption)
                list.Insert(0, new SelectListItem { Text = "All Categories", Value = string.Empty });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocationOptions()
        {
            var list = _searchService.GetLocations()
                .Select(l => new SelectListItem { Text = l.Title, Value = l.LocationId.ToString() })
                .OrderBy(l => l.Text)
                .ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
