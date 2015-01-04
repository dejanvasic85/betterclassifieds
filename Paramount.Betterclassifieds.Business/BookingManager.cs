namespace Paramount.Betterclassifieds.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Broadcast;
    using Managers;
    using Models;
    using Repository;
    using Booking;

    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingCartRepository _cartRepository;
        private readonly IAdRepository _adRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IClientConfig _clientConfigSettings;
        private readonly IPaymentsRepository _payments;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IBookingSessionIdentifier _bookingSessionIdentifier;

        public BookingManager(IBookingRepository bookingRepository,
            IPublicationRepository publicationRepository,
            IClientConfig clientConfigSettings,
            IPaymentsRepository payments,
            IAdRepository adRepository,
            IUserManager userManager,
            IBroadcastManager broadcastManager,
            IBookingSessionIdentifier bookingSessionIdentifier,
            IBookingCartRepository cartRepository)
        {
            _bookingRepository = bookingRepository;
            _publicationRepository = publicationRepository;
            _clientConfigSettings = clientConfigSettings;
            _payments = payments;
            _adRepository = adRepository;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
            _bookingSessionIdentifier = bookingSessionIdentifier;
            _cartRepository = cartRepository;
        }

        public AdBookingExtensionModel CreateExtension(int adBookingId, int numberOfInsertions, string username, decimal price, ExtensionStatus status, bool isOnlineOnly)
        {
            // Create a new extension model
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

        public void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None)
        {
            var adBooking = _bookingRepository.GetBooking(extensionModel.AdBookingId);

            if (extensionModel.IsOnlineOnly)
            {
                // Only required to update the end date on the booking
                // Fetch original booking
                DateTime newEndDate = adBooking.EndDate.AddDays(extensionModel.Insertions);
                _bookingRepository.UpdateBooking(extensionModel.AdBookingId, newEndDate);
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
                _bookingRepository.UpdateBooking(adBooking.AdBookingId, lastEditionDate, price);
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

            // Create the booking extension
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
            // Create enquiry in Db
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

        public BookingCart GetCart(Func<BookingCart> creator = null)
        {
            // Fetch the booking based on the current session id
            var bookingCart = _cartRepository.GetBookingCart(_bookingSessionIdentifier.Id);

            if (bookingCart == null && creator != null)
            {
                // The booking cart is not available so use the creation function instead
                var newCart = creator();
                SaveBookingCart(newCart);
                return newCart;
            }

            if (bookingCart == null)
            {
                throw new NullReferenceException("Booking cart is null and creator function was not supplied.");
            }

            return bookingCart;
        }

        public void SaveBookingCart(BookingCart bookingCart)
        {
            // Save it to the mongo db store ( until the booking is complete )
            _cartRepository.SaveBookingCart(bookingCart);
        }

        /// <summary>
        /// Gets the current booking and saves it to the database
        /// </summary>
        public int? CompleteCurrentBooking(BookingCart bookingCart)
        {
            bookingCart.Completed = true;
            SaveBookingCart(bookingCart);
            return _bookingRepository.SubmitBooking(bookingCart);
        }
    }
}