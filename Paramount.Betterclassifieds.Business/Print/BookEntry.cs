namespace Paramount.Betterclassifieds.Business.Print
{
    using Booking;
    using System;

    public class BookEntry
    {
        public int BookEntryId { get; set; }
        
        public int AdBookingId { get; set; }
        public AdBookingModel AdBooking { get; set; }

        public int PublicationId { get; set; }
        public DateTime EditionDate { get; set; }
    }
}