using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Paramount.Betterclassifieds.DataLayer
{
    using Business;
    using Configurations;

    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0)]
    public class BookingContext : DbContext
    {
        public BookingContext()
           // : base(ConnectionReader.GetConnectionString("paramount/services", "BetterclassifiedsConnection"))
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<AdBooking> AdBookings { get; set; }
        public IDbSet<BookEntry> BookEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdBookingConfiguration());
            modelBuilder.Configurations.Add(new BookEntryConfiguration());
        }
    }
}
