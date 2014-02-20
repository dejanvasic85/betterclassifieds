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
            return View(new HomeModel
            {
                AdSummaryList = new List<AdSummaryModel>{
                    new AdSummaryModel
                    {
                        AdId = 100,
                        Title = "THE WAILERS, SLY & ROBBIE ANNOUNCE BLUESFEST SIDESHOWS",
                        Description = "Bluesfest boasts some seriously impressive reggae on its line up for 2014 and one of the biggest acts on the bill has locked in three sideshows next April",
                        ParentCategoryName = "Festival",
                        Publications = new []{"The Music Sydney"}
                    },
                    new AdSummaryModel
                    {
                        AdId = 184,
                        Title = "Rock drummer seeking band",
                        Description = "Hey Im a 20 year old drummer in sydney been in god knows how bands since I began drumming. have been drumming for nearly 7 years.I love performing live and would love to join a band that has the same amount of energy and like mindedness + great music and performs. Check out some of the bands/EP's i'm in here: https://soundcloud.com/hindley-ermel",
                        ParentCategoryName = "Musicians wanted",
                        Publications = new []{"The Music Melbourne"}
                    },
                    new AdSummaryModel
                    {
                        AdId = 139,
                        Title = "THE WAILERS, SLY & ROBBIE ANNOUNCE BLUESFEST SIDESHOWS",
                        Description = "Bluesfest boasts some seriously impressive reggae on its line up for 2014 and one of the biggest acts on the bill has locked in three sideshows next April",
                        ParentCategoryName = "Festival",
                        Publications = new []{"The Music Perth", "The Music Sydney"}
                    },
                }
            });
        }
    }
}
