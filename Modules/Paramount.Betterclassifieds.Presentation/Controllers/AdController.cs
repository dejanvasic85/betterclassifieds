using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdRepository adRepository;

        public AdController(IAdRepository adRepository)
        {
            this.adRepository = adRepository;
        }

        public ActionResult Index(string title, int id)
        {
            return View();
        }


        public ActionResult CategoryTest(int[] catIds)
        {
            var adList = adRepository.GetOnlineAdsByCategory(catIds.EmptyIfNull().ToList());
            return Json(adList, JsonRequestBehavior.AllowGet);
        }
    }
}