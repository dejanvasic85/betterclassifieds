using System;
using System.Threading;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class UserNetworkController : Controller
    {
        private readonly ISearchService _searchService;

        public UserNetworkController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult NotifyAd()
        {
            var viewModel = new UserNetworkNotifierView
            {
                AdId = 10,
                Users = new[]
                {
                    new UserNetworkEmailView {Email = "hello@world.com", FullName = "Hello World", Selected = true},
                    new UserNetworkEmailView {Email = "foo@bar.com", FullName = "Foo Bar", Selected = true},
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NotifyAd(int adId, UserNetworkEmailView[] userNetworkUsers)
        {
            Thread.Sleep(2000);
            return Json(true);
        }

        [HttpPost]
        public ActionResult Create(UserNetworkEmailView viewModel)
        {
            return Json(true);
        }
    }
}