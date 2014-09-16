using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PublicationEditionView
    {
        public int PublicationId { get; set; }

        public DateTime EditionDate { get; set; }

        public bool IsSelected { get; set; }
    }
}