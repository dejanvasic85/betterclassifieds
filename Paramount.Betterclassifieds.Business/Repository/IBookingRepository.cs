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
        int AddBookingExtension(AdBookingExtensionModel extension);
        AdBookingExtensionModel GetBookingExtension(int extensionId);
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void AddBookEntries(BookEntryModel[] bookEntries);
        void CancelAndExpireBooking(int adBookingId);
        List<AdBookingModel> GetBookingsForEdition(DateTime editionDate);
        void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate);
    }
}