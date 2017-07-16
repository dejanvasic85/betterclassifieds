using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public abstract class ApplicationController : Controller
    {
        protected JsonResult JsonModelErrors()
        {
            return Json(new { Errors = ModelState.ToErrors() });
        }

        protected void AddModelErrorCaptchaFailed()
        {
            ModelState.AddModelError("Captcha", "Please click 'I'm not a robot' to continue.");
        }
    }
}