using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            TicketingEnabled = true;
        }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AdStartDate { get; set; }

        [Required]
        public string Location { get; set; }

        public decimal? LocationLatitude { get; set; }

        public decimal? LocationLongitude { get; set; }

        public string EventPhoto { get; set; }

        [Required]
        public string EventStartDate { get; set; }

        [Required]
        [TimeAsString(ErrorMessage = "Start time is not in a valid format")]
        public string EventStartTime { get; set; }

        [Required]
        public string EventEndDate { get; set; }

        [Required]
        [TimeAsString(ErrorMessage = "End time is not in a valid format")]
        public string EventEndTime { get; set; }

        [Required]
        public string OrganiserName { get; set; }

        public string OrganiserPhone { get; set; }

        public bool TicketingEnabled { get; set; }
    }
}