using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }
    }
}