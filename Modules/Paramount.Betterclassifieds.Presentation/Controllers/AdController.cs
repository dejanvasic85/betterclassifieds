using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
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



        //
        // GET: /Category/

        public ActionResult Category(string title = "")
        {
            var mapping = seoMappingRepository.GetCategoryMapping(title);
            IEnumerable<int> catids = null;
            if (mapping != null)
            {
                catids = mapping.CategoryIdList;
            }

            return Json(GetAdsByCategory(catids.EmptyIfNull().ToList()), JsonRequestBehavior.AllowGet);
        }

        #region Private methods

        private List<OnlineAdModel> GetAdsByCategory(List<int> catIds)
        {
            return catIds.IsNullOrEmpty() ? adRepository.GetLatestAds(25) : adRepository.GetOnlineAdsByCategory(catIds);
        }

        #endregion  
    }
}