using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class AdBookingModel
    {
        public AdBookingModel()
        {
            Ads = new List<IAd>();
        }

        public int AdBookingId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? SubCategoryId { get; set; }

        public int? CategoryId { get; set; }

        public int Insertions { get; set; }

        [Obsolete]
        public BookingType BookingType { get; set; }

        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }

        public bool IsExpired => EndDate < DateTime.Today;

        public bool IsFutureAd => StartDate > DateTime.Today;

        public bool IsCurrentAd => !IsFutureAd && !IsExpired;

        public BookingStatusType BookingStatus { get; set; }

        public string BookReference { get; set; }

        public string ExtensionReference => $"{BookReference}EX";


        public List<IAd> Ads { get; set; }

        public OnlineAdModel OnlineAd => Ads.OfType<OnlineAdModel>().SingleOrDefault();

        public LineAdModel LineAd => Ads.OfType<LineAdModel>().SingleOrDefault();

        public bool HasLineAd => this.LineAd != null;

        public List<Enquiry> Enquiries { get; set; }
        public int[] Publications { get; set; }
        public string CategoryAdType { get; set; }
        public string CategoryFontIcon { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }
    }
}