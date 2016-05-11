using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        [ActionName("how-it-works")]
        public ActionResult How()
        {
            return View();
        }
    }
}