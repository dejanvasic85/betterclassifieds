using System;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserBookingViewModel
    {
        public UserBookingViewModel()
        { }

        public UserBookingViewModel(AdBookingModel ad, UrlHelper urlHelper)
        {
            AdId = ad.AdBookingId;
            CategoryAdType = ad.CategoryAdType;
            AdImageId = ad.OnlineAd.PrimaryImageId != null ? ad.OnlineAd.PrimaryImageId.DocumentId : string.Empty;
            Heading = ad.OnlineAd.Heading.TruncateOnWordBoundary(35);
            Description = ad.OnlineAd.Description.TruncateOnWordBoundary(200);
            Starts = ad.StartDate.ToString("dd-MMM-yyyy");
            Ends = ad.EndDate.ToString("dd-MMM-yyyy");
            TotalPrice = ad.TotalPrice;
            Status = GetViewStatusFrom(ad);
            Visits = ad.OnlineAd.NumOfViews;
            AdViewUrl = urlHelper.AdUrl(Slug.Create(true, ad.OnlineAd.Heading), ad.AdBookingId, routeName: ad.CategoryAdType);

            Messages = ad.Enquiries.Select(enq => new AdEnquiryViewModel
            {
                FullName = enq.FullName,
                AdId = ad.AdBookingId,
                Email = enq.Email,
                Question = enq.EnquiryText,
                CreatedDate = enq.CreatedDate.ToString("dd-MMM-yyyy")
            })
                .OrderByDescending(enq => enq.CreatedDate)
                .ToArray();
        }

        public string AdViewUrl { get; set; }

        private string GetViewStatusFrom(AdBookingModel ad)
        {
            if (ad.IsExpired)
                return "Expired";

            if (ad.IsFutureAd)
                return "Scheduled";

            if (ad.IsCurrentAd)
                return "Current";

            throw new ArgumentException("Unable to retrieve view status from booking");
        }

        public int AdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string AdImageId { get; set; }
        public string Starts { get; set; }
        public string Ends { get; set; }
        public int Visits { get; set; }
        public int MessageCount { get { return this.Messages.Length; } }
        public string Status { get; set; }
        public AdEnquiryViewModel[] Messages { get; set; }
        public decimal TotalPrice { get; set; }
        public string CategoryAdType { get; set; }
    }

}