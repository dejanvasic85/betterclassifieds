using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ErrorController : ApplicationController
    {
        //
        // GET: /Error/NotFound

        public ActionResult NotFound()
        {
            return View();
        }

        // 
        // GET: /Error/Server

        public ActionResult ServerProblem()
        {
            return View();
        }
    }
}
