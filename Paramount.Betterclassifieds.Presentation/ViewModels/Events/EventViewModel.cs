using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Paramount.Betterclassifieds.Mvc.Validators;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventViewModel : IValidatableObject
    {
        public EventViewModel()
        {
            TicketingEnabled = true;
            CanEdit = true;
        }

        public int EventId { get; set; }

        public bool CanEdit { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AdStartDate { get; set; }

        [Required(ErrorMessage = "Location is required and must be displayed")]
        public string Location { get; set; }

        public decimal? LocationLatitude { get; set; }

        public decimal? LocationLongitude { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
        public long TimeZoneDaylightSavingsOffsetSeconds { get; set; }
        public long TimeZoneUtcOffsetSeconds { get; set; }

        public string LocationFloorPlanDocumentId { get; set; }

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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var startDateTime = new ServerDateService().ConvertFromString(EventStartDate, EventStartTime);
            var endDateTime = new ServerDateService().ConvertFromString(EventEndDate, EventEndTime);

            if (endDateTime <= startDateTime)
            {
                validationResults.Add(new ValidationResult("End date must be after the start date", new[] { "EventStartDate" }));
            }

            return validationResults;
        }
    }
}