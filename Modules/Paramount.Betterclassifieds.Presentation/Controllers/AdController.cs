using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdRepository adRepository;
        private readonly ISeoMappingRepository seoMappingRepository;

        public AdController(IAdRepository adRepository, ISeoMappingRepository seoMappingRepository)
        {
            this.adRepository = adRepository;
            this.seoMappingRepository = seoMappingRepository;
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

        public ActionResult CreateCategorySeoNameTest(string seoName, int categoryId)
        {
            seoMappingRepository.CreateCategoryMapping(seoName, categoryId);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}