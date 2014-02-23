using Paramount.Betterclassifieds.Presentation.Models;
using System;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AdController : Controller
    {
        //private readonly IAdRepository adRepository;
        //private readonly ISeoMappingRepository seoMappingRepository;

        //public AdController(IAdRepository adRepository, ISeoMappingRepository seoMappingRepository)
        //{
        //    this.adRepository = adRepository;
        //    this.seoMappingRepository = seoMappingRepository;
        //}


        public ActionResult Index()
        {
            var adModel = new AdModel()
            {
                AdId = 139,
                Title = "THE WAILERS, SLY & ROBBIE ANNOUNCE BLUESFEST SIDESHOWS",
                Description = "Bluesfest boasts some seriously impressive reggae on its line up for 2014 and one of the biggest acts on the bill has locked in three sideshows next April",
                ParentCategoryName = "Festival",
                ChildCategoryName = "Rock",
                Publications = new[] { "The Music Perth", "The Music Sydney" },
                ContactDetail = "dude@gmail.com",
                ContactName = "Mister Dude",
                PostedDate = DateTime.Today,
                HitCount = 10,
                ImageUrl = ""
            };

            ViewBag.Title = adModel.Title;

            return View(adModel);
        }
    }
}