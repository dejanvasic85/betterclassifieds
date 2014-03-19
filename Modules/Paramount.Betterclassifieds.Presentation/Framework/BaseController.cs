using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BaseController : Controller
    {

        [HttpPost]
        public ActionResult Search(string searchKeyword, int? searchCategoryId)
        {

            OnlineSearchParam["SearchKeywordParam"] = searchKeyword;
            OnlineSearchParam["CategoryIdParam"] = searchCategoryId;

            return Redirect(LegacyIntegration.LegacyLinks.SearchResults);
        }


        // Currently this is legacy integration
        // So just set the session parameter to the search and redirect...
        // todo - legacy integration
        private Dictionary<string, object> OnlineSearchParam
        {
            get
            {
                Dictionary<string, object> sessionSearchParam =
                    Session["OnlineSearchParameter"] as Dictionary<string, object>;

                if (sessionSearchParam == null)
                {
                    sessionSearchParam = new Dictionary<string, object>();
                    Session["OnlineSearchParameter"] = sessionSearchParam;
                }

                return sessionSearchParam;
            }
        }

        public bool IsUserLoggedIn()
        {
            return this.User != null && this.User.Identity.IsAuthenticated;
        }
    }
}