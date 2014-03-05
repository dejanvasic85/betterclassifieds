using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.Presentation.Models;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IAdRepository adRepository;
        private readonly ISeoMappingRepository seoMappingRepository;
        private readonly IBookingRepository bookingRepository;

        public ListingsController(IAdRepository adRepository, ISeoMappingRepository seoMappingRepository, IBookingRepository bookingRepository)
        {
            this.adRepository = adRepository;
            this.seoMappingRepository = seoMappingRepository;
            this.bookingRepository = bookingRepository;
        }

        //
        // GET: /Listings/
        //e.g: listings/employment
        public ActionResult Index(string seoName, int index = 0, int pageSize = 10)
        {
            var onlineAds = new List<ListingSummaryViewModel>();
            if (string.IsNullOrEmpty(seoName))
            {
                return View(onlineAds);
            }

            var seoModel = GetMappingModel(seoName);

            if (seoModel == null)
            {
                return View(onlineAds);
            }

            onlineAds = GetListingBySeo(seoModel, index, pageSize).Select(a => new ListingSummaryViewModel(a)).ToList();

            return View(onlineAds);
        }

        public ActionResult TestIndex(string seoName, int index = 0, int pageSize = 25)
        {
            if (string.IsNullOrEmpty(seoName))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            SeoNameMappingModel seoModel = GetMappingModel(seoName);

            if (seoModel == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            List<OnlineListingModel> onlineAds = GetListingBySeo(seoModel, index, pageSize);

            return Json(onlineAds, JsonRequestBehavior.AllowGet); ;
        }

        private List<OnlineListingModel> GetListingBySeo(SeoNameMappingModel seoModel, int index = 0, int pageSize = 25)
        {
            List<OnlineListingModel> onlineAds = adRepository.SearchOnlineListing(seoModel.SearchTerm, seoModel.CategoryIds, seoModel.LocationIds,
               seoModel.AreaIds, index, pageSize);

            return onlineAds;
        }

        private SeoNameMappingModel GetMappingModel(string seoName)
        {
            SeoNameMappingModel seo = seoMappingRepository.GetSeoMapping(seoName);
            return seo;
        }

       
    }
}
