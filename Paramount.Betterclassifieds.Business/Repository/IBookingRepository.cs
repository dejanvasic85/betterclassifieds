﻿using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{       
    public interface IBookingRepository
    {
        // Fetch
        AdBookingModel GetBooking(int id, bool withLineAd = false);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
        List<UserBookingModel> GetBookingsForUser(string username);
        List<AdBookingModel> GetBookingsForEdition(DateTime editionDate);
        List<AdBookingModel> GetBookings(int takeAmount = 10);
        AdBookingExtensionModel GetBookingExtension(int extensionId);

        // Add
        int AddBookingExtension(AdBookingExtensionModel extension);
        void AddBookEntries(BookEntryModel[] bookEntries);

        // Update
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void CancelAndExpireBooking(int adBookingId);
        
        // Delete
        void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate);

        // Other
        bool IsBookingOnline(int adBookingId);
        bool IsBookingInPrint(int adBookingId);
        bool IsBookingOnlineOnly(int adBookingId);
    }
}