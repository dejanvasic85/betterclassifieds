using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class AdBooking
    {
        public AdBooking()
        {
            this.BookEntries = new List<BookEntry>();
        }

        public int AdBookingId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public string BookReference { get; set; }
        public Nullable<int> AdId { get; set; }
        public string UserId { get; set; }
        public Nullable<int> BookingStatus { get; set; }
        public Nullable<int> MainCategoryId { get; set; }
        public string BookingType { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<int> Insertions { get; set; }
        public virtual Ad Ad { get; set; }
        public virtual MainCategory MainCategory { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }
    }
}
