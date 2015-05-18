using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AccountConfirmationViewModel
    {
        [Required]
        public int? RegistrationId { get; set; }
        public string ReturnUrl { get; set; }
        [Required]
        [Display(Name = "Confirmation Code")]
        public int Token { get; set; }
        public bool TokenNotValid { get; set; }
    }
}