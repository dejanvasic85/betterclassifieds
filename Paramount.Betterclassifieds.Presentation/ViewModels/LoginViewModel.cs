using System.ComponentModel.DataAnnotations;

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
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string RegisterEmail { get; set; }

        [Required]
        [Display(Name = "Confirm Email")]
        [Compare("RegisterEmail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string RegisterPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        [StringLength(6)]
        public string PostCode { get; set; }
    }
}