using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Framework;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    /// <summary>
    /// Ad Details
    /// </summary>
    public class Step2View
    {
        #region Online Ad Details
        
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

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        #endregion

        #region Print Ad Details
        
        public bool IsLineAdIncluded { get; set; }

        public List<string> OnlineAdImages { get; set; }
        
        [Display(Name = "Heading")]
        [StringLength(30)]
        [MaxLength(30)]
        public string LineAdHeader { get; set; }
        
        [Display(Name = "Description")]
        [RequiredIf("IsLineAdIncluded", true)]
        [AllowHtml]
        public string LineAdText { get; set; }
        
        public string LineAdImageId { get; set; }
        
        public DateTime? FirstPrintDateFormatted
        {
            get
            {
                if (FirstPrintDate.IsNullOrEmpty())
                    return null;

                return DateTime.ParseExact(FirstPrintDate, "dd/MM/yyyy", new DateTimeFormatInfo());
            }
        }
        
        [RequiredIf("IsLineAdIncluded", true)]
        [Display(Name = "First Print Edition")]
        public string FirstPrintDate { get; set; }

        [RequiredIf("IsLineAdIncluded", true)]
        [Display(Name = "Insertions")]
        public int PrintInsertions { get; set; }

        /// <summary>
        /// Returns Print Details as json string
        /// </summary>
        public string LineAdAsJson
        {
            get
            {
                if (!IsLineAdIncluded)
                {
                    return null;
                }
                var lineAd = new
                {
                    LineAdText,
                    LineAdHeader,
                    LineAdImageId,
                    PrintInsertions
                };

                var json = lineAd.ToJsonString();
                return json;
            }
        }

        #endregion

        #region Collections - Select Inputs
        public IEnumerable<SelectListItem> UpcomingEditions { get; set; }
        public IEnumerable<SelectListItem> AvailableInsertions { get; set; }

        #endregion

        #region Configuration

        public int ConfigDurationDays { get; set; }
        
        public int? MaxOnlineImages { get; set; }

        public int MaxImageUploadBytes { get; set; }

        public decimal MaxImageUploadInMegabytes
        {
            get { return MaxImageUploadBytes / (decimal)1000000; }
        }

        #endregion

    }
}