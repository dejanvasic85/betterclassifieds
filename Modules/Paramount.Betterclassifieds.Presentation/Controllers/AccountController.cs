using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Web.Mvc;

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
        [RequireHttps]
        public ActionResult Login(string returnUrl = "")
        {
            if (IsUserLoggedIn())
                return RedirectToAction("Index", "Home");

            if (returnUrl.HasValue())
                TempData[ReturnUrlKey] = returnUrl;

            // Render Login page
            return View(new LoginOrRegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireHttps]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("AlreadyLoggedIn", "You are already logged in!");
                return View();
            }

            var user = _userManager.GetUserByEmailOrUsername(loginViewModel.Username);

            if (user == null)
            {
                ModelState.AddModelError("EmailNotValid", "The username/email provided is not a registered user.");
                //return View(loginOrRegister);
                return View(new LoginOrRegisterModel {LoginViewModel = loginViewModel});
            }

            // Authenticate
            if (!user.AuthenticateUser(_authManager, loginViewModel.Password))
            {
                ModelState.AddModelError("BadPassword", "The password provided is not correct.");
                //return View(loginOrRegister);
                return View(new LoginOrRegisterModel { LoginViewModel = loginViewModel });
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
        [RequireHttps]
        public ActionResult Register(RegisterViewModel registerModel)
        {            
            if (this.IsUserLoggedIn())
            {
                ModelState.AddModelError("UserAlreadyRegistered", "You are already logged in.");
                return View("Login", new LoginOrRegisterModel { RegisterViewModel = registerModel });
            }

            if (!this.ModelState.IsValid)
            {
                return View("Login", new LoginOrRegisterModel { RegisterViewModel = registerModel });
            }

            // Create the membership, then create the profile
            _authManager.CreateMembership(registerModel.RegisterUsername, registerModel.RegisterPassword);
            _userManager.CreateUserProfile(registerModel.RegisterUsername, registerModel.FirstName,
                registerModel.LastName, registerModel.PostCode);
            
            // Login
            _authManager.Login(registerModel.RegisterUsername, false);

            // Finally, the user is registered and logged in.. so redirect them appropriately
            if (TempData[ReturnUrlKey] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect((string)TempData[ReturnUrlKey]);
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
