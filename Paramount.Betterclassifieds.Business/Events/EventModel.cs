﻿using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventModel : ICategoryAd
    {
        public int EventId { get; set; }
        public int OnlineAdId { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public virtual ICollection<EventTicket> Tickets { get; set; }
    }
}
