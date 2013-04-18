using System;

namespace BetterClassified.UI.Presenters
{
    using System.Collections.Generic;
    using System.Linq;
    using BetterclassifiedsCore.DataModel;
    using Models;
    using Views;

    public class ExtendBookingPresenter : Controller<IExtendBookingView>
    {
        private readonly Repository.IBookingRepository bookingRepository;
        private readonly Repository.IPublicationRepository publicationRepository;

        // The following are hard coded (should be configurable per client)
        private const int RestrictedEditionCount = 10;
        private const int NumberOfDaysAfterLastEdition = 6;

        public ExtendBookingPresenter(IExtendBookingView view, Repository.IBookingRepository bookingRepository, Repository.IPublicationRepository publicationRepository)
            : base(view)
        {
            this.bookingRepository = bookingRepository;
            this.publicationRepository = publicationRepository;
        }

        public void Load()
        {
            // Hard code ten weeks to allow the extensions
            View.DataBindOptions(Enumerable.Range(1, RestrictedEditionCount));

            // Load for a single edition first
            LoadForInsertions(1);
        }

        public void LoadForInsertions(int insertions)
        {
            // Fetch and display the list of editions
            var editions = GenerateExtensionDates(View.AdBookingId, insertions).ToList();

            // Fetch the online end date
            DateTime onlineAdEndDate = editions
                .SelectMany(e => e.Editions)
                .OrderBy(date => date.EditionDate)
                .Last()
                .EditionDate
                .AddDays(NumberOfDaysAfterLastEdition);

            // Fetch price per edition
            decimal pricePerEdition = editions.Sum(s => s.Editions.First().EditionPrice);
            decimal totalPrice = pricePerEdition * insertions;
            View.DataBindEditions(editions, onlineAdEndDate, pricePerEdition, totalPrice);
        }

        private IEnumerable<PublicationEditionModel> GenerateExtensionDates(int adBookingId, int numberOfInsertions)
        {
            foreach (var publicationEntries in bookingRepository.GetBookEntriesForBooking(adBookingId).GroupBy(be => be.PublicationId))
            {
                if (publicationRepository.IsOnlinePublication(publicationEntries.Key.GetValueOrDefault()))
                    continue;

                // Fetch the last edition
                BookEntry bookEntry = publicationEntries.OrderByDescending(be => be.EditionDate).First();

                // Fetch the up-coming editions for the publication
                List<Edition> upComingEditions = publicationRepository
                    .GetEditionsForPublication(bookEntry.PublicationId.GetValueOrDefault(), bookEntry.EditionDate.GetValueOrDefault().AddDays(1), numberOfInsertions)
                    .ToList();

                yield return new PublicationEditionModel
                {
                    PublicationId = publicationEntries.Key.GetValueOrDefault(),
                    PublicationName = publicationRepository.GetPublication(publicationEntries.Key.GetValueOrDefault()).Title,
                    Editions = upComingEditions.Select(e => new EditionModel
                        {
                            EditionDate = e.EditionDate.GetValueOrDefault(),
                            EditionPrice = bookEntry.EditionAdPrice.GetValueOrDefault()
                        }).ToList()
                };
            }
        }

        public void ProcessExtensions()
        {
            
        }
    }

}