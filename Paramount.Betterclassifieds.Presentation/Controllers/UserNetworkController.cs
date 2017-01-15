using System;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class UserNetworkController : ApplicationController
    {
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;

        public UserNetworkController(ISearchService searchService, IUserManager userManager, IBroadcastManager broadcastManager)
        {
            _searchService = searchService;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
        }

        [HttpPost]
        public ActionResult NotifyAd(int adId, UserNetworkEmailView[] userNetworkUsers)
        {
            var adSearchResult = _searchService.GetByAdId(adId);

            foreach (var friendEmail in userNetworkUsers.Where(u => u.Selected).Select(u => u.Email))
            {
                _broadcastManager.Queue(new AdShare
                {
                    AdvertiserName = adSearchResult.ContactName,
                    AdDescription = adSearchResult.HtmlText,
                    AdTitle = adSearchResult.Heading,
                    ClientName = friendEmail
                }, friendEmail);
            }
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