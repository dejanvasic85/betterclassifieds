using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UI.Repository
{
    public interface IBookingRepository
    {
        AdBooking GetBooking(int id);
        List<BookEntry> GetBookEntriesForBooking(int adBookingId);
    }

    public class BookingRepository : IBookingRepository
    {
        public AdBooking GetBooking(int id)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return context.AdBookings.First(b => b.AdBookingId == id);
            }
        }

        public List<BookEntry> GetBookEntriesForBooking(int adBookingId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return context.BookEntries.Where(entries => entries.AdBookingId == adBookingId).ToList();
            }
        }
    }
}
