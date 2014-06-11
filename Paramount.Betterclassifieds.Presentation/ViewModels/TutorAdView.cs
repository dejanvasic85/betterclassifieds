using System.ComponentModel.DataAnnotations;
using WebGrease.Extensions;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class TutorAdView
    {
        public int? AgeGroupMin { get; set; }
        public int? AgeGroupMax { get; set; }

        [Display(Name = "Expertise")]
        public string ExpertiseLevel { get; set; }

        [Display(Name = "Travel")]
        public string TravelOption { get; set; }

        [Display(Name = "Pricing")]
        public string PricingOption { get; set; }

        [Display(Name = "What to bring")]
        public string WhatToBring { get; set; }

        public string Subjects { get; set; }

        public string[] SubjectsAsArray
        {
            get
            {
                if (this.Subjects.HasValue())
                    return Subjects.Split(',');
                return null;
            }
        }

        public string Objective { get; set; }

        [Display(Name = "Age Group")]
        public string AgeGroupDescription
        {
            get
            {
                if (!AgeGroupMax.HasValue && !AgeGroupMin.HasValue)
                {
                    return "Open for all";
                }

                if (!AgeGroupMax.HasValue && AgeGroupMin.HasValue)
                {
                    return string.Format("{0} years and older", AgeGroupMin.Value);
                }

                if (AgeGroupMax.HasValue && !AgeGroupMin.HasValue)
                {
                    return string.Format("Up to {0} years of age", AgeGroupMax.Value);
                }

                return string.Format("From {0} to {1} years of age", 
                    AgeGroupMin.Value,
                    AgeGroupMax.Value );
            }
        }
    }
}