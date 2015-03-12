namespace Paramount.Betterclassifieds.Business.Print
{
    using System;
    using System.Collections.Generic;
    using Booking;

    public interface IEditionManager
    {
        void RemoveEditionAndExtendBookings(DateTime edition);
        List<DateTime> GetUpcomingEditions(params int[] publications);
        List<DateTime> GetUpcomingEditionsForPublication(int publication, DateTime fromDate, out string publicationName);
        IEnumerable<int> GetAvailableInsertions();
    }

    public class EditionManager : IEditionManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingManager _bookingManager;
        private readonly IEditionRepository _editionRepository;
        private readonly IPublicationRepository _publicationRepository;

        public EditionManager(IBookingRepository bookingRepository, IBookingManager bookingManager, IEditionRepository editionRepository, IPublicationRepository publicationRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingManager = bookingManager;
            _editionRepository = editionRepository;
            _publicationRepository = publicationRepository;
        }

        /// <summary>
        /// Removes and edition from the system for all publications and extends any bookings if already booked
        /// </summary>
        /// <param name="editionDate"></param>
        public void RemoveEditionAndExtendBookings(DateTime editionDate)
        {
            // Fetch bookings that belong to this edition
            _bookingRepository.GetBookingsForEdition(editionDate).ForEach(booking =>
                {
                    // Extend the booking
                    _bookingManager.Extend(booking.AdBookingId, numberOfInsertions: 1, isOnlineOnly: null);

                    // Remove the book entries
                    _bookingRepository.DeleteBookEntriesForBooking(booking.AdBookingId, editionDate);
                });

            // Finally remove the edition
            _editionRepository.DeleteEditionByDate(editionDate);
        }

        private List<DateTime> GetUpcomingEditions(DateTime fromDate, DateTime fromDeadline, params int[] publications)
        {
            return _editionRepository.GetUpcomingEditions(fromDate, fromDeadline, 50, publications);
        }

        public List<DateTime> GetUpcomingEditions(params int[] publications)
        {
            return _editionRepository.GetUpcomingEditions(minEditionDate: DateTime.Today, minDeadlineDate: DateTime.Now, publicationIds: publications);
        }

        public List<DateTime> GetUpcomingEditionsForPublication(int publication, DateTime fromDate, out string publicationName)
        {
            // Fetch the publication name first
            publicationName = _publicationRepository.GetPublication(publication).Title;

            return GetUpcomingEditions(fromDate, fromDeadline: DateTime.Now, publications: publication);
        }

        public IEnumerable<int> GetAvailableInsertions()
        {
            const int maxInsertions = 20;
            for (int i = 1; i <= maxInsertions; i++)
            {
                yield return i;
            }
        }
    }
}