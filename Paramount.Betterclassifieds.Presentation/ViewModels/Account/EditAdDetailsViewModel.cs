using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Mvc.Validators;
using Paramount.Betterclassifieds.Presentation.Framework;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EditAdDetailsViewModel
    {
        public EditAdDetailsViewModel()
        { }

        public EditAdDetailsViewModel(int id, IClientConfig clientConfig, OnlineAdModel onlineAd, AdBookingModel adBooking, IApplicationConfig applicationConfig)
        {
            Id = id;
            
            MaxOnlineImages = clientConfig.MaxOnlineImages > onlineAd.Images.Count ? clientConfig.MaxOnlineImages : onlineAd.Images.Count;
            MaxImageUploadBytes = applicationConfig.MaxImageUploadBytes;
            ConfigDurationDays = clientConfig.RestrictedOnlineDaysCount;
            StartDate = adBooking.StartDate;
            IsFutureScheduledAd = adBooking.StartDate >= DateTime.Today;
            OnlineAdImages = onlineAd.Images.Select(a => a.DocumentId).ToList();
        }

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
        [MustNotBePastDate(ErrorMessage = "Start date cannot be a past date")]
        public DateTime? StartDate { get; set; }

        public bool IsFutureScheduledAd { get; set; }

        #endregion

        #region Print Ad Details

        public bool IsLineAdIncluded { get; set; }
        public bool HeaderPurchased { get; set; }
        public bool PhotoPurchased { get; set; }
        public int LineWordsPurchased { get; set; }

        public List<string> OnlineAdImages { get; set; }

        [Display(Name = "Heading")]
        [RequiredIf("HeaderPurchased", true)]
        [StringLength(30)]
        [MaxLength(30)]
        public string LineAdHeader { get; set; }

        [Display(Name = "Description")]
        [RequiredIf("IsLineAdIncluded", true)]
        [MaxWords("LineWordsPurchased")]
        [AllowHtml]
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