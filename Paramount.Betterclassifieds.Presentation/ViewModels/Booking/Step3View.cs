using System;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// Scheduling
    /// </summary>
    public class Step3View
    {
        [Required]
        public DateTime? StartDate { get; set; }

        public PublicationEditionView[] Editions { get; set; } 

        public bool IsLineAdIncluded { get; set; }
        
        public int DurationDays { get; set; }
    }
}