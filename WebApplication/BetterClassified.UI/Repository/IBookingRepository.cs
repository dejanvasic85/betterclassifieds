namespace BetterClassified.UI.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BetterclassifiedsCore.DataModel;
    using Models;
    using System;
    using System.Data.Linq;

    public interface IBookingRepository
    {
        AdBookingModel GetBooking(int id, bool withLineAd = false);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
        int AddBookingExtension(AdBookingExtensionModel extension);
        AdBookingExtensionModel GetBookingExtension(int extensionId);
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void AddBookEntries(BookEntryModel[] bookEntries);
    }

    public class BookingRepository : IBookingRepository, IMappingBehaviour
    {
        public AdBookingModel GetBooking(int id, bool withLineAd = false)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var booking = context.AdBookings.FirstOrDefault(b => b.AdBookingId == id);
                if (booking == null)
                    return null;

                AdBookingModel model = this.Map<AdBooking, AdBookingModel>(booking);

                // Fetch line ad if required
                if (withLineAd)
                {
                    var design = booking.Ad.AdDesigns.FirstOrDefault(d => d.LineAds.Any());
                    if (design != null)
                        model.LineAd = this.Map<LineAd, LineAdModel>(design.LineAds.First());
                }

                return model;
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
                var adBookingExtension = context.AdBookingExtensions.FirstOrDefault(extension => extension.AdBookingExtensionId == extensionId);
                return adBookingExtension == null
                    ? null
                    : this.Map<AdBookingExtension, AdBookingExtensionModel>(adBookingExtension);
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

        public void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var adBooking = context.AdBookings.First(booking => booking.AdBookingId == adBookingId);

                if (newEndDate.HasValue)
                    adBooking.EndDate = newEndDate.Value;

                if (totalPrice.HasValue)
                    adBooking.TotalPrice = totalPrice.Value;

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
            configuration.CreateMap<LineAd, LineAdModel>();

            // To data
            configuration.CreateMap<AdBookingExtensionModel, AdBookingExtension>()
                .ForMember(member => member.AdBookingExtensionId, options => options.Condition(con => con.AdBookingExtensionId > 0));
            configuration.CreateMap<BookEntryModel, BookEntry>()
                .ForMember(member => member.AdBooking, options => options.Ignore())
                .ForMember(member => member.Publication, options => options.Ignore());
        }
    }
}
