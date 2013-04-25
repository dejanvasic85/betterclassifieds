using System;
using System.Data.Linq;

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
        int AddBookingExtension(AdBookingExtensionModel extension);
        AdBookingExtensionModel GetBookingExtension(int extensionId);
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate);
        void AddBookEntries(BookEntryModel[] bookEntries);
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

        public int AddBookingExtension(AdBookingExtensionModel extension)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                AdBookingExtension adBookingExtension = this.Map<AdBookingExtensionModel, AdBookingExtension>(extension);
                context.AdBookingExtensions.InsertOnSubmit(adBookingExtension);
                context.SubmitChanges();
                extension.AdBookingExtensionId = adBookingExtension.AdBookingExtensionId;
                extension.AdBookingId = adBookingExtension.AdBookingId;
                return extension.AdBookingExtensionId;
            }
        }

        public AdBookingExtensionModel GetBookingExtension(int extensionId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var adBookingExtension = context.AdBookingExtensions.First(extension => extension.AdBookingExtensionId == extensionId);
                return this.Map<AdBookingExtension, AdBookingExtensionModel>(adBookingExtension);
            }
        }

        public void UpdateExtesion(int extensionId, int? status)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                // Fetch and update
                AdBookingExtension extension = context.AdBookingExtensions.First(e => e.AdBookingExtensionId == extensionId);
                if (status.HasValue)
                    extension.Status = status.Value;

                context.SubmitChanges();
            }
        }

        public void UpdateBooking(int adBookingId, DateTime? newEndDate)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var adBooking = context.AdBookings.First(booking => booking.AdBookingId == adBookingId);

                if (newEndDate.HasValue)
                    adBooking.EndDate = newEndDate.Value;

                // Update
                context.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }

        public void AddBookEntries(BookEntryModel[] bookEntries)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                context.BookEntries.InsertAllOnSubmit(this.MapList<BookEntryModel, BookEntry>(bookEntries.ToList()));
                context.SubmitChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From data
            configuration.CreateMap<AdBooking, AdBookingModel>();
            configuration.CreateMap<BookEntry, BookEntryModel>();
            configuration.CreateMap<AdBookingExtension, AdBookingExtensionModel>();

            // To data
            configuration.CreateMap<AdBookingExtensionModel, AdBookingExtension>()
                .ForMember(member => member.AdBookingExtensionId, options => options.Condition(con => con.AdBookingExtensionId > 0));
            configuration.CreateMap<BookEntryModel, BookEntry>()
                .ForMember(member => member.AdBooking, options => options.Ignore())
                .ForMember(member => member.Publication, options => options.Ignore());
        }
    }
}
