using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Framework
{
    public class ApplicationController : Controller
    {
        protected JsonResult JsonModelErrors()
        {
            return Json(new { Errors = ModelState.ToErrors() });
        }
    }
}