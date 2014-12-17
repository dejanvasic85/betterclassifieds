using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserDetailsEditView
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [StringLength(10)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)]
        [StringLength(6, ErrorMessage = "Please enter a valid postcode")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Postcode must be a number")]
        public string PostCode { get; set; }

        
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Phone number must be between 8 and 10 characters long")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone must be a number")]
        public string Phone { get; set; }

        public IEnumerable<SelectListItem> StateList
        {
            get
            {
                return new[]
                {
                    new SelectListItem {Text = "VIC", Value = "VIC"},
                    new SelectListItem {Text = "NSW", Value = "NSW"},
                    new SelectListItem {Text = "QLD", Value = "QLD"},
                    new SelectListItem {Text = "SA", Value = "SA"},
                    new SelectListItem {Text = "WA", Value = "WA"},
                    new SelectListItem {Text = "ACT", Value = "ACT"},
                    new SelectListItem {Text = "TAS", Value = "TAS"}
                }.AsEnumerable();
            }
        }
    }
}