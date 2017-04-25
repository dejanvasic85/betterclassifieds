using System.Monads;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    using AutoMapper;
    using Business;
    using Business.Booking;
    using Business.Print;
    using Classifieds;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class BookingRepository : IBookingRepository, IMappingBehaviour
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDateService _dateService;

        public BookingRepository(IDateService dateService, IDbContextFactory dbContextFactory)
        {
            _dateService = dateService;
            _dbContextFactory = dbContextFactory;
        }

        #region Fetch Bookings


        public AdBookingModel GetBooking(int id, bool withOnlineAd = false,
            bool withLineAd = false,
            bool withPublications = false,
            bool withEnquiries = false)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var dataModels = context.AdBookings.Where(bk => bk.AdBookingId == id);
                return MapToModels(dataModels, withOnlineAd, withLineAd, withPublications, withEnquiries).Single();
            }
        }

        public AdBookingModel GetBookingForOnlineAdId(int onlineAdId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var dataModels = from o in context.OnlineAds
                                 where o.OnlineAdId == onlineAdId
                                 join d in context.AdDesigns on o.AdDesignId equals d.AdDesignId
                                 join b in context.AdBookings on d.AdId equals b.AdId
                                 select b;

                return MapToModels(dataModels).SingleOrDefault();
            }
        }


        public List<AdBookingModel> GetUserBookings(string username, int takeMax)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Get the current ads first and if it's less than 
                var datamodels = context.AdBookings
                    .Where(bk => bk.UserId == username)
                    .Where(bk => bk.EndDate > DateTime.Today.AddYears(-1))
                    .OrderByDescending(bk => bk.AdBookingId)
                    .Take(takeMax);

                return MapToModels(datamodels, withOnlineAd: true, withLineAd: true, withPublications: true, withEnquiries: true);
            }
        }

        public List<AdBookingModel> GetBookingsForOnlineAds(int[] onlineAds)
        {
            var results = new List<AdBookingModel>(onlineAds.Length);

            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                foreach (var onlineAdId in onlineAds)
                {
                    var data = context.OnlineAds
                        .Single(o => o.OnlineAdId == onlineAdId)
                        .AdDesign
                        .Ad
                        .AdBookings
                        .ToArray();

                    if (data.Length > 1)
                        throw new ApplicationException($"There are multiple bookings for online ad {onlineAdId}");

                    results.AddRange(MapToModels(data, withOnlineAd: true, withLineAd: true, withPublications: true));
                }
            }

            return results;
        }

        public List<AdBookingModel> GetBookingsForEdition(DateTime editionDate)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
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

        private List<AdBookingModel> MapToModels(
            IEnumerable<AdBooking> dataModels,
            bool withOnlineAd = false,
            bool withLineAd = false,
            bool withPublications = false,
            bool withEnquiries = false)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adBookingModels = new List<AdBookingModel>();

                foreach (var adBookingData in dataModels)
                {
                    var booking = this.Map<AdBooking, AdBookingModel>(adBookingData);

                    var category = adBookingData.MainCategory;
                    var parentCategory = context.MainCategories.Single(m => m.MainCategoryId == category.ParentId);

                    booking.CategoryAdType = category.CategoryAdType;
                    booking.CategoryFontIcon = parentCategory.FontIcon;
                    booking.ParentCategoryName = parentCategory.Title;
                    booking.CategoryName = category.Title;

                    // Line ad
                    if (withLineAd)
                    {
                        var lineAdDesign = adBookingData.Ad.AdDesigns.FirstOrDefault(ds => ds.LineAds.Any());
                        if (lineAdDesign != null)
                        {
                            var lineAd = this.Map<LineAd, LineAdModel>(lineAdDesign.LineAds.Single());
                            lineAd.AdImageId = lineAdDesign.AdGraphics.FirstOrDefault().With(g => g.DocumentID);
                            booking.Ads.Add(lineAd);
                        }
                    }

                    // Online ad
                    if (withOnlineAd)
                    {
                        var onlineAdDataModel = adBookingData.Ad.AdDesigns.First(ds => ds.AdTypeId == AdTypeCode.OnlineCodeId).OnlineAds.Single();
                        var onlineAd = this.Map<OnlineAd, OnlineAdModel>(onlineAdDataModel);

                        onlineAd.Images.AddRange(onlineAdDataModel
                            .With(o => o.AdDesign)
                            .With(d => d.AdGraphics)
                            .With(gr => new AdImage(gr.DocumentID)));

                        booking.Ads.Add(onlineAd);

                        // Ad Enquiry
                        if (withEnquiries)
                        {
                            booking.Enquiries = onlineAdDataModel.OnlineAdEnquiries.Select(this.Map<OnlineAdEnquiry, Enquiry>).ToList();
                        }
                    }

                    if (withPublications)
                    {
                        booking.Publications = context.BookEntries
                            .Where(en => en.AdBookingId == adBookingData.AdBookingId)
                            .Select(en => en.PublicationId.GetValueOrDefault())
                            .Distinct()
                            .ToArray();
                    }

                    adBookingModels.Add(booking);
                }

                return adBookingModels;
            }
        }

        #endregion

        public List<BookEntryModel> GetBookEntriesForBooking(int adBookingId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var bookEntryList = context.BookEntries.Where(entries => entries.AdBookingId == adBookingId).ToList();
                return this.MapList<Classifieds.BookEntry, BookEntryModel>(bookEntryList);
            }
        }

        public OnlineAdModel GetOnlineAd(int adId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Fetch the original online ad
                var ad = context
                    .AdBookings.Single(bk => bk.AdBookingId == adId)
                    .Ad
                    .AdDesigns.SingleOrDefault(ds => ds.AdTypeId == AdTypeCode.OnlineCodeId)
                    .OnlineAds.SingleOrDefault();

                return this.Map<OnlineAd, OnlineAdModel>(ad);
            }
        }

        public int AddBookingExtension(AdBookingExtensionModel extension)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adBookingExtension = this.Map<AdBookingExtensionModel, AdBookingExtension>(extension);
                context.AdBookingExtensions.InsertOnSubmit(adBookingExtension);
                context.SubmitChanges();
                extension.AdBookingExtensionId = adBookingExtension.AdBookingExtensionId;
                extension.AdBookingId = adBookingExtension.AdBookingId;
                return extension.AdBookingExtensionId;
            }
        }

        public AdBookingExtensionModel GetBookingExtension(int extensionId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adBookingExtension = context.AdBookingExtensions.FirstOrDefault(extension => extension.AdBookingExtensionId == extensionId);
                return adBookingExtension == null
                    ? null
                    : this.Map<AdBookingExtension, AdBookingExtensionModel>(adBookingExtension);
            }
        }

        public void UpdateExtesion(int extensionId, int? status)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Fetch and update
                AdBookingExtension extension = context.AdBookingExtensions.First(e => e.AdBookingExtensionId == extensionId);
                if (status.HasValue)
                    extension.Status = status.Value;

                context.SubmitChanges();
            }
        }

        public void UpdateBooking(int adBookingId, DateTime? newStartDate = null, DateTime? newEndDate = null, decimal? totalPrice = null)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adBooking = context.AdBookings.First(booking => booking.AdBookingId == adBookingId);

                if (newStartDate.HasValue)
                    adBooking.StartDate = newStartDate.Value;

                if (newEndDate.HasValue)
                    adBooking.EndDate = newEndDate.Value;

                if (totalPrice.HasValue)
                    adBooking.TotalPrice = totalPrice.Value;

                // Update
                context.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
        }

        public void UpdateOnlineAd(int adBookingId, OnlineAdModel onlineAd, bool updateImages = false)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Fetch the original online ad
                var originalOnlineAd = context
                    .AdBookings.Single(bk => bk.AdBookingId == adBookingId)
                    .Ad
                    .AdDesigns.Single(ds => ds.AdTypeId == AdTypeCode.OnlineCodeId)
                    .OnlineAds.Single();

                // Map the changes
                this.Map(onlineAd, originalOnlineAd);
                context.SubmitChanges();

                if (updateImages && onlineAd.Images.Any())
                {
                    var existingImages = context.AdGraphics.Where(g => g.AdDesignId == originalOnlineAd.AdDesignId);
                    context.AdGraphics.DeleteAllOnSubmit(existingImages);
                    context.SubmitChanges();

                    var newImages = onlineAd.Images.Select(i => new AdGraphic { AdDesignId = originalOnlineAd.AdDesignId, DocumentID = i.DocumentId });
                    context.AdGraphics.InsertAllOnSubmit(newImages);
                    context.SubmitChanges();
                }
            }
        }

        public void UpdateLineAd(int adBookingId, LineAdModel lineAd)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Fetch the original online ad
                var originalLineAd = context
                    .AdBookings.Single(bk => bk.AdBookingId == adBookingId)
                    .Ad
                    .AdDesigns.Single(ds => ds.AdTypeId == AdTypeCode.LineCodeId)
                    .LineAds.Single();

                // Map the changes
                this.Map(lineAd, originalLineAd);

                // And submit
                context.SubmitChanges();
            }
        }

        public void AddBookEntries(BookEntryModel[] bookEntries)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                context.BookEntries.InsertAllOnSubmit(this.MapList<BookEntryModel, Classifieds.BookEntry>(bookEntries.ToList()));
                context.SubmitChanges();
            }
        }

        public void CancelAndExpireBooking(int adBookingId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var booking = context.AdBookings.Single(adBooking => adBooking.AdBookingId == adBookingId);
                booking.EndDate = _dateService.Today.AddDays(-1);
                booking.BookingStatus = (int)BookingStatusType.Cancelled;

                // Delete all book entries after today
                var entriesToDelete = context
                    .BookEntries
                    .Where(bookEntry => bookEntry.EditionDate >= _dateService.Today)
                    .Where(bookEntry => bookEntry.AdBookingId == adBookingId);
                context.BookEntries.DeleteAllOnSubmit(entriesToDelete);

                context.SubmitChanges();
            }
        }

        public void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var bookEntriesToDelete = context.BookEntries.Where(bookEntry => bookEntry.EditionDate == editionDate && bookEntry.AdBookingId == adBookingId);
                context.BookEntries.DeleteAllOnSubmit(bookEntriesToDelete);
                context.SubmitChanges();
            }
        }

        public bool IsBookingOnline(int adBookingId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
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
            using (var context = _dbContextFactory.CreateClassifiedContext())
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

        public int? CreateBooking(BookingCart bookingCart)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                int? adBookingId = null;
                int? onlineDesignId = null;
                int? onlineAdId = null;
                int transactionType = bookingCart.TotalPrice == 0 ? 3 : 2;

                // Save booking to database
                context.Booking_Create(
                    startDate: bookingCart.StartDate,
                    endDate: bookingCart.EndDate,
                    totalPrice: bookingCart.TotalPrice,
                    bookReference: bookingCart.BookingReference,
                    userId: bookingCart.UserId,
                    mainCategoryId: bookingCart.SubCategoryId,
                    insertions: bookingCart.PrintInsertions,
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
                    onlineAdId: ref onlineAdId,
                    onlineDesignId: ref onlineDesignId,
                    transactionType: transactionType
                    );

                // Save the images for online ad
                var graphics = bookingCart.OnlineAdModel.Images.Select(img => new AdGraphic { AdDesignId = onlineDesignId, DocumentID = img.DocumentId });
                context.AdGraphics.InsertAllOnSubmit(graphics);
                context.SubmitChanges();

                // Update the object passed in with any returned ID
                bookingCart.OnlineAdModel.OnlineAdId = onlineAdId.GetValueOrDefault();

                return adBookingId;
            }
        }

        public void CreateLineAd(int? adBookingId, LineAdModel lineAdModel)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
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
                    lineAdModel.IsSuperHeadingPurchased,
                    lineAdModel.WordsPurchased, ref lineAdId);

                if (lineAdModel.AdImageId.HasValue())
                {
                    // Attach the ad graphic to the inserted line ad
                    context
                        .LineAds.Single(l => l.LineAdId == lineAdId)
                        .AdDesign
                        .AdGraphics.Add(new AdGraphic { DocumentID = lineAdModel.AdImageId });

                    context.SubmitChanges();
                }
            }
        }

        public void CreateLineAdEditions(int? adBookingId, DateTime startDate, int insertions, int publicationId, decimal? publicationPrice = null, decimal? editionPrice = null, int? rateId = null)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
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

        public void CreateBookingOrder(BookingOrderResult bookingOrder, int adBookingId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Map the summary 
                var bookingSummaryData = this.Map<BookingOrderResult, AdBookingOrderSummary>(bookingOrder);
                bookingSummaryData.AdBookingId = adBookingId;
                context.AdBookingOrderSummaries.InsertOnSubmit(bookingSummaryData);

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

        public void CreateImage(int adBookingId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId, bool removeExisting = false)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adDesign = context
                    .AdBookings.First(bk => bk.AdBookingId == adBookingId)
                    .Ad
                    .AdDesigns
                    .Single(d => d.AdTypeId == adTypeId);

                if (removeExisting)
                {
                    context.AdGraphics.DeleteAllOnSubmit(adDesign.AdGraphics);
                    context.SubmitChanges();
                }

                adDesign.AdGraphics.Add(new AdGraphic
                {
                    DocumentID = documentId
                });

                if (adTypeId == AdTypeCode.LineCodeId)
                {
                    adDesign
                        .LineAds.Single()
                        .UsePhoto = true;
                }

                context.SubmitChanges();
            }
        }

        public void DeleteImage(int adBookingId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var adDesign = context
                    .AdBookings.First(bk => bk.AdBookingId == adBookingId)
                    .Ad
                    .AdDesigns.SingleOrDefault(d => d.AdTypeId == adTypeId);

                if (adDesign == null)
                    return;

                context.AdGraphics.DeleteAllOnSubmit(adDesign.AdGraphics.Where(g => g.DocumentID == documentId));

                if (adTypeId == AdTypeCode.LineCodeId)
                {
                    adDesign.LineAds.Single().UsePhoto = false;
                }

                context.SubmitChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From data
            configuration.CreateProfile("BookingMapProfile");
            configuration.CreateMap<AdBooking, AdBookingModel>()
                .ForMember(member => member.BookingType, options => options.ResolveUsing<BookingTypeResolver>())
                .ForMember(m => m.SubCategoryId, options => options.MapFrom(src => src.MainCategoryId))
                .ForMember(m => m.CategoryId, options => options.MapFrom(src => src.MainCategory.ParentId))
                ;

            configuration.CreateMap<Classifieds.BookEntry, BookEntryModel>();
            configuration.CreateMap<AdBookingExtension, AdBookingExtensionModel>();
            configuration.CreateMap<LineAd, LineAdModel>();
            configuration.CreateMap<MainCategory, Category>();
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<AdGraphic, AdImage>().ForMember(member => member.DocumentId, options => options.MapFrom(source => source.DocumentID));
            configuration.CreateMap<Publication, PublicationModel>();
            configuration.CreateMap<OnlineAdEnquiry, Enquiry>().ForMember(m => m.EnquiryId, options => options.MapFrom(src => src.OnlineAdEnquiryId));



            // To data
            configuration.CreateMap<AdBookingExtensionModel, AdBookingExtension>().ForMember(member => member.AdBookingExtensionId, options => options.Condition(con => con.AdBookingExtensionId > 0));
            configuration.CreateMap<BookEntryModel, Classifieds.BookEntry>()
                .ForMember(member => member.AdBooking, options => options.Ignore())
                .ForMember(member => member.Publication, options => options.Ignore());
            configuration.CreateMap<BookingAdRateResult, AdBookingOrder>();
            configuration.CreateMap<PrintAdChargeItem, AdBookingOrderItem>();
            configuration.CreateMap<OnlineChargeItem, AdBookingOrderItem>();
            configuration.CreateMap<OnlineAdModel, OnlineAd>().ForMember(m => m.OnlineAdId, options => options.Ignore());
            configuration.CreateMap<LineAdModel, LineAd>().ForMember(m => m.LineAdId, options => options.Ignore());
            configuration.CreateMap<BookingOrderResult, AdBookingOrderSummary>().ForMember(member => member.AdBooking, options => options.Ignore());
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
