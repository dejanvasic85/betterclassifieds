using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserNetworkController : ApplicationController
    {
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;
        private readonly IMailService _mailService;

        public UserNetworkController(ISearchService searchService, IUserManager userManager, IMailService mailService)
        {
            _searchService = searchService;
            _userManager = userManager;
            _mailService = mailService.Initialise(this);
        }

        [HttpPost]
        public ActionResult NotifyAd(int adId, UserNetworkEmailView[] userNetworkUsers)
        {
            var adSearchResult = _searchService.GetByAdId(adId);

            _mailService.SendListingNotificationToUserNetwork(userNetworkUsers, adSearchResult);

            return Json(true);
        }

        [HttpPost]
        public ActionResult Create(UserNetworkEmailView viewModel)
        {
            if (!ModelState.IsValid)
            {
                return JsonModelErrors();;
            }

            // Adds a contact for the existing (logged in user)
            _userManager.CreateUserNetwork(User.Identity.Name, viewModel.Email, viewModel.FullName);
            return Json(true);
        }
    }
}