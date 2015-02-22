﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [AllowHtml]
        [Display(Name = "Details")]
        public string OnlineAdDescription { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(200)]
        public string OnlineAdContactName { get; set; }

        [Display(Name = "Contact Phone")]
        [StringLength(50)]
        public string OnlineAdContactPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Contact Email")]
        [StringLength(100)]
        public string OnlineAdContactEmail { get; set; }

        [Display(Name = "Price")]
        public decimal? OnlineAdPrice { get; set; }

        [Display(Name = "Location")]
        public int? OnlineAdLocationId { get; set; }

        [Display(Name = "Area")]
        public int? OnlineAdLocationAreaId { get; set; }

        public bool IsLineAdIncluded { get; set; }

        public List<string> OnlineAdImages { get; set; }

        public int? MaxOnlineImages { get; set; }

        public int MaxImageUploadBytes { get; set; }

        public decimal MaxImageUploadInMegabytes
        {
            get { return MaxImageUploadBytes / (decimal)1000000; }
        }

        public IEnumerable<SelectListItem> LocationOptions { get; set; }

        public IEnumerable<SelectListItem> LocationAreaOptions { get; set; }
        
        [Display(Name = "Heading")]
        [StringLength(100)]
        public string LineAdHeader { get; set; }
        
        [Display(Name = "Description")]
        [RequiredIf("IsLineAdIncluded", true)]
        [AllowHtml]
        public string LineAdText { get; set; }
        
        [Display(Name = "Super Bold Heading")]
        public bool LineIsSuperBoldHeading { get; set; }

        public string LineAdImageId { get; set; }
    }
}