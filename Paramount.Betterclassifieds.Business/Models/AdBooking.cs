namespace Paramount.Betterclassifieds.Business
{
    using System.Collections.Generic;

    public class AdBooking
    {
        public int AdBookingId { get; set; }
        public int PublicationId { get; set; }
        public List<BookEntry> BookEntries { get; set; }
    }
}
