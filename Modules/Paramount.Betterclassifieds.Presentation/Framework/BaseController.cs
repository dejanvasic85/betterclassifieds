using System.Collections.Generic;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Models;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BaseController : Controller
    {
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
            Dictionary<string, object> sessionSearchParam = Session["OnlineSearchParam"] as Dictionary<string, object>;

            if (sessionSearchParam != null)
            {
                sessionSearchParam["SearchKeywordParam"] = searchKeyword;
                sessionSearchParam["CategoryIdParam"] = searchCategoryId;
            }

            return Redirect(LegacyIntegration.LegacyLinks.SearchResults);
        }
    }
}