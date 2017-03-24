namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using Business;
    using Business.Search;
    using ViewModels;
    using System.Linq;
    using System.Web.Mvc;
    using System.Text;
    using Services.Seo;
    using Services;

    public class HomeController : ApplicationController, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IClientConfig _clientConfig;
        private readonly IMailService _mailService;
        private readonly ISitemapFactory _sitemapProvider;

        public HomeController(ISearchService searchService, IClientConfig clientConfig, ISitemapFactory sitemapFactory, IMailService mailService)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _sitemapProvider = sitemapFactory;
            _mailService = mailService;
        }

        public ActionResult Index()
        {
            var results = _searchService.GetLatestAds(pageSize: 6);
            var adSummaryViewModels = this.MapList<AdSearchResult, AdSummaryViewModel>(results.ToList());

            return View(new HomeModel
            {
                AdSummaryList = adSummaryViewModels
            });
        }

        public ActionResult ContactUs()
        {
            var contactUs = new ContactUsView();
            ViewBag.Address = this.Map<Address, AddressViewModel>(_clientConfig.ClientAddress);
            ViewBag.PhoneNumber = _clientConfig.ClientPhoneNumber;
            ViewBag.AddressLatitude = _clientConfig.ClientAddressLatLong.Item1;
            ViewBag.AddressLongitude = _clientConfig.ClientAddressLatLong.Item2;

            return View(contactUs);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ContactUs(ContactUsView contactUsView)
        {
            if (!ModelState.IsValid)
            {
                return JsonModelErrors();
            }

            _mailService.SendSupportEmail(contactUsView);

            return Json(new { IsValid = true });
        }

        // Terms and conditions
        public ActionResult Terms()
        {
            return View();
        }

        [ActionName("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        [Route("sitemap.xml")]
        public ActionResult Sitemap()
        {
            var xml = _sitemapProvider.Create();
            return Content(xml, ContentType.Xml, Encoding.UTF8);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("HomeControllerProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
            configuration.CreateMap<Address, AddressViewModel>();
        }
    }
}
