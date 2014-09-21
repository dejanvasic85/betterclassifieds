using System.ComponentModel.DataAnnotations;
using Paramount.ApplicationBlock.Mvc.Validators;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class Step4View
    {
     
        [MustBeTrue]
        [Display(Name = "I have read and agree to the terms and conditions")]
        public bool AgreeToTerms { get; set; }

        [MustBeTrue]
        [Display(Name = "I have ensured that the booking details are correct")]
        public bool DetailsAreCorrect { get; set; }
    }
}