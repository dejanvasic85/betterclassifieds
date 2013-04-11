using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UIController.Repository
{
    public interface IBookingRepository
    {
        AdBooking GetBooking(int id);
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
    }
}
