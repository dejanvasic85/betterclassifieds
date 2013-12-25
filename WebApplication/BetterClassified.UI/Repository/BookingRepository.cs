using Paramount.Betterclassifieds.Repository;

namespace BetterClassified.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BetterclassifiedsCore.DataModel;
    using Models;
    using System;
    using System.Data.Linq;
    
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

        public List<UserBookingModel> GetBookingsForUser(string username)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                IQueryable<AdBooking> bookings = context.AdBookings.Where(bk =>
                    bk.UserId == username &&
                    (BookingStatusType)bk.BookingStatus == BookingStatusType.Booked);

                return bookings.Select(bk =>
                    new UserBookingModel
                    {
                        AdBookingId = bk.AdBookingId,
                        CategoryName = bk.MainCategory.Title,
                        TotalPrice = bk.TotalPrice,
                        BookingReference = bk.BookReference,
                        StartDate = bk.StartDate.Value,
                        EndDate = bk.EndDate.Value,

                        AdTitle = bk.Ad.AdDesigns.Any(d => d.AdType.Code == AdTypeCode.ONLINE)
                            ? bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.ONLINE).OnlineAds.First().Heading
                            : bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.LINE).LineAds.First().AdHeader,

                        OnlineImageId = bk.Ad.AdDesigns.Any(d=> d.AdType.Code == AdTypeCode.ONLINE && d.AdGraphics.Any()) 
                            ? bk.Ad.AdDesigns.First(d=>d.AdType.Code == AdTypeCode.ONLINE).AdGraphics.First().DocumentID
                            : string.Empty,
                 
                        LineAdImageId = bk.Ad.AdDesigns.Any(d => d.AdType.Code == AdTypeCode.LINE && d.AdGraphics.Any())
                            ? bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.LINE).AdGraphics.First().DocumentID
                            : string.Empty,

                        OnlineAdId = bk.Ad.AdDesigns.Any(d => d.AdType.Code == AdTypeCode.ONLINE)
                            ? bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.ONLINE).OnlineAds.First().OnlineAdId
                            : (int?)null,

                        LineAdId = bk.Ad.AdDesigns.Any(d => d.AdType.Code == AdTypeCode.LINE)
                            ? bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.LINE).LineAds.First().LineAdId
                            : (int?)null,
                    }
                ).ToList();
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

        public void CancelAndExpireBooking(int adBookingId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var booking = context.AdBookings.First(bk => bk.AdBookingId == adBookingId);
                booking.EndDate = DateTime.Today.AddDays(-1);
                booking.BookingStatus = (int)BookingStatusType.Cancelled;
                context.SubmitChanges();
            }
        }

        public List<AdBookingModel> GetBookingsForEdition(DateTime editionDate)
        {
            throw new NotImplementedException();
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
