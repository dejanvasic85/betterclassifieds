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

        [Obsolete]
        public BookingType BookingType { get; set; }

        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }

        public bool IsExpired
        {
            get { return EndDate < DateTime.Today; }
        }

        public BookingStatusType BookingStatus { get; set; }

        public string BookReference { get; set; }

        public string ExtensionReference
        {
            get { return string.Format("{0}EX", BookReference); }
        }


        public List<IAd> Ads { get; set; }

        public OnlineAdModel OnlineAd
        {
            // There can only be a single online ad per booking 
            get { return Ads.OfType<OnlineAdModel>().SingleOrDefault(); }
        }

        public LineAdModel LineAd
        {
            // There can only be a single Line Ad per booking
            get { return Ads.OfType<LineAdModel>().SingleOrDefault(); }
        }

        public bool HasLineAd { get { return this.LineAd != null; } }
    }
}