using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventViewModel
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Location { get; set; }

        public int? LocationLatitude { get; set; }

        public int? LocationLongitude { get; set; }

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
    }
}