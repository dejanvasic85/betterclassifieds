using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AdController : Controller
    {
        public ActionResult Index(string title, int id)
        {
            return View();
        }
    }
}