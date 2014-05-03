using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [EmailAddress]
        [Remote("IsEmailUnique", "Account", ErrorMessage = "Email provided is not available.")]
        public string RegisterEmail { get; set; }

        [Required]
        [Display(Name = "Confirm Email")]
        [System.ComponentModel.DataAnnotations.Compare("RegisterEmail", ErrorMessage = "Email and Confirm email do not match")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [EmailAddress]
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
        [System.ComponentModel.DataAnnotations.Compare("RegisterPassword", ErrorMessage = "Confirm Password and Password do not match")]
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
        [StringLength(6, ErrorMessage = "Please enter a valid postcode")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Postcode must be a number")]
        public string PostCode { get; set; }
    }
}