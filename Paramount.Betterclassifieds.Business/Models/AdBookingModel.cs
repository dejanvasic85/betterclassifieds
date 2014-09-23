﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Models
{
    public class AdBookingModel
    {
        public AdBookingModel()
        {
            Ads = new List<IAd>();
            Publications = new List<PublicationModel>();
        }

        public int AdBookingId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

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

        public List<PublicationModel> Publications { get; set; }

        public Category Category { get; set; }

        public List<IAd> Ads { get; set; }

        public OnlineAdModel OnlineAd
        {
            // There can only be a single online ad per booking 
            get { return Ads.OfType<OnlineAdModel>().FirstOrDefault(); }
        }

        public LineAdModel LineAd
        {
            // There can only be a single Line Ad per booking
            get { return Ads.OfType<LineAdModel>().FirstOrDefault(); }
        }
    }
}