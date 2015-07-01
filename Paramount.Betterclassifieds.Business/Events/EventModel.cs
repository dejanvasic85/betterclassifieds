﻿using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? LocationLatitude { get; set; }
        public int? LocationLongitude { get; set; }
        public string EventPhoto { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
    }
}
