using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
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
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 50 characters long")]
        public string RegisterPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        [StringLength(6, ErrorMessage = "Please enter a valid postcode")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Postcode must be a number")]
        public string PostCode { get; set; }
        
        
        [Display(Name = "How did you hear about us")]
        [StringLength(100)]
        public string HowYouFoundUs { get; set; }

        public IEnumerable<SelectListItem> HowYouFoundUsOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem {Text = "African Music and Culture Festival"},
                    new SelectListItem {Text = "Google"},
                    new SelectListItem {Text = "Friend"},
                    new SelectListItem {Text = "Other"},
                };
            }
        }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone must be a number")]
        [StringLength(12)]
        public string Phone { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}