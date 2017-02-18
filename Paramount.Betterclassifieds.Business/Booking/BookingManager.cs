
namespace Paramount.Betterclassifieds.Business.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Broadcast;
    using Print;
    using Payment;
    using Events;

    public class BookingManager : IBookingManager
    {

        public AdBookingExtensionModel CreateExtension(int adBookingId, int numberOfInsertions, string username, decimal price, ExtensionStatus status, bool isOnlineOnly)
        {
            // CreateRepository a new extension model
            var extension = new AdBookingExtensionModel
            {
                AdBookingId = adBookingId,
                Insertions = numberOfInsertions,
                LastModifiedDate = DateTime.Now,
                LastModifiedUserId = username,
                ExtensionPrice = price,
                Status = status,
                IsOnlineOnly = isOnlineOnly
            };

            _bookingRepository.AddBookingExtension(extension);

            return extension;
        }

        public AdBookingExtensionModel GetExtension(int extensionId)
        {
            return _bookingRepository.GetBookingExtension(extensionId);
        }

        public AdBookingModel GetBooking(int id)
        {
            return _bookingRepository.GetBooking(id, withOnlineAd: true, withLineAd: true, withPublications: true, withEnquiries: true);
        }

        public IEnumerable<AdBookingModel> GetBookingsForUser(string username, int takeMax)
        {
            var ads = _bookingRepository.GetUserBookings(username, takeMax);

            // Each category ad type (e.g. Events) could have other ways of authorisation to an ad
            // E.g. EventOrganiser
            var onlineAdIds =
                _categoryAdFactory.CreateAuthorisers()
                .SelectMany(a => a.GetAdsForUser(username))
                .Distinct()
                .ToArray();

            if (onlineAdIds.Length > 0)
            {
                var adsAsEventOrganiser = _bookingRepository.GetBookingsForOnlineAds(onlineAdIds);
                return ads.Union(adsAsEventOrganiser, new AdBookingIdComparer());
            }

            return ads;
        }

        public void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None)
        {
            var adBooking = _bookingRepository.GetBooking(extensionModel.AdBookingId);

            if (extensionModel.IsOnlineOnly)
            {
                // Only required to update the end date on the booking
                // Fetch original booking
                DateTime newEndDate = adBooking.EndDate.AddDays(extensionModel.Insertions);
                _bookingRepository.UpdateBooking(extensionModel.AdBookingId, newEndDate: newEndDate);
            }
            else
            {
                // Generate the bookEntries and create them in repository
                BookEntryModel[] models = GenerateExtensionDates(extensionModel.AdBookingId, extensionModel.Insertions)
                    .SelectMany(e => e.Editions)
                    .ToArray();

                _bookingRepository.AddBookEntries(models);

                // Update the booking end date
                var lastEditionDate = models.OrderByDescending(d => d.EditionDate).First().EditionDate;

                if (adBooking.BookingType == BookingType.Bundled)
                {
                    lastEditionDate = lastEditionDate.AddDays(_clientConfigSettings.NumberOfDaysAfterLastEdition);
                }

                var price = adBooking.TotalPrice + extensionModel.ExtensionPrice;
                _bookingRepository.UpdateBooking(adBooking.AdBookingId, newEndDate: lastEditionDate, totalPrice: price);
            }

            // Mark the extension as complete
            if (extensionModel.Status == ExtensionStatus.Pending)
            {
                _bookingRepository.UpdateExtesion(extensionModel.AdBookingExtensionId, (int)ExtensionStatus.Complete);
            }

            if (extensionModel.IsPaid)
                this._payments.CreateTransaction(userId: extensionModel.LastModifiedUserId,
                    reference: adBooking.ExtensionReference,
                    description: "Booking Extension",
                    amount: extensionModel.ExtensionPrice,
                    paymentType: paymentType
                    );
        }

        public void Extend(int adBookingId, int numberOfInsertions, bool? isOnlineOnly = null, ExtensionStatus extensionStatus = ExtensionStatus.Complete, int price = 0, string username = "admin", PaymentType payment = PaymentType.None)
        {
            if (!isOnlineOnly.HasValue)
            {
                isOnlineOnly = _bookingRepository.IsBookingOnlineOnly(adBookingId);
            }

            // CreateRepository the booking extension
            var extension = CreateExtension(adBookingId, numberOfInsertions, username, price, extensionStatus, isOnlineOnly.Value);

            // Then extend it :)
            // Single responsibility = EXTEND
            Extend(extension, payment);
        }

        public void IncrementHits(int adId)
        {
            var onlineAd = _adRepository.GetOnlineAd(adId);
            if (onlineAd == null)
                return;

            onlineAd.IncrementHits();
            _adRepository.UpdateOnlineAd(onlineAd);
        }

        public void SubmitAdEnquiry(AdEnquiry adEnquiry)
        {
            // CreateRepository enquiry in Db
            _adRepository.CreateAdEnquiry(adEnquiry);

            var booking = _bookingRepository.GetBooking(adEnquiry.AdId);
            var bookingUser = _userManager.GetUserByEmailOrUsername(booking.UserId);

            // Send email to the advertiser about the enquiry
            _broadcastManager.SendEmail(new AdEnquiryTemplate
            {
                AdNumber = adEnquiry.AdId.ToString(),
                Name = adEnquiry.FullName,
                Email = adEnquiry.Email,
                Question = adEnquiry.Question
            }, bookingUser.Email);
        }

        public int? CreateBooking(BookingCart bookingCart, BookingOrderResult bookingOrder)
        {
            //using (var scope = new TransactionScope())
            //{
            var adBookingId = _bookingRepository.CreateBooking(bookingCart);

            if (!adBookingId.HasValue)
                throw new Exception("AdBookingId was not returned when trying to create a new booking");

            // CreateRepository the order details in the database 
            // that are used for invoice details 
            _bookingRepository.CreateBookingOrder(bookingOrder, adBookingId.Value);

            if (bookingCart.CategoryAdType.HasValue())
            {
                var categoryAdRepository = _categoryAdFactory.CreateRepository(bookingCart);
                var categoryAd = bookingCart.GetCategoryAd();
                categoryAd.OnlineAdId = bookingCart.OnlineAdModel.OnlineAdId;
                categoryAdRepository.Add(categoryAd);
            }

            // CreateRepository the line ad
            if (!bookingCart.IsLineAdIncluded)
                return adBookingId;

            _bookingRepository.CreateLineAd(adBookingId, bookingCart.LineAdModel);

            // Set the edition dates for each publication
            bookingCart.Publications.ForEach(publicationId =>
            {
                var printRate = bookingOrder.GetPrintRateForPublication(publicationId);
                var publicationPrice = printRate.OrderTotal;
                var editionValue = publicationPrice / bookingCart.PrintInsertions;

                _bookingRepository.CreateLineAdEditions(adBookingId,
                    bookingCart.PrintFirstEditionDate.GetValueOrDefault(),
                    bookingCart.PrintInsertions.GetValueOrDefault(),
                    publicationId,
                    publicationPrice,
                    editionValue,
                    printRate.RateId);
            });

            //  scope.Complete();

            return adBookingId;
        }

        public bool AdBelongsToUser(int adId, string username)
        {
            var booking = _bookingRepository.GetBooking(adId);

            return booking.UserId.Equals(username, StringComparison.OrdinalIgnoreCase);
        }

        public void AddOnlineImage(int adId, Guid documentId, bool removeExisting)
        {
            _bookingRepository.CreateImage(adId, documentId.ToString(), AdTypeCode.OnlineCodeId, removeExisting);
        }

        public void RemoveOnlineImage(int adId, Guid documentId)
        {
            _bookingRepository.DeleteImage(adId, documentId.ToString());
        }

        public void UpdateOnlineAd(int adId, OnlineAdModel onlineAd, bool updateImages = false)
        {
            _bookingRepository.UpdateOnlineAd(adId, onlineAd, updateImages);
        }

        public void UpdateLineAd(int id, LineAdModel lineAd)
        {
            _bookingRepository.UpdateLineAd(id, lineAd);
        }

        public void AssignLineAdImage(int id, Guid documentId)
        {
            _bookingRepository.CreateImage(id, documentId.ToString(), AdTypeCode.LineCodeId);
        }

        public void RemoveLineAdImage(int id, Guid documentId)
        {
            _bookingRepository.DeleteImage(id, documentId.ToString(), AdTypeCode.LineCodeId);
        }

        public void CancelAd(int adId)
        {
            _bookingRepository.CancelAndExpireBooking(adId);
        }

        public void UpdateSchedule(int id, DateTime newStartDate)
        {
            var originalBooking = _bookingRepository.GetBooking(id);

            if (originalBooking.StartDate < _dateService.Today)
            {
                // Booking schedule cannot be updated if ad has already started
                return;
            }

            // Generate the end date
            var endDate = newStartDate.AddDays(_clientConfigSettings.RestrictedOnlineDaysCount);
            _bookingRepository.UpdateBooking(id, newStartDate: newStartDate, newEndDate: endDate);
        }

        public OnlineAdModel GetOnlineAd(int adId)
        {
            return _bookingRepository.GetOnlineAd(adId);
        }

        public IEnumerable<PublicationEditionModel> GenerateExtensionDates(int adBookingId, int numberOfInsertions)
        {
            foreach (var publicationEntries in _bookingRepository.GetBookEntriesForBooking(adBookingId).GroupBy(be => be.PublicationId))
            {
                if (_publicationRepository.IsOnlinePublication(publicationEntries.Key))
                    continue;

                // Fetch the last edition (used continuing the dates and price)
                BookEntryModel bookEntry = publicationEntries.OrderByDescending(be => be.EditionDate).First();

                // Fetch the up-coming editions for the publication
                List<BookEntryModel> upComingEditions = _publicationRepository
                    .GetEditionsForPublication(bookEntry.PublicationId, bookEntry.EditionDate.AddDays(1), numberOfInsertions)
                    .Select(m =>
                    {
                        var pubEntry = publicationEntries.First(p => p.PublicationId == m.PublicationId);

                        return new BookEntryModel
                        {
                            AdBookingId = adBookingId,
                            BaseRateId = pubEntry.BaseRateId,
                            EditionAdPrice = pubEntry.EditionAdPrice,
                            EditionDate = m.EditionDate,
                            PublicationId = m.PublicationId,
                            PublicationPrice = pubEntry.PublicationPrice,
                            RateType = pubEntry.RateType
                        };
                    })
                    .ToList();

                yield return new PublicationEditionModel
                {
                    PublicationId = publicationEntries.Key,
                    PublicationName = _publicationRepository.GetPublication(publicationEntries.Key).Title,
                    Editions = upComingEditions
                };
            }
        }

        private readonly IBookingRepository _bookingRepository;
        private readonly IAdRepository _adRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IClientConfig _clientConfigSettings;
        private readonly IPaymentsRepository _payments;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly ICategoryAdFactory _categoryAdFactory;
        private readonly IDateService _dateService;
        private readonly IEventRepository _eventRepository;

        public BookingManager(IBookingRepository bookingRepository,
            IPublicationRepository publicationRepository,
            IClientConfig clientConfigSettings,
            IPaymentsRepository payments,
            IAdRepository adRepository,
            IUserManager userManager,
            IBroadcastManager broadcastManager,
            ICategoryAdFactory categoryAdFactory,
            IDateService dateService,
            IEventRepository eventRepository)
        {
            _bookingRepository = bookingRepository;
            _publicationRepository = publicationRepository;
            _clientConfigSettings = clientConfigSettings;
            _payments = payments;
            _adRepository = adRepository;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
            _categoryAdFactory = categoryAdFactory;
            _dateService = dateService;
            _eventRepository = eventRepository;
        }

    }
}