using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Paramount.ApplicationBlock.Mvc.Validators;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class Step4View
    {
        public Step4View()
        {
            OnlineAdImages = new string[0];
        }
     
        [MustBeTrue(ErrorMessage = "You must agree to our terms and conditions")]
        [Display(Name = "I have read and agree to the terms and conditions")]
        public bool AgreeToTerms { get; set; }

        [MustBeTrue(ErrorMessage = "You must confirm your details")]
        [Display(Name = "I have ensured that the booking details are correct")]
        public bool DetailsAreCorrect { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OnlineAdDescriptionHtml { get; set; }
        public string OnlineAdHeading { get; set; }
        public string[] OnlineAdImages { get; set; }
    }
}