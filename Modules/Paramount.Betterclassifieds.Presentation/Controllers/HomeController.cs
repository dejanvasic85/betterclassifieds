using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Models;

    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(new List<AdSummaryView>
            {
                new AdSummaryView{
                    AdId = 100,
                    Title = "THE WAILERS, SLY & ROBBIE ANNOUNCE BLUESFEST SIDESHOWS",
                    Description = "Bluesfest boasts some seriously impressive reggae on its line up for 2014 and one of the biggest acts on the bill has locked in three sideshows next April",
                    ParentCategoryName = "Festival",
                    ChildCategoryName = "Tours",
                    PostedDate = DateTime.Today
                }
            });
        }
    }
}
