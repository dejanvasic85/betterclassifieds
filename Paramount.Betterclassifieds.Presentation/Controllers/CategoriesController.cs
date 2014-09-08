using System.Linq;
using Paramount.Betterclassifieds.Business.Search;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;

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

        public ActionResult GetCategories(int? parentId)
        {
            var list = _searchService.GetCategories()
                .Where(c => c.ParentId == parentId)
                .Select(c => new CategoryView { CategoryId = c.MainCategoryId, Title = c.Title })
                .ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
