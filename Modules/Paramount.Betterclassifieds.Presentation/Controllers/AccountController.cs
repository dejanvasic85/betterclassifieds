using Paramount.Betterclassifieds.Business.Managers;
using System;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;

        public const string ReturnUrlKey = "ReturnUrlForLogin";

        public AccountController(IUserManager userManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        //
        // GET: /Account/

        // Todo - Home page for the user ( part of the membership pages - could be a SPA!)
        //public ActionResult Index()
        //{
        //    return View();
        //}


        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (returnUrl.HasValue())
                TempData[ReturnUrlKey] = returnUrl;

            // Render Login page
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(String.Empty, "You are already logged in!");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userManager.GetUserByEmailOrUsername(loginModel.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or Email is not a registered member.");
                return View();
            }

            // Authenticate
            if (!user.AuthenticateUser(_authManager, loginModel.Password))
            {
                ModelState.AddModelError(string.Empty, "Username and Password are not valid");
                return View();
            }

            // Finally, the user is ok.. so redirect them appropriately
            if (TempData[ReturnUrlKey] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect((string)TempData[ReturnUrlKey]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            // Todo - as part of the next gen pages 
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Simply call the auth manager to get out of forms auth
            _authManager.Logout();

            // Redirect to the home page - since this is where they can only hit this anyway
            return RedirectToAction("Index", "Home");
        }
    }
}
