namespace Paramount.Betterclassifieds.Presentation.Services.Mail
{
    public static class Views
    {
        private static string Folder = "~/Views/Email/";
        
        public static string EventOrganiserView = $"{Folder}EventOrganiserInvite.cshtml";
        public static string EventPurchaserNotificationView = $"{Folder}EventTicketBuyer.cshtml";
        public static string EventBookingInvoiceView = "~/Views/Templates/Invoice.cshtml";
        public static string WelcomeView = $"{Folder}Welcome.cshtml";
        public static string ForgotPasswordView = $"{Folder}ForgotPassword.cshtml";
        public static string EventGuestTicketView = $"{Folder}EventTicketGuest.cshtml";
        public static string EventTicketTransferView = $"{Folder}EventTicketTransfer.cshtml";
        public static string EventGuestRemoved = $"{Folder}EventGuestRemoved.cshtml";
        public static string EventPaymentRequestView = $"{Folder}EventPaymentRequest.cshtml";
        public static string EventOrganiserIdentityConfirmation = $"{Folder}EventOrganiserIdentityConfirmation.cshtml";
        public static string ConfirmationEmail = $"{Folder}RegistrationConfirmation.cshtml";
        public static string ListingCompleteView = $"{Folder}ListingCompleteView.cshtml";
        public static string SupportEmailView = $"{Folder}SupportEmailView.cshtml";
        public static string NewListingNetworkView = $"{Folder}NewListingNetworkView.cshtml";
        public static string ContactAdvertiser = $"{Folder}ContactAdvertiser.cshtml";
        
    }
}