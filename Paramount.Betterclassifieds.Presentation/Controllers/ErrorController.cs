using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/NotFound

        public ActionResult NotFound()
        {
            return View();
        }

        // 
        // GET: /Error/Server

        public ActionResult Server()
        {
            return View();
        }
    }
}
