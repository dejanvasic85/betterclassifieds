using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{       
    public interface IBookingRepository
    {
        AdBookingModel GetBooking(int id, bool withLineAd = false);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
        List<UserBookingModel> GetBookingsForUser(string username);
        List<AdBookingModel> GetBookingsForEdition(DateTime editionDate);
        List<AdBookingModel> GetBookings(int takeAmount = 10);

        int AddBookingExtension(AdBookingExtensionModel extension);
        AdBookingExtensionModel GetBookingExtension(int extensionId);
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void AddBookEntries(BookEntryModel[] bookEntries);
        void CancelAndExpireBooking(int adBookingId);
        void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate);
        bool IsBookingOnline(int adBookingId);
        bool IsBookingInPrint(int adBookingId);
        bool IsBookingOnlineOnly(int adBookingId);
    }
}