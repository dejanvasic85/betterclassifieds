using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            Tickets = new List<EventTicketViewModel>();
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

        public List<EventTicketViewModel> Tickets { get; set; }
        public bool TicketingEnabled { get; set; }
        public int Views { get; set; }
    }

    public class EventTicketViewModel
    {
        public int? TicketId { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
    }
}