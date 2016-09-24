using System;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class LoginOrRegisterModel
    {
        public LoginOrRegisterModel()
        {
            LoginViewModel = new LoginViewModel();
            RegisterViewModel = new RegisterViewModel();
        }

        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }

        public string LoginHelpMessage { get; set; }
    }

    public class LoginMessageFactory
    {
        private readonly IClientConfig _clientConfig;

        public LoginMessageFactory(IClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public string Get(string returnUrl)
        {
            if (returnUrl.EndsWith("Event/BookTickets", StringComparison.OrdinalIgnoreCase))
            {

                return $"Your tickets have been reserved for {_clientConfig.EventTicketReservationExpiryMinutes} minutes. " +
                       "Please login or create an account before purchasing the tickets.";
            }

            return string.Empty;
        }
    }
}