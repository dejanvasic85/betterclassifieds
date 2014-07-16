using AutoMapper;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class HomeController : BaseController, IMappingBehaviour
    {
        private readonly Business.Managers.IClientConfig _clientConfig;

        public HomeController(ISearchService searchService, Business.Managers.IClientConfig clientConfig)
            : base(searchService)
        {
            _clientConfig = clientConfig;
        }

        public ActionResult Index()
        {
            var results = _searchService.GetLatestAds();

            return View(new HomeModel
            {
                AdSummaryList = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList())
            });
        }

        public ActionResult ContactUs()
        {
            var contactUs = new ContactUsModel();
            ViewBag.Address = this.Map<Business.Models.Address, AddressViewModel>(_clientConfig.ClientAddress);
            return View(contactUs);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsModel contactUsModel)
        {
            // Todo - Email the support team
            // Todo - Store record of the contact in the database
            
            return Json(new {IsValid = true});
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("HomeControllerProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
            configuration.CreateMap<Business.Models.Address, AddressViewModel>();
        }
    }
}
