﻿using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventBookedViewModel
    {
        public string EventName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string OrganiserName { get; set; }
        public string OrganiserEmail { get; set; }
        public string Address { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string EventUrl { get; set; }
    }
}