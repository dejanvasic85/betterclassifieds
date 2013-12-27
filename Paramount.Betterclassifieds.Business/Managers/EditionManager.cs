namespace Paramount.Betterclassifieds.Business.Managers
{
    using System;
    using Repository;

    public class EditionManager : IEditionManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingManager _bookingManager;

        public EditionManager(IBookingRepository bookingRepository, IBookingManager bookingManager)
        {
            _bookingRepository = bookingRepository;
            _bookingManager = bookingManager;
        }

        public void RemoveEditionAndExtendBookings(DateTime editionDate)
        {
            // Fetch bookings that belong to this edition
            _bookingRepository.GetBookingsForEdition(editionDate)
                .ForEach(booking => _bookingManager.Extend(booking.AdBookingId, 1));
        }
    }
}