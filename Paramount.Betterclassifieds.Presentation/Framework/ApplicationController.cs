using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public class ApplicationController : Controller
    {
        protected JsonResult JsonModelErrors()
        {
            return Json(new { Errors = ModelState.ToErrors() });
        }
    }
}