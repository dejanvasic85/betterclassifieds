using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class BookEntry
    {
        public int BookEntryId { get; set; }
        public Nullable<System.DateTime> EditionDate { get; set; }
        public Nullable<int> AdBookingId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<decimal> EditionAdPrice { get; set; }
        public Nullable<decimal> PublicationPrice { get; set; }
        public Nullable<int> BaseRateId { get; set; }
        public string RateType { get; set; }
        public virtual AdBooking AdBooking { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
