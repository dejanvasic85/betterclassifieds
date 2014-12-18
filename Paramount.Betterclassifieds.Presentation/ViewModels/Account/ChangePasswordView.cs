using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class ChangePasswordView
    {
        public bool UpdatedSuccessfully { get; set; }
        public bool PasswordIsNotValid { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 50 characters long")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 50 characters long")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 50 characters long")]
        [Compare("NewPassword", ErrorMessage = "Does not match new password")]
        public string ConfirmNewPassword { get; set; }
    }
}