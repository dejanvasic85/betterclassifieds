using System;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            // Initialise with start date and hours to be close to the current time
            var start = DateTime.Now;

            EventStartDate = start.ToString("dd/MM/yyyy");
            EventStartTime = "10:20";
            EventStartTimeHours = start.Hour;
            EventStartTimeMinutes = start.Minute;

            var end = start.AddHours(1);
            EventEndDate = end.ToString("dd/MM/yyyy");
            EventEndTimeHours = end.Hour;
            EventEndTimeMinutes = end.Minute;
        }

        public string EventStartTime { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
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