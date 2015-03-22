namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using CaptchaMvc.Attributes;
    using CaptchaMvc.HtmlHelpers;
    using CaptchaMvc.Interface;
    using Business;
    using Business.Broadcast;
    using Business.Search;
    using ViewModels;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly IClientConfig _clientConfig;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IEnquiryManager _enquiryManager;

        public HomeController(ISearchService searchService, IClientConfig clientConfig, IBroadcastManager broadcastManager, IEnquiryManager enquiryManager)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _broadcastManager = broadcastManager;
            _enquiryManager = enquiryManager;
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
            var contactUs = new ContactUsView();
            ViewBag.Address = this.Map<Address, AddressViewModel>(_clientConfig.ClientAddress);
            ViewBag.PhoneNumber = _clientConfig.ClientPhoneNumber;
            ViewBag.AddressLatitude = _clientConfig.ClientAddressLatLong.Item1;
            ViewBag.AddressLongitude = _clientConfig.ClientAddressLatLong.Item2;

            return View(contactUs);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, CaptchaVerify("Captcha is not valid")]
        public ActionResult ContactUs(ContactUsView contactUsView)
        {
            if (!ModelState.IsValid)
            {
                IUpdateInfoModel updatedCaptcha = this.GenerateCaptchaValue(5);
                return Json(new
                {
                    isValid = false,
                    imageElementId = updatedCaptcha.ImageUrl,
                    tokenElementId = updatedCaptcha.TokenValue
                });
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

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("HomeControllerProfile");
            configuration.CreateMap<AdSearchResult, AdSummaryViewModel>();
            configuration.CreateMap<Address, AddressViewModel>();
        }
    }
}
