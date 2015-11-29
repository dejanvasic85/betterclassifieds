using System;
using System.ComponentModel.DataAnnotations;
using Paramount.Betterclassifieds.Mvc.Validators;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class Step3View
    {
        [MustBeTrue(ErrorMessage = "You must agree to our terms and conditions")]
        [Display(Name = "I have read and agree to the terms and conditions")]
        public bool AgreeToTerms { get; set; }
        [MustBeTrue(ErrorMessage = "You must confirm your details")]
        [Display(Name = "I have ensured that the booking details are correct")]
        public bool DetailsAreCorrect { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string BookingReference { get; set; }
        public bool IsPaymentRequired
        {
            get { return this.TotalPrice > 0; }
        }
        public bool IsPaymentCancelled { get; set; }
        public DateTime? PrintFirstEditionDate { get; set; }
        public int? PrintInsertions { get; set; }
        public bool IsLineAdIncluded { get; set; }
        public int? PublicationCount { get; set; }
        public string PreviousStepUrl { get; set; }
    }
}