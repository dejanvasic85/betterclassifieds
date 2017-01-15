﻿namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using Business;
    using Business.Broadcast;
    using Business.Search;
    using ViewModels;
    using System.Linq;
    using System.Web.Mvc;
    using System.Text;
    using Services.Seo;

    public class HomeController : ApplicationController, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IClientConfig _clientConfig;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IEnquiryManager _enquiryManager;
        private readonly ISitemapFactory _sitemapProvider;

        public HomeController(ISearchService searchService, IClientConfig clientConfig, IBroadcastManager broadcastManager, IEnquiryManager enquiryManager, ISitemapFactory sitemapFactory)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _broadcastManager = broadcastManager;
            _enquiryManager = enquiryManager;
            _sitemapProvider = sitemapFactory;
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
                return Json(new { isValid = false });
            }

            _broadcastManager.SendEmail(new SupportRequest
            {
                RequestDetails = contactUsView.Comment,
                Email = contactUsView.Email,
                Name = contactUsView.FullName,
                Phone = contactUsView.Phone
            }, _clientConfig.SupportEmailList);

            _enquiryManager.CreateSupportEnquiry(contactUsView.FullName,
                contactUsView.Email,
                contactUsView.Phone,
                contactUsView.Comment);

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
