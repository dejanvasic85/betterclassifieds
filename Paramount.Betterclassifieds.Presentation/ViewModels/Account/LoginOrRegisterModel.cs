using System;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class LoginOrRegisterModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public string LoginHelpMessage { get; set; }
    }

    public class LoginOrRegisterModelFactory
    {
        private readonly IClientConfig _clientConfig;
        private readonly IApplicationConfig _appConfig;

        public LoginOrRegisterModelFactory(IClientConfig clientConfig, IApplicationConfig appConfig)
        {
            _clientConfig = clientConfig;
            _appConfig = appConfig;
        }

        public LoginOrRegisterModel Create(string returnUrl)
        {
            return new LoginOrRegisterModel
            {
                LoginHelpMessage = CreateLoginHelpMessage(returnUrl),
                LoginViewModel = new LoginViewModel { ReturnUrl = returnUrl },
                RegisterViewModel = new RegisterViewModel
                {
                    ReturnUrl = returnUrl,
                    GoogleCaptchaEnabled = _appConfig.GoogleCaptchaEnabled,
                    GoogleCaptchaKey = _appConfig.GoogleRegistrationCatpcha.Key
                }
            };
        }

        public LoginOrRegisterModel Create(LoginViewModel loginViewModel)
        {
            return new LoginOrRegisterModel
            {
                LoginHelpMessage = CreateLoginHelpMessage(loginViewModel.ReturnUrl),
                LoginViewModel = loginViewModel,
                RegisterViewModel = new RegisterViewModel
                {
                    ReturnUrl = loginViewModel.ReturnUrl,
                    GoogleCaptchaEnabled = _appConfig.GoogleCaptchaEnabled,
                    GoogleCaptchaKey = _appConfig.GoogleRegistrationCatpcha.Key
                }
            };
        }

        public LoginOrRegisterModel Create(RegisterViewModel registerViewModel)
        {
            registerViewModel.GoogleCaptchaKey = _appConfig.GoogleRegistrationCatpcha.Key;
            registerViewModel.GoogleCaptchaEnabled = _appConfig.GoogleCaptchaEnabled;

            return new LoginOrRegisterModel
            {
                LoginHelpMessage = CreateLoginHelpMessage(registerViewModel.ReturnUrl),
                LoginViewModel = new LoginViewModel { ReturnUrl = registerViewModel.ReturnUrl },
                RegisterViewModel = registerViewModel
            };
        }

        private string CreateLoginHelpMessage(string returnUrl)
        {
            if (returnUrl.HasValue() && returnUrl.EndsWith("Event/BookTickets", StringComparison.OrdinalIgnoreCase))
            {

                return $"Your tickets have been reserved for {_clientConfig.EventTicketReservationExpiryMinutes} minutes. " +
                       "Please login or create an account before purchasing the tickets.";
            }

            return string.Empty;
        }
    }
}