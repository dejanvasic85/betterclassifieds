using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// Ad Details
    /// </summary>
    public class Step2View
    {
        [Required]
        [Display(Name = "Heading")]
        [StringLength(255)]
        public string OnlineAdHeading { get; set; }

        [Required]
        [Display(Name = "Details")]
        public string OnlineAdDescription { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(200)]
        public string OnlineAdContactName { get; set; }

        [Display(Name = "Contact Phone")]
        [StringLength(50)]
        public string OnlineAdPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Contact Email")]
        [StringLength(100)]
        public string OnlineAdEmail { get; set; }

        [Display(Name = "Price")]
        public decimal? OnlineAdPrice { get; set; }

        public int? OnlineAdLocationAreaId { get; set; }

        public bool IsLineAdIncluded { get; set; }

        public List<string> OnlineAdImages { get; set; }
    }
}