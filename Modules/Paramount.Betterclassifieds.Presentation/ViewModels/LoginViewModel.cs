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
        public string RegisterUsername { get; set; }

        [Required]
        [Display(Name = "Confirm Email")]
        [Compare("RegisterUsername")]
        [DataType(DataType.EmailAddress)]
        public string ConfirmUsername { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
    }
}