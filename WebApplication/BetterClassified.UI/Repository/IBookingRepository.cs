namespace BetterClassified.UI.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BetterclassifiedsCore.DataModel;
    using Models;

    public interface IBookingRepository
    {
        AdBookingModel GetBooking(int id);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
    }

    public class BookingRepository : IBookingRepository, IMappingBehaviour
    {
        public AdBookingModel GetBooking(int id)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var booking = context.AdBookings.FirstOrDefault(b => b.AdBookingId == id);
                if (booking == null)
                    return null;

                return this.Map<AdBooking, AdBookingModel>(booking);
            }
        }

        public List<BookEntryModel> GetBookEntriesForBooking(int adBookingId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return this.MapList<BookEntry, BookEntryModel>(context.BookEntries.Where(entries => entries.AdBookingId == adBookingId).ToList());
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdBooking, AdBookingModel>();
            configuration.CreateMap<BookEntry, BookEntryModel>();
        }
    }
}
