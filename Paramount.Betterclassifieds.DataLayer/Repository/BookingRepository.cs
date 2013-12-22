namespace Paramount.Betterclassifieds.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Business;
    using Repository;

    public class BookingRepository : EntityRepository<BookingContext, AdBooking>, IBookingRepository
    {
        public List<AdBooking> GetBookingsForEdition(DateTime editionDate)
        {
            return Context.BookEntries
                .Where(bookEntry => bookEntry.EditionDate == editionDate)
                .Select(bookEntry => bookEntry.AdBooking).ToList();
        }
    }
}
