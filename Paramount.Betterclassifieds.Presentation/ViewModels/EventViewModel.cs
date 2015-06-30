using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? LocationLatitude { get; set; }
        public int? LocationLongitude { get; set; }
        public string EventPhoto { get; set; }
        public string EventStartDate { get; set; }
        public int? EventStartTimeHours { get; set; }
        public int? EventStartTimeMinutes { get; set; }

        public string EventEndDate { get; set; }
        public int? EventEndTimeHours { get; set; }
        public int? EventEndTimeMinutes { get; set; }
    }
}