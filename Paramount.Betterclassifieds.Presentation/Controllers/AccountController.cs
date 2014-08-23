using AutoMapper;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business;
    using Business.Broadcast;
    using Business.Search;
    using ViewModels;

    public class AccountController : BaseController, IMappingBehaviour
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;
        private readonly IBroadcastManager _broadcastManager;

        public const string ReturnUrlKey = "ReturnUrlForLogin";

        public AccountController(IUserManager userManager, IAuthManager authManager, 
            IBroadcastManager broadcastManager, ISearchService searchService)
            : base(searchService)
        {
            _userManager = userManager;
            _authManager = authManager;
            _broadcastManager = broadcastManager;
        }

        [HttpGet]
        [RequireHttps]
        public ActionResult Login(string returnUrl = "")
        {
            if (_authManager.IsUserIdentityLoggedIn(this.User))
                return RedirectToAction("Index", "Home");

            if (returnUrl.HasValue())
            {
                TempData[ReturnUrlKey] = returnUrl;
            }

            // Render Login page
            return View(new LoginOrRegisterModel { LoginViewModel = new LoginViewModel { ReturnUrl = returnUrl } });
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
            if (!user.AuthenticateUser(_authManager, loginViewModel.Password, loginViewModel.RememberMe))
            {
                ModelState.AddModelError("BadPassword", "Username/email or password is invalid.");
                //return View(loginOrRegister);
                return View(new LoginOrRegisterModel { LoginViewModel = loginViewModel });
            }

            // Finally, the user is ok.. so redirect them appropriately
            if (TempData[ReturnUrlKey] != null)
            {
                return Redirect((string)TempData[ReturnUrlKey]);
            }

            if (loginViewModel.ReturnUrl.HasValue())
            {
                return Redirect(loginViewModel.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireHttps]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (_authManager.IsUserIdentityLoggedIn(this.User))
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
            registrationModel
                .GenerateUniqueUsername(_authManager.CheckUsernameExists)
                .GenerateToken()
                .SetPasswordFromPlaintext(viewModel.RegisterPassword);

            _authManager.CreateRegistration(registrationModel);

            _broadcastManager.SendEmail(new NewRegistration
            {
                FirstName = viewModel.FirstName,
                VerificationLink = Url.ActionAbsolute("Confirmation", "Account", new
                {
                    registrationModel.RegistrationId,
                    registrationModel.Token,
                    registrationModel.Username
                })
            }, viewModel.RegisterEmail);

            return View("ThankYou");
        }

        public ActionResult Confirmation(int registrationId, string token, string username)
        {
            // Fetch the registration record
            RegistrationModel registerModel = _authManager.GetRegistration(registrationId, token, username);

            if (registerModel == null || registerModel.HasExpired())
            {
                return View(new AccountConfirmationViewModel { RegistrationExpiredOrNotExists = true });
            }

            if (registerModel.HasConfirmedAlready())
            {
                return View(new AccountConfirmationViewModel { AccountAlreadyConfirmed = true });
            }

            if (_authManager.CheckEmailExists(registerModel.Email) || _authManager.CheckUsernameExists(registerModel.Username))
            {
                return View(new AccountConfirmationViewModel { DuplicateUsernameOrEmail = true, Username = registerModel.Username });
            }

            // Register 
            _authManager.CreateMembershipFromRegistration(registerModel);
            _userManager.CreateUserProfile(registerModel.Email, registerModel.FirstName, registerModel.LastName, registerModel.PostCode);

            // Login
            _authManager.Login(registerModel.Username, createPersistentCookie: false);

            return View(new AccountConfirmationViewModel { IsSuccessfulConfirmation = true });
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Simply call the auth manager to get out of forms auth
            _authManager.Logout();

            // Redirect to the home page - since this is where they can only hit this anyway
            return RedirectToAction("Index", "Home");
        }

        public JsonResult IsEmailUnique(string registerEmail)
        {
            return Json(!_authManager.CheckEmailExists(registerEmail), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ForgotPassword(string email)
        {
            var user = _userManager.GetUserByEmailOrUsername(email);

            if (user == null)
                return Json(new { Error = "The provided email is not valid or does not exist." });

            var password = _authManager.SetRandomPassword(user.Email);

            _broadcastManager.SendEmail(new ForgottenPassword
            {
                Email = email,
                Password = password,
                Username = user.Username
            }, email);

            return Json(new { Completed = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<RegisterViewModel, RegistrationModel>()
                .ForMember(member => member.Email, options => options.MapFrom(source => source.RegisterEmail));

        }
    }
}
