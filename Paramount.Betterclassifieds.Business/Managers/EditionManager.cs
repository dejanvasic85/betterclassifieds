namespace Paramount.Betterclassifieds.Business.Managers
{
    using System;
    using Repository;

    public class EditionManager : IEditionManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingManager _bookingManager;
        private readonly IEditionRepository _editionRepository;

        public EditionManager(IBookingRepository bookingRepository, IBookingManager bookingManager, IEditionRepository editionRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingManager = bookingManager;
            _editionRepository = editionRepository;
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
                    _bookingManager.Extend( booking.AdBookingId, numberOfInsertions: 1, isOnlineOnly:null );
                    
                    // Remove the book entries
                    _bookingRepository.DeleteBookEntriesForBooking(booking.AdBookingId, editionDate);
                });

            // Finally remove the edition
            _editionRepository.DeleteEditionByDate(editionDate);
        }
    }
}