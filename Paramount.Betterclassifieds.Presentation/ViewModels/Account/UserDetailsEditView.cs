using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserDetailsEditView
    {
        public SelectList StateList
        {
            get
            {
                IEnumerable<SelectListItem> items = new[]
                {
                    new SelectListItem {Text = "VIC", Value = "VIC"},
                    new SelectListItem {Text = "NSW", Value = "NSW"},
                    new SelectListItem {Text = "QLD", Value = "QLD"},
                    new SelectListItem {Text = "SA", Value = "SA"},
                    new SelectListItem {Text = "WA", Value = "WA"},
                    new SelectListItem {Text = "ACT", Value = "ACT"},
                    new SelectListItem {Text = "TAS", Value = "TAS"}
                };

                return new SelectList(items);
            }
        }

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
    }
}