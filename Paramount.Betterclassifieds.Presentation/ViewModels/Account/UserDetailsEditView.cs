using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Payment;

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

        [Display(Name = "Preferred Payment Method")]
        [MustBeOneOf("None", "PayPal", "DirectDebit")]
        public string PreferredPaymentMethod { get; set; }

        [Display(Name = "PayPal Email")]
        [EmailAddress]
        [MaxLength(255)]
        [RequiredIf("PreferredPaymentMethod", PaymentType.PayPal, ValidationMessage = "Please provide PayPal email if preferred method is PayPal")]
        public string PayPalEmail { get; set; }

        [Display(Name = "Bank Name")]
        [RequiredIf("PreferredPaymentMethod", PaymentType.DirectDebit, ValidationMessage = "Please provide Bank Name if preferred method is Direct Debit")]
        public string BankName { get; set; }

        [Display(Name = "Account Name")]
        [RequiredIf("PreferredPaymentMethod", PaymentType.DirectDebit, ValidationMessage = "Please provide Account Name if preferred method is Direct Debit")]
        public string BankAccountName { get; set; }

        [Display(Name = "Account Number")]
        [RequiredIf("PreferredPaymentMethod", PaymentType.DirectDebit, ValidationMessage = "Please provide Account Number if preferred method is Direct Debit")]
        public string BankAccountNumber { get; set; }

        [Display(Name = "BSB")]
        [RequiredIf("PreferredPaymentMethod", PaymentType.DirectDebit, ValidationMessage = "Please provide BSB if preferred method is Direct Debit")]
        public string BankBsbNumber { get; set; }
    }
}