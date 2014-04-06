namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AccountConfirmationViewModel
    {
        public bool IsSuccessfulConfirmation { get; set; }
        public bool RegistrationExpiredOrNotExists { get; set; }
        public bool DuplicateUsernameOrEmail { get; set; }
        public bool AccountAlreadyConfirmed { get; set; }
        public string Username { get; set; }
    }
}