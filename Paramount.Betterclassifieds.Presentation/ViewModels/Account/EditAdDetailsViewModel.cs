using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Framework;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EditAdDetailsViewModel
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
        [StringLength(10)]
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

        [Display(Name = "Start Date")]
        [RequiredIf("IsFutureScheduledAd", true)]
        public DateTime? StartDate { get; set; }

        public bool IsFutureScheduledAd { get; set; }

        #endregion

        #region Print Ad Details

        public bool IsLineAdIncluded { get; set; }
        public bool IsPrintDescriptionBooked { get; set; }
        public bool IsPrintHeaderBooked { get; set; }
        public bool IsPrintImageBooked { get; set; }
        public int PrintWordsPurchased { get; set; }

        public List<string> OnlineAdImages { get; set; }

        [Display(Name = "Heading")]
        [RequiredIf("IsPrintHeaderBooked", true)]
        [StringLength(100)]
        public string LineAdHeader { get; set; }

        [Display(Name = "Description")]
        [RequiredIf("IsPrintDescriptionBooked", true)]
        [MaxWords("PrintWordsPurchased")]
        public string LineAdText { get; set; }

        public string LineAdImageId { get; set; }

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
                    LineAdImageId
                };

                var json = lineAd.ToJsonString();
                return json;
            }
        }

        #endregion

        #region Configuration

        public int ConfigDurationDays { get; set; }

        public int? MaxOnlineImages { get; set; }

        public int MaxImageUploadBytes { get; set; }

        public decimal MaxImageUploadInMegabytes
        {
            get { return MaxImageUploadBytes / (decimal)1000000; }
        }

        public int Id { get; set; }

        #endregion
    }
}