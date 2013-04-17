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

        public ExtendBookingPresenter(IExtendBookingView view, Repository.IBookingRepository bookingRepository, Repository.IPublicationRepository publicationRepository)
            : base(view)
        {
            this.bookingRepository = bookingRepository;
            this.publicationRepository = publicationRepository;
        }

        public void Load()
        {
            // Hard code ten weeks to allow the extensions
            View.DataBindInsertionList(Enumerable.Range(1, 10));

            // Load for a single edition first
            LoadForInsertions(1);
        }

        public void LoadForInsertions(int value)
        {
            // Fetch and display the list of editions
            var editions = GenerateExtensionDates(View.AdBookingId, value);
            View.DataBindEditions(editions);
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
                    Editions = upComingEditions.Select(e => new EditionModel { EditionDate = e.EditionDate.GetValueOrDefault() }).ToList()
                };
            }
        }

        public void ProcessExtensions()
        {

        }
    }

}