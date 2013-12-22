namespace Paramount.Betterclassifieds.Business
{
    using System;

    public interface IEditionManager
    {
        void RemoveEdition(DateTime edition);
    }

    public class EditionManager : IEditionManager
    {
        private readonly Repository.IBookingRepository _bookingRepository;

        public EditionManager(Repository.IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void RemoveEdition(DateTime editionDate)
        {
            // Fetch bookings that belong to this edition
            var bookingsToProcess = _bookingRepository.GetBookingsForEdition(editionDate);


        }
    }

}
