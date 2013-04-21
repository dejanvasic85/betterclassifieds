namespace BetterClassified.UI.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Views;

    public class ExtendBookingPresenter : Controller<IExtendBookingView>
    {
        private readonly Repository.IBookingRepository bookingRepository;
        private readonly Repository.IPublicationRepository publicationRepository;
        private readonly Repository.IConfigSettings configSettings;

        public ExtendBookingPresenter(IExtendBookingView view, Repository.IBookingRepository bookingRepository, Repository.IPublicationRepository publicationRepository, Repository.IConfigSettings configSettings)
            : base(view)
        {
            this.bookingRepository = bookingRepository;
            this.publicationRepository = publicationRepository;
            this.configSettings = configSettings;
        }

        public void Load()
        {
            // Fetch the original booking
            AdBookingModel booking = bookingRepository.GetBooking(View.AdBookingId);

            // Ensure booking exists and belongs to the user
            if (booking == null)
            {
                View.DisplayBookingDoesNotExist();
                return;
            }

            // Ensure that the booking belongs to the logged in user!
            if (!View.LoggedInUserName.Equals(booking.UserId))
            {
                View.DisplayBookingDoesNotExist();
                return;
            }

            // Ensure the booking has not already expired
            if (booking.IsExpired)
            {
                // Get out and display a message that the booking has expired
                View.DisplayExpiredBookingMessage();
                return;
            }

            // Load the screen as per usual (depending on the type of booking)
            View.DataBindOptions(booking.BookingType == BookingType.Bundled
                ? Enumerable.Range(1, configSettings.RestrictedEditionCount)
                : Enumerable.Range(1, configSettings.RestrictedOnlineDaysCount));

            // Load for a single edition first
            LoadForInsertions(1, booking);
        }

        public void LoadForInsertions(int insertions, AdBookingModel booking = null)
        {
            // Fetch the original booking
            if (booking == null)
                booking = bookingRepository.GetBooking(View.AdBookingId);

            // Check whether the booking is online only
            if (booking.BookingType == BookingType.Bundled)
            {
                // Fetch and display the list of editions
                List<PublicationEditionModel> editions = GenerateExtensionDates(View.AdBookingId, insertions).ToList();

                // Fetch the online end date
                DateTime onlineAdEndDate = editions
                    .SelectMany(e => e.Editions)
                    .OrderBy(date => date.EditionDate)
                    .Last()
                    .EditionDate
                    .AddDays(configSettings.NumberOfDaysAfterLastEdition);

                // Fetch price per edition
                decimal pricePerEdition = editions.Sum(s => s.Editions.First().EditionPrice);
                decimal totalPrice = pricePerEdition * insertions;
                View.DataBindEditions(editions, onlineAdEndDate, pricePerEdition, totalPrice);
            }
            else
            {
                View.SetupOnlineOnlyView();
                View.DataBindEditions(null, booking.EndDate.AddDays(insertions), booking.TotalPrice, booking.TotalPrice);
            }
        }

        private IEnumerable<PublicationEditionModel> GenerateExtensionDates(int adBookingId, int numberOfInsertions)
        {
            foreach (var publicationEntries in bookingRepository.GetBookEntriesForBooking(adBookingId).GroupBy(be => be.PublicationId))
            {
                if (publicationRepository.IsOnlinePublication(publicationEntries.Key))
                    continue;

                // Fetch the last edition (used continuing the dates and price)
                BookEntryModel bookEntry = publicationEntries.OrderByDescending(be => be.EditionDate).First();

                // Fetch the up-coming editions for the publication
                List<EditionModel> upComingEditions = publicationRepository
                    .GetEditionsForPublication(bookEntry.PublicationId, bookEntry.EditionDate.AddDays(1), numberOfInsertions)
                    .ToList();

                yield return new PublicationEditionModel
                {
                    PublicationId = publicationEntries.Key,
                    PublicationName = publicationRepository.GetPublication(publicationEntries.Key).Title,
                    Editions = upComingEditions.Select(e => new EditionModel
                        {
                            EditionDate = e.EditionDate,
                            EditionPrice = bookEntry.EditionAdPrice
                        }).ToList()
                };
            }
        }

        public void ProcessExtensions()
        {
            // Create a new extension model
            AdBookingExtensionModel extension = new AdBookingExtensionModel
            {
                AdBookingId = View.AdBookingId,
                Insertions = View.SelectedInsertionCount,
                LastModifiedDate = DateTime.Now,
                LastModifiedUserId = View.LoggedInUserName,
                ExtensionPrice = View.TotalPrice,
                Status = AdBookingExtensionStatus.Pending
            };

            if (View.IsPaymentRequired)
            {
                // Todo - payment processing goes here!
            }
            else
            {
                // Free extension! Create extension record and update the original booking end dates and bookentries
                

                
                extension.Status = AdBookingExtensionStatus.Complete;
            }
        }
    }
}
