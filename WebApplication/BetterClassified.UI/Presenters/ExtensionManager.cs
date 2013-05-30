using System;
using System.Collections.Generic;
using System.Linq;
using BetterClassified.UI.Models;

namespace BetterClassified.UI.Presenters
{
    public class ExtensionManager
    {
        private readonly Repository.IBookingRepository bookingRepository;
        private readonly Repository.IPublicationRepository publicationRepository;
        private readonly Repository.IConfigSettings configSettings;
        private readonly Repository.IPaymentsRepository payments;

        public ExtensionManager(Repository.IBookingRepository bookingRepository,
            Repository.IPublicationRepository publicationRepository,
            Repository.IConfigSettings configSettings,
            Repository.IPaymentsRepository payments)
        {
            this.bookingRepository = bookingRepository;
            this.publicationRepository = publicationRepository;
            this.configSettings = configSettings;
            this.payments = payments;
        }

        public IEnumerable<PublicationEditionModel> ExtensionDates(int adBookingId, int numberOfInsertions)
        {
            foreach (var publicationEntries in bookingRepository.GetBookEntriesForBooking(adBookingId).GroupBy(be => be.PublicationId))
            {
                if (publicationRepository.IsOnlinePublication(publicationEntries.Key))
                    continue;

                // Fetch the last edition (used continuing the dates and price)
                BookEntryModel bookEntry = publicationEntries.OrderByDescending(be => be.EditionDate).First();

                // Fetch the up-coming editions for the publication
                List<BookEntryModel> upComingEditions = publicationRepository
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
                        PublicationName = publicationRepository.GetPublication(publicationEntries.Key).Title,
                        Editions = upComingEditions
                    };
            }
        }

        public AdBookingExtensionModel CreateExtension(int adBookingId, int numberOfInsertions, string username, decimal price, ExtensionStatus status, bool isOnlineOnly)
        {
            // Create a new extension model
            AdBookingExtensionModel extension = new AdBookingExtensionModel
                {
                    AdBookingId = adBookingId,
                    Insertions = numberOfInsertions,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedUserId = username,
                    ExtensionPrice = price,
                    Status = status,
                    IsOnlineOnly = isOnlineOnly
                };

            bookingRepository.AddBookingExtension(extension);

            return extension;
        }

        public AdBookingExtensionModel GetExtension(int extensionId)
        {
            return bookingRepository.GetBookingExtension(extensionId);
        }

        public void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None)
        {
            AdBookingModel adBooking = bookingRepository.GetBooking(extensionModel.AdBookingId);

            if (extensionModel.IsOnlineOnly)
            {
                // Only required to update the end date on the booking
                // Fetch original booking
                DateTime newEndDate = adBooking.EndDate.AddDays(extensionModel.Insertions);
                bookingRepository.UpdateBooking(extensionModel.AdBookingId, newEndDate);
            }
            else
            {
                // Generate the bookEntries and create them in repository
                BookEntryModel[] models = ExtensionDates(extensionModel.AdBookingId, extensionModel.Insertions)
                    .SelectMany(e => e.Editions)
                    .ToArray();

                bookingRepository.AddBookEntries(models);

                // Update the booking end date
                var lastEditionDate = models.OrderByDescending(d => d.EditionDate).First().EditionDate;

                if (adBooking.BookingType == BookingType.Bundled)
                {
                    lastEditionDate = lastEditionDate.AddDays(configSettings.NumberOfDaysAfterLastEdition);
                }

                var price = adBooking.TotalPrice + extensionModel.ExtensionPrice;
                bookingRepository.UpdateBooking(adBooking.AdBookingId, lastEditionDate, price);
            }

            // Mark the extension as complete
            if (extensionModel.Status == ExtensionStatus.Pending)
            {
                bookingRepository.UpdateExtesion(extensionModel.AdBookingExtensionId, (int)ExtensionStatus.Complete);
            }

            if (extensionModel.IsPaid)
                this.payments.CreateTransaction(userId: extensionModel.LastModifiedUserId,
                    reference: adBooking.ExtensionReference,
                    description: "Booking Extension",
                    amount: extensionModel.ExtensionPrice,
                    paymentType: paymentType
                    );
        }
    }
}