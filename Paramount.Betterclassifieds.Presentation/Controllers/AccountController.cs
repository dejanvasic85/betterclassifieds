namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using AutoMapper;
    using Business;
    using System.Web.Mvc;
    using ViewModels;
    using Services.Mail;
    using Services;
    using System.Linq;

    public class AccountController : ApplicationController, IMappingBehaviour
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;
        private readonly IMailService _mailService;
        private readonly IApplicationConfig _appConfig;
        private readonly IGoogleCaptchaVerifier _googleCaptchaVerifier;
        private readonly LoginOrRegisterModelFactory _loginOrRegisterModelFactory;

        public const string ReturnUrlKey = "ReturnUrlForLogin";

        public AccountController(IUserManager userManager, IAuthManager authManager, IMailService mailService, IGoogleCaptchaVerifier googleCaptchaVerifier, LoginOrRegisterModelFactory loginOrRegisterModelFactory, IApplicationConfig appConfig)
        {
            _userManager = userManager;
            _authManager = authManager;
            _googleCaptchaVerifier = googleCaptchaVerifier;
            _loginOrRegisterModelFactory = loginOrRegisterModelFactory;
            _appConfig = appConfig;
            _mailService = mailService.Initialise(this);
        }

        [HttpGet]
        [RequireHttps]
        public ActionResult Login(string returnUrl = "")
        {
            if (_authManager.IsUserIdentityLoggedIn(User))
            {
                return Url.Home().ToRedirectResult();
            }

            if (returnUrl.HasValue())
            {
                TempData[ReturnUrlKey] = returnUrl;
            }

            var loginOrRegisterModel = _loginOrRegisterModelFactory.Create(returnUrl);

            return View(loginOrRegisterModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireHttps]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (_authManager.IsUserIdentityLoggedIn(User))
            {
                ModelState.AddModelError("AlreadyLoggedIn", "You are already logged in!");
                return View();
            }

            var user = _userManager.GetUserByEmailOrUsername(loginViewModel.Username);

            if (user == null)
            {
                ModelState.AddModelError("EmailNotValid", "Username/email or password is invalid.");
                //return View(loginOrRegister);
                return View(_loginOrRegisterModelFactory.Create(loginViewModel));
            }

            // Authenticate
            if (!user.AuthenticateUser(_authManager, loginViewModel.Password, loginViewModel.RememberMe))
            {
                ModelState.AddModelError("BadPassword", "Username/email or password is invalid.");
                //return View(loginOrRegister);
                return View(_loginOrRegisterModelFactory.Create(loginViewModel));
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
            if (_authManager.IsUserIdentityLoggedIn(User))
            {
                ModelState.AddModelError("UserAlreadyRegistered", "You are already logged in.");
                return View("Login", _loginOrRegisterModelFactory.Create(viewModel));
            }

            if (!ModelState.IsValid)
            {
                return View("Login", _loginOrRegisterModelFactory.Create(viewModel));
            }

            if (!_googleCaptchaVerifier.IsValid(_appConfig.GoogleRegistrationCatpcha.Secret, Request))
            {
                ModelState.AddModelError("Captcha", "Please click 'I'm not a robot' to continue.");
                return View("Login", _loginOrRegisterModelFactory.Create(viewModel));
            }

            var registrationResult = _userManager.RegisterUser(this.Map<RegisterViewModel, RegistrationModel>(viewModel), viewModel.RegisterPassword);

            if (registrationResult.RequiresConfirmation)
            {
                // Send email
                var confirmationCode = registrationResult.Registration.Token;
                _mailService.SendRegistrationConfirmationEmail(registrationResult.Registration.Email, confirmationCode);

                return View("Confirmation", new AccountConfirmationViewModel
                {
                    RegistrationId = registrationResult.Registration.RegistrationId,
                    ReturnUrl = TempData[ReturnUrlKey]?.ToString() ?? string.Empty,
                });
            }

            // Send welcome email
            _mailService.SendWelcomeEmail(registrationResult.Registration.Email, registrationResult.Registration.Username);

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

        [HttpPost, Authorize]
        public ActionResult Find(string email)
        {
            var user = _userManager.GetUserByEmail(email);

            return Json(user != null);
        }

        public JsonResult ForgotPassword(string email)
        {
            var user = _userManager.GetUserByEmailOrUsername(email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "The provided email does not exist or is invalid");
                return JsonModelErrors();
            }

            var password = _authManager.SetRandomPassword(user.Email);

            _mailService.SendForgotPasswordEmail(email, password);

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

        [HttpGet]
        [ActionName("confirm-event-organiser")]
        [Authorize]
        public ActionResult ConfirmEventOrganiser()
        {
            return View(new EventOrganiserConfirmationViewModel());
        }

        [ActionName("confirm-event-organiser")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmEventOrganiser(EventOrganiserConfirmationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var attachments = viewModel.Files.Select(f => MailAttachment.New(f.FileName, f.InputStream.ToBytes(), f.ContentType));
            _mailService.SendEventOrganiserIdentityConfirmation(attachments);

            viewModel.IsSubmitted = true;
            return View(viewModel);
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
