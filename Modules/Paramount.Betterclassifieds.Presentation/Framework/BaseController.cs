using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BaseController : Controller, IMappingBehaviour
    {

        [HttpPost]
        public ActionResult Search(string searchKeyword, int? searchCategoryId)
        {
            // Currently this is legacy integration
            // So just set the session parameter to the search and redirect...
            // todo - legacy integration
            Dictionary<string, object> sessionSearchParam = Session["OnlineSearchParam"] as Dictionary<string, object>;

            if (sessionSearchParam != null)
            {
                sessionSearchParam["SearchKeywordParam"] = searchKeyword;
                sessionSearchParam["CategoryIdParam"] = searchCategoryId;
            }

            return Redirect(LegacyIntegration.LegacyLinks.SearchResults);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            
        }
    }
}