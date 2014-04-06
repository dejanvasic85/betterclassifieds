using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AccountController : BaseController, IMappingBehaviour
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;
        private readonly IBroadcastManager _broadcastManager;

        public const string ReturnUrlKey = "ReturnUrlForLogin";

        public AccountController(IUserManager userManager, IAuthManager authManager, IBroadcastManager broadcastManager)
        {
            _userManager = userManager;
            _authManager = authManager;
            _broadcastManager = broadcastManager;
        }

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
                ModelState.AddModelError("EmailNotValid", "Username/email or password is invalid.");
                //return View(loginOrRegister);
                return View(new LoginOrRegisterModel { LoginViewModel = loginViewModel });
            }

            // Authenticate
            if (!user.AuthenticateUser(_authManager, loginViewModel.Password))
            {
                ModelState.AddModelError("BadPassword", "Username/email or password is invalid.");
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
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (IsUserLoggedIn())
            {
                ModelState.AddModelError("UserAlreadyRegistered", "You are already logged in.");
                return View("Login", new LoginOrRegisterModel { RegisterViewModel = viewModel });
            }
            
            if (!ModelState.IsValid)
            {
                return View("Login", new LoginOrRegisterModel { RegisterViewModel = viewModel });
            }

            // Store the registration model temporarily with a registered token ( until the confirmation is completed )
            var registrationModel = this.Map<RegisterViewModel, RegistrationModel>(viewModel);
            registrationModel.GenerateUniqueUsername(_authManager.CheckUsernameExists)
                .GenerateToken();

            _authManager.CreateRegistration(registrationModel);

            _broadcastManager.SendEmail(new NewRegistration
            {
                FirstName = viewModel.FirstName,
                VerificationLink = Url.ActionAbsolute("Confirm", "Account", new { registrationModel.RegistrationId, registrationModel.Token })
            }, viewModel.RegisterEmail);

            return View("ThankYou");
        }

        public ActionResult Confirm(string registrationId, string token)
        {
            //_authManager.CreateMembership(registerModel.RegisterUsername, registerModel.RegisterPassword);
            //_userManager.CreateUserProfile(registerModel.RegisterUsername, registerModel.FirstName,
            //    registerModel.LastName, registerModel.PostCode);

            return View("Complete");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Simply call the auth manager to get out of forms auth
            _authManager.Logout();

            // Redirect to the home page - since this is where they can only hit this anyway
            return RedirectToAction("Index", "Home");
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<RegisterViewModel, RegistrationModel>()
                .ForMember(member => member.Email, options => options.MapFrom(source => source.RegisterEmail))
                .ForMember(member => member.Password, options => options.MapFrom(source => source.RegisterPassword));

        }
    }
}
