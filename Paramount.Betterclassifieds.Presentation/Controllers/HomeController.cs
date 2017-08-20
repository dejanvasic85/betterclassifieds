namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using Business;
    using Business.Search;
    using ViewModels;
    using System.Web.Mvc;
    using System.Text;
    using Services.Seo;
    using Services.Mail;
    using Services;

    public class HomeController : ApplicationController, IMappingBehaviour
    {
        private readonly IClientConfig _clientConfig;
        private readonly IMailService _mailService;
        private readonly ISitemapFactory _sitemapProvider;
        private readonly IApplicationConfig _appConfig;
        private readonly IRobotVerifier _robotVerifier;

        public HomeController(IClientConfig clientConfig, ISitemapFactory sitemapFactory, IMailService mailService, IApplicationConfig appConfig, IRobotVerifier robotVerifier)
        {
            _clientConfig = clientConfig;
            _sitemapProvider = sitemapFactory;
            _appConfig = appConfig;
            _robotVerifier = robotVerifier;
            _mailService = mailService.Initialise(this);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            var contactUs = new ContactUsView
            {
                GoogleCaptchaEnabled = _appConfig.GoogleCaptchaEnabled,
                GoogleCaptchaKey = _appConfig.GoogleGeneralEnquiryCatpcha.Key
            };
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
            if (!_robotVerifier.IsValid(_appConfig.GoogleGeneralEnquiryCatpcha.Secret, Request))
            {
                AddModelErrorCaptchaFailed();
            }

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
            return View("Terms");
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
