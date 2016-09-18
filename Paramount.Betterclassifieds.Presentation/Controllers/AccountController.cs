namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using Business;
    using Business.Broadcast;
    using System.Web.Mvc;
    using ViewModels;

    public class AccountController : Controller, IMappingBehaviour
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
            if (_authManager.IsUserIdentityLoggedIn(this.User))
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

            return Url.Home().ToRedirectResult();
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

            var registrationResult = _userManager.RegisterUser(this.Map<RegisterViewModel, RegistrationModel>(viewModel), viewModel.RegisterPassword);

            if (registrationResult.RequiresConfirmation)
            {
                return View("Confirmation", new AccountConfirmationViewModel
                {
                    RegistrationId = registrationResult.Registration.RegistrationId,
                    ReturnUrl = TempData[ReturnUrlKey]?.ToString() ?? string.Empty,
                });
            }

            if (viewModel.ReturnUrl.HasValue())
            {
                return Redirect(viewModel.ReturnUrl);
            }

            return Url.Home().ToRedirectResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmation(AccountConfirmationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = _userManager.ConfirmRegistration(viewModel.RegistrationId.GetValueOrDefault(), viewModel.Token.ToString());

            if (result != RegistrationConfirmationResult.Successful)
            {
                ModelState.AddModelError("Token", "Token is not valid or expired.");
                return View(viewModel);
            }

            if (viewModel.ReturnUrl.HasValue())
            {
                return Redirect(viewModel.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Simply call the auth manager to get out of forms auth
            _authManager.Logout();

            // Redirect to the home page - since this is where they can only hit this anyway
            return RedirectToAction("Index", "Home");
        }

        // Todo this should be a post and not a GET! No wonder it was caching the result (BAD)
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
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

        [Authorize]
        public ActionResult Details()
        {
            var applicationUser = _userManager.GetCurrentUser(this.User);

            UserDetailsEditView viewModel = this.Map<ApplicationUser, UserDetailsEditView>(applicationUser);

            ViewBag.Updated = false;
            ViewBag.ModelStateNotValid = false;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Details(UserDetailsEditView userDetailsView)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.Updated = false;
                ViewBag.ModelStateNotValid = true;
                return View(userDetailsView);
            }

            var applicationUser = this.Map<UserDetailsEditView, ApplicationUser>(userDetailsView);
            applicationUser.Username = this.User.Identity.Name;
            _userManager.UpdateUserProfile(applicationUser);

            ViewBag.Updated = true;
            ViewBag.ModelStateNotValid = false;
            return View(userDetailsView);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            var changePasswordView = new ChangePasswordView();

            return View(changePasswordView);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordView changePasswordView)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordView);
            }

            // Validate the existing password
            if (!_authManager.ValidatePassword(this.User.Identity.Name, changePasswordView.OldPassword))
            {
                changePasswordView.PasswordIsNotValid = true;
                return View(changePasswordView);
            }

            _authManager.SetPassword(this.User.Identity.Name, changePasswordView.NewPassword);

            changePasswordView.UpdatedSuccessfully = true;
            return View(changePasswordView);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("accountCtrlMap");

            // From View Model
            configuration.CreateMap<RegisterViewModel, RegistrationModel>()
                .ForMember(member => member.Email, options => options.MapFrom(source => source.RegisterEmail));
            configuration.CreateMap<UserDetailsEditView, ApplicationUser>();

            // To View Model
            configuration.CreateMap<ApplicationUser, UserDetailsEditView>();
        }

    }
}
