namespace Paramount.Betterclassifieds.Business
{
    using System;

    public class BookEntry
    {
        public int BookEntryId { get; set; }
        
        public int AdBookingId { get; set; }
        public AdBooking AdBooking { get; set; }

        public int PublicationId { get; set; }
        public DateTime EditionDate { get; set; }
    }
}