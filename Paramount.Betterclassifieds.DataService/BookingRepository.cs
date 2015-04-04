namespace Paramount.Betterclassifieds.DataService.Repository
{
    using AutoMapper;
    using Business;
    using Business.Booking;
    using Classifieds;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using Business.Print;

    public class BookingRepository : IBookingRepository, IMappingBehaviour
    {
        public AdBookingModel GetBooking(int id, bool withLineAd = false)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var booking = context.AdBookings.FirstOrDefault(b => b.AdBookingId == id);
                if (booking == null)
                    return null;

                var adBookingModel = this.Map<AdBooking, AdBookingModel>(booking);

                // Fetch line ad if required
                if (withLineAd)
                {
                    var design = booking.Ad.AdDesigns.FirstOrDefault(d => d.LineAds.Any());
                    if (design != null)
                    {
                        var lineAdModel = this.Map<LineAd, LineAdModel>(design.LineAds.Single());
                        adBookingModel.Ads.Add(lineAdModel);

                        // Fetch the images
                        var lineAdImg = context.AdGraphics.FirstOrDefault(gr => gr.AdDesignId == design.AdDesignId);
                        if (lineAdImg != null)
                        {
                            lineAdModel.AdImageId = lineAdImg.DocumentID;
                        }
                    }
                }

                // Always fetch the online ad
                var onlineDesign = booking.Ad.AdDesigns.FirstOrDefault(d => d.OnlineAds.Any());
                if (onlineDesign != null)
                {
                    var onlineAdModel = this.Map<OnlineAd, OnlineAdModel>(onlineDesign.OnlineAds.Single());
                    adBookingModel.Ads.Add(onlineAdModel);

                    // Fetch the images
                    onlineAdModel.Images = context.AdGraphics.Where(g => g.AdDesignId == onlineDesign.AdDesignId).Select(g => new AdImage(g.DocumentID)).ToList();
                }

                return adBookingModel;
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

        public void UpdateOnlineAd(int adBookingId, OnlineAdModel onlineAd)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Fetch the original online ad
                var originalOnlineAd = context
                    .AdBookings.Single(bk => bk.AdBookingId == adBookingId)
                    .Ad
                    .AdDesigns.Single(ds => ds.AdTypeId == AdTypeCode.OnlineCodeId)
                    .OnlineAds.Single();

                // Map the changes
                this.Map(onlineAd, originalOnlineAd);

                // And submit
                context.SubmitChanges();
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

        public int? SubmitBooking(BookingCart bookingCart)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                int? adBookingId = null;
                int? onlineDesignId = null;
                int transactionType = bookingCart.TotalPrice == 0 ? 3 : 2;

                // Save booking to database
                context.Booking_Create(
                    startDate: bookingCart.StartDate,
                    endDate: bookingCart.EndDate,
                    totalPrice: bookingCart.TotalPrice,
                    bookReference: bookingCart.Reference,
                    userId: bookingCart.UserId,
                    mainCategoryId: bookingCart.SubCategoryId,
                    insertions: 1,
                    adBookingId: ref adBookingId,
                    onlineAdHeading: bookingCart.OnlineAdModel.Heading,
                    onlineAdDescription: bookingCart.OnlineAdModel.Description,
                    onlineAdHtml: bookingCart.OnlineAdModel.HtmlText,
                    onlineAdPrice: null,
                    locationId: bookingCart.OnlineAdModel.LocationId,
                    locationAreaId: bookingCart.OnlineAdModel.LocationAreaId,
                    contactName: bookingCart.OnlineAdModel.ContactName,
                    contactEmail: bookingCart.OnlineAdModel.ContactEmail,
                    contactPhone: bookingCart.OnlineAdModel.ContactPhone,
                    onlineDesignId: ref onlineDesignId,
                    transactionType: transactionType
                    );

                // Save the images for online ad
                var graphics = bookingCart.OnlineAdModel.Images.Select(img => new AdGraphic { AdDesignId = onlineDesignId, DocumentID = img.DocumentId });
                context.AdGraphics.InsertAllOnSubmit(graphics);
                context.SubmitChanges();

                return adBookingId;
            }
        }

        public void SubmitLineAd(int? adBookingId, LineAdModel lineAdModel)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                int? lineAdId = null;

                context.LineAd_Create(adBookingId,
                    lineAdModel.AdHeader,
                    lineAdModel.AdText,
                    lineAdModel.NumOfWords,
                    lineAdModel.UsePhoto,
                    lineAdModel.UseBoldHeader,
                    lineAdModel.IsColourBoldHeading,
                    lineAdModel.IsColourBorder,
                    lineAdModel.IsColourBackground,
                    lineAdModel.IsSuperBoldHeading,
                    lineAdModel.BoldHeadingColourCode,
                    lineAdModel.BorderColourCode,
                    lineAdModel.BackgroundColourCode,
                    lineAdModel.IsSuperHeadingPurchased, ref lineAdId);
            }
        }

        public void SubmitLineAdEditions(int? adBookingId, DateTime startDate, int insertions, int publicationId, decimal? publicationPrice = null, decimal? editionPrice = null, int? rateId = null)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                context.BookEntry_Create(adBookingId,
                    startDate,
                    insertions,
                    publicationId,
                    editionPrice: editionPrice,
                    publicationPrice: publicationPrice,
                    ratecardId: rateId,
                    ratecardType: "Ratecard");
            }
        }

        public void SubmitBookingOrder(BookingOrderResult bookingOrder, int adBookingId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Map online
                var onlineOrder = this.Map<BookingAdRateResult, AdBookingOrder>(bookingOrder.OnlineBookingAdRate);
                onlineOrder.AdBookingOrderItems.AddRange(this.MapList<ILineItem, AdBookingOrderItem>(bookingOrder.OnlineBookingAdRate.GetItems().ToList()));
                onlineOrder.AdBookingId = adBookingId;
                onlineOrder.CreatedDate = DateTime.Now;
                onlineOrder.CreateDateUtc = DateTime.UtcNow;
                context.AdBookingOrders.InsertOnSubmit(onlineOrder);

                // Map Print
                if (bookingOrder.PrintRates != null && bookingOrder.PrintRates.Count > 0)
                {
                    bookingOrder.PrintRates.ForEach(pr =>
                    {
                        var printDataModel = this.Map<BookingAdRateResult, AdBookingOrder>(pr);
                        printDataModel.AdBookingId = adBookingId;
                        printDataModel.CreatedDate = DateTime.Now;
                        printDataModel.CreateDateUtc = DateTime.UtcNow;

                        var items = this.MapList<ILineItem, AdBookingOrderItem>(pr.GetItems().ToList());
                        printDataModel.AdBookingOrderItems.AddRange(items);

                        context.AdBookingOrders.InsertOnSubmit(printDataModel);
                    });
                }

                context.SubmitChanges();
            }
        }

        public void AddImage(int adBookingId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var adDesign = context.AdBookings.First(bk => bk.AdBookingId == adBookingId).Ad.AdDesigns.Single(d => d.AdTypeId == adTypeId);
                adDesign.AdGraphics.Add(new AdGraphic
                {
                    DocumentID = documentId
                });
                context.SubmitChanges();
            }
        }

        public void RemoveImage(int adBookingId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var adDesign = context.AdBookings.First(bk => bk.AdBookingId == adBookingId).Ad.AdDesigns.Single(d => d.AdTypeId == adTypeId);
                context.AdGraphics.DeleteOnSubmit(adDesign.AdGraphics.Single(g => g.DocumentID == documentId));
                context.SubmitChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From data
            configuration.CreateProfile("BookingMapProfile");
            configuration.CreateMap<AdBooking, AdBookingModel>()
                .ForMember(member => member.BookingType, options => options.ResolveUsing<BookingTypeResolver>());
            configuration.CreateMap<Classifieds.BookEntry, BookEntryModel>();
            configuration.CreateMap<AdBookingExtension, AdBookingExtensionModel>();
            configuration.CreateMap<LineAd, LineAdModel>();
            configuration.CreateMap<MainCategory, Category>();
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<AdGraphic, AdImage>()
                .ForMember(member => member.DocumentId, options => options.MapFrom(source => source.DocumentID));
            configuration.CreateMap<Publication, PublicationModel>();

            // To data
            configuration.CreateMap<AdBookingExtensionModel, AdBookingExtension>()
                .ForMember(member => member.AdBookingExtensionId, options => options.Condition(con => con.AdBookingExtensionId > 0));
            configuration.CreateMap<BookEntryModel, Classifieds.BookEntry>()
                .ForMember(member => member.AdBooking, options => options.Ignore())
                .ForMember(member => member.Publication, options => options.Ignore());
            configuration.CreateMap<BookingAdRateResult, AdBookingOrder>();
            configuration.CreateMap<PrintAdChargeItem, AdBookingOrderItem>();
            configuration.CreateMap<OnlineChargeItem, AdBookingOrderItem>();
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(m => m.OnlineAdId, options => options.Ignore());

        }
    }

    public class BookingTypeResolver : ValueResolver<AdBooking, BookingType>
    {
        protected override BookingType ResolveCore(AdBooking source)
        {
            switch (source.BookingType.ToLower())
            {
                case "regular": return BookingType.Regular;
                case "bundled": return BookingType.Bundled;
                default: return BookingType.Special;
            }
        }
    }
}
