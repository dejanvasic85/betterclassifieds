﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Models.Comparers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.Comparers;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class BookingRepository : IBookingRepository, IMappingBehaviour
    {
        public AdBookingModel GetBooking(int id, bool withLineAd = false)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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
                        model.Ads.Add(this.Map<LineAd, LineAdModel>(design.LineAds.First()));
                }

                return model;
            }
        }

        public List<BookEntryModel> GetBookEntriesForBooking(int adBookingId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var bookEntryList = context.BookEntries.Where(entries => entries.AdBookingId == adBookingId).ToList();
                return this.MapList<Classifieds.BookEntry, BookEntryModel>(bookEntryList);
            }
        }

        public List<UserBookingModel> GetBookingsForUser(string username)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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

                        OnlineImageId = bk.Ad.AdDesigns.Any(d => d.AdType.Code == AdTypeCode.ONLINE && d.AdGraphics.Any())
                            ? bk.Ad.AdDesigns.First(d => d.AdType.Code == AdTypeCode.ONLINE).AdGraphics.First().DocumentID
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

        public List<AdBookingModel> GetBookings(int takeAmount = 10)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var data = context.AdBookings
                    .Where(b => b.BookingStatus == (int)BookingStatusType.Booked && b.EndDate > DateTime.Today)
                    .OrderByDescending(b => b.BookingDate) // Latest first
                    .Take(takeAmount)
                    .AsEnumerable();

                IEnumerable<AdBookingModel> bookings = data
                    .Select(b =>
                    {
                        var booking = this.Map<AdBooking, AdBookingModel>(b);

                        // Fetch category
                        booking.Category = this.Map<MainCategory, Category>(b.MainCategory);

                        // Fetch online ad
                        var onlineAdData = context.OnlineAds.FirstOrDefault(o => o.AdDesign.AdId == b.AdId);
                        var onlineAdModel = this.Map<OnlineAd, OnlineAdModel>(onlineAdData);

                        // Fetch the images
                        if (onlineAdModel != null)
                        {
                            var graphics = onlineAdData.AdDesign.AdGraphics.ToList();
                            onlineAdModel.Images = this.MapList<AdGraphic, AdImage>(graphics);
                        }

                        booking.Ads.Add(onlineAdModel);

                        
                        // Fetch publications
                        var publications = b.BookEntries.Select(entry => entry.Publication)
                            .Distinct(new PublicationDataIdComparer())
                            .Where(p => p.PublicationTypeId != (int)PublicationTypeModel.Online)// Do not get online publications
                            .ToList();

                        booking.Publications = this.MapList<Publication, PublicationModel>(publications);

                        return booking;
                    });

                return bookings.ToList();
            }
        }

        public int AddBookingExtension(AdBookingExtensionModel extension)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var adBookingExtension = context.AdBookingExtensions.FirstOrDefault(extension => extension.AdBookingExtensionId == extensionId);
                return adBookingExtension == null
                    ? null
                    : this.Map<AdBookingExtension, AdBookingExtensionModel>(adBookingExtension);
            }
        }

        public void UpdateExtesion(int extensionId, int? status)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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
            using (var context = DataContextFactory.CreateClassifiedContext())
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
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                context.BookEntries.InsertAllOnSubmit(this.MapList<BookEntryModel, Classifieds.BookEntry>(bookEntries.ToList()));
                context.SubmitChanges();
            }
        }

        public void CancelAndExpireBooking(int adBookingId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var booking = context.AdBookings.First(bk => bk.AdBookingId == adBookingId);
                booking.EndDate = DateTime.Today.AddDays(-1);
                booking.BookingStatus = (int)BookingStatusType.Cancelled;
                context.SubmitChanges();
            }
        }

        public List<AdBookingModel> GetBookingsForEdition(DateTime editionDate)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var bookings = context.AdBookings.Join(
                    context.BookEntries.Where(b => b.EditionDate == editionDate),
                        booking => booking.AdBookingId,
                        entry => entry.AdBookingId,
                        (booking, entry) => booking).ToList();

                var models = this.MapList<AdBooking, AdBookingModel>(bookings.ToList());

                return models.Distinct(new AdBookingIdComparer()).ToList();
            }
        }

        public void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var bookEntriesToDelete = context.BookEntries.Where(bookEntry => bookEntry.EditionDate == editionDate && bookEntry.AdBookingId == adBookingId);
                context.BookEntries.DeleteAllOnSubmit(bookEntriesToDelete);
                context.SubmitChanges();
            }
        }

        public bool IsBookingOnline(int adBookingId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Make sure there's just one ad design (online)
                var onlineAd = from o in context.OnlineAds
                               join d in context.AdDesigns on o.AdDesignId equals d.AdDesignId
                               join b in context.AdBookings on d.Ad.AdId equals b.AdId
                               where b.AdBookingId == adBookingId
                               select o;

                return onlineAd.FirstOrDefault() != null;
            }
        }

        public bool IsBookingInPrint(int adBookingId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Make sure there's just one ad design (online)
                var printad = from o in context.LineAds
                              join d in context.AdDesigns on o.AdDesignId equals d.AdDesignId
                              join b in context.AdBookings on d.Ad.AdId equals b.AdId
                              where b.AdBookingId == adBookingId
                              select o;

                return printad.FirstOrDefault() != null;
            }
        }

        public bool IsBookingOnlineOnly(int adBookingId)
        {
            return this.IsBookingOnline(adBookingId) && !this.IsBookingInPrint(adBookingId);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From data
            configuration.CreateProfile("BookingMapProfile");
            configuration.CreateMap<AdBooking, AdBookingModel>()
                .ForMember(member => member.BookingType, options => options.Ignore());
            configuration.CreateMap<Classifieds.BookEntry, BookEntryModel>();
            configuration.CreateMap<AdBookingExtension, AdBookingExtensionModel>();
            configuration.CreateMap<LineAd, LineAdModel>();
            configuration.CreateMap<MainCategory, Category>();
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<AdGraphic, AdImage>()
                .ForMember(member => member.ImageUrl, options => options.MapFrom(source => source.DocumentID));
            configuration.CreateMap<Publication, PublicationModel>();

            // To data
            configuration.CreateMap<AdBookingExtensionModel, AdBookingExtension>()
                .ForMember(member => member.AdBookingExtensionId, options => options.Condition(con => con.AdBookingExtensionId > 0));
            configuration.CreateMap<BookEntryModel, Classifieds.BookEntry>()
                .ForMember(member => member.AdBooking, options => options.Ignore())
                .ForMember(member => member.Publication, options => options.Ignore());
        }
    }
}
