﻿using System;
using Paramount.Betterclassifieds.Repository;

namespace Paramount.Betterclassifieds.Business
{
    public class EditionManager : IEditionManager
    {
        private readonly IBookingRepository _bookingRepository;

        public EditionManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void RemoveEdition(DateTime editionDate)
        {
            // Fetch bookings that belong to this edition
            var bookingsToExtend = _bookingRepository.GetBookingsForEdition(editionDate);


        }
    }
}