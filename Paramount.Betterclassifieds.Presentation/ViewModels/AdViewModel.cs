using System;
using System.Collections.Generic;
using Humanizer;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdViewModel
    {
        public AdViewModel()
        {
            AdEnquiry = new AdEnquiryViewModel();
        }
        public int AdId { get; set; }
        public int OnlineAdId { get; set; }
        public string Heading { get; set; }
        public string HeadingSlug { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public String[] ImageUrls { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }
        public DateTime? BookingDate { get; set; }
        public string[] Publications { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string LocationName { get; set; }
        public string LocationAreaName { get; set; }
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public int NumOfViews { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Price { get; set; }

        public string PriceFriendlyDisplay
        {
            get
            {
                if (Price.HasValue && Price.Value > 0)
                {
                    return string.Format("{0:N}", Price);
                }
                return string.Empty;
            }
        }

        public bool HasImages
        {
            get { return this.ImageUrls != null && this.ImageUrls.Length > 0; }
        }

        public bool ShowImageSlideshow
        {
            get { return this.HasImages && this.ImageUrls.Length > 1; }
        }
        
        public AdEnquiryViewModel AdEnquiry { get; set; }

        public string PostedDate
        {
            get
            {
                if (BookingDate.HasValue)
                {
                    // Booking date has a time component, but start date does not
                    // So if the dates are the same day, then use the booking date
                    if (BookingDate.Value.Day == StartDate.Day)
                        return BookingDate.Value.Humanize(utcDate: false);
                }
                return StartDate.Humanize(utcDate: false);
            }
        }

        public bool VisitorIsTheOwner { get; set; }
    }
}