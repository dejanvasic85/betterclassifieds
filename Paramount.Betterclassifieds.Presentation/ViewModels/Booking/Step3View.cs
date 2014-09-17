using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// Scheduling
    /// </summary>
    public class Step3View
    {
        [Required]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        public PublicationEditionView[] Editions { get; set; } 

        public bool IsLineAdIncluded { get; set; }
        
        public int DurationDays { get; set; }
    }
}