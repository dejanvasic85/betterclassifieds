namespace Paramount.Betterclassifieds.Repository
{
    using System;
    using System.Collections.Generic;

    using Business;

    public interface IBookingRepository : IRepository<AdBooking>
    {
        List<AdBooking> GetBookingsForEdition(DateTime editionDate);
    }
}