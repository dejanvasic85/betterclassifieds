using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Models;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IAdRepository _adRepository;

        //public HomeController(IAdRepository adRepository)
        //{
        //    _adRepository = adRepository;
        //}

        //
        // GET: /Home/
        //public ActionResult Index()
        //{
        //    // Fetch the ads from the repository
        //    var ads = _adRepository.GetLatestAds(takeLast: 10);

        //    return View(new List<AdSummaryView>());
        //}

        public ActionResult Index()
        {
            return View(new List<AdSummaryModel>
            {
                new AdSummaryModel{
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
