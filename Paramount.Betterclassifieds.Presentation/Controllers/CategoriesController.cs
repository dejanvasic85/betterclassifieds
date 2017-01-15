using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class CategoriesController : ApplicationController
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
                .Select(c => new CategoryView
                {
                    CategoryId = c.MainCategoryId,
                    Title = c.Title,
                    IsOnlineOnly = c.IsOnlineOnly,
                    FontIcon = c.FontIcon
                })
                .ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IsOnlineOnlyCategory(int id)
        {
            return Json(new { isOnlineOnly = _searchService.GetCategories().Single(c => c.MainCategoryId == id).IsOnlineOnly});
        }
    }
}
