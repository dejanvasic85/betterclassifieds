using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Models;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BaseController : Controller
    {
        private readonly LegacyIntegration.OnlineSearchParameter _legacySearchParameter;

        public BaseController(LegacyIntegration.OnlineSearchParameter legacySearchParameter)
        {
            _legacySearchParameter = legacySearchParameter;
        }

        [HttpGet]
        public ActionResult GetCategorySearchOptions(int? parentId)
        {
            var model = new []
            {
                new CategorySearchModel{ CategoryId = null, CategoryName = "All Categories" },
                new CategorySearchModel{ CategoryId = 1, CategoryName = "Tutoring" },
                new CategorySearchModel{ CategoryId = 2, CategoryName = "Musicians Wanted" }
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword, int? searchCategoryId)
        {
            // Currently this is legacy integration
            // So just set the session parameter to the search and redirect...
            _legacySearchParameter["SearchKeywordParam"] = searchKeyword;
            _legacySearchParameter["CategoryIdParam"] = searchCategoryId;

            return Redirect(LegacyIntegration.LegacyLinks.SearchResults);
        }
    }
}