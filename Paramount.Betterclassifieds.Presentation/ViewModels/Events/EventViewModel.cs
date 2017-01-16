using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "yyyy-MM-dd H:mm")]
        public DateTime? AdStartDate { get; set; }

        [MaxLength(100)]
        public string VenueName { get; set; }

        [Required(ErrorMessage = "Location is required and must be displayed")]
        public string Location { get; set; }

        public decimal? LocationLatitude { get; set; }

        public decimal? LocationLongitude { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
        public long TimeZoneDaylightSavingsOffsetSeconds { get; set; }
        public long TimeZoneUtcOffsetSeconds { get; set; }

        public string EventPhoto { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd H:mm")]
        public DateTime EventStartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd H:mm")]
        public DateTime EventEndDate { get; set; }

        [Required]
        public string OrganiserName { get; set; }

        public string OrganiserPhone { get; set; }

        public bool TicketingEnabled { get; set; }
        public string LocationFloorPlanDocumentId { get; set; }
        public string LocationFloorPlanFilename { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            
            if (this.EventEndDate <= this.EventStartDate)
            {
                validationResults.Add(new ValidationResult("End date must be after the start date", new[] { "EventStartDate" }));
            }

            return validationResults;
        }
    }
}