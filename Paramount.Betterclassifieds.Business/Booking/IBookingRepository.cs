using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Booking
{       
    public interface IBookingRepository
    {
        // Fetch
        AdBookingModel GetBooking(int id, bool withLineAd = false);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
        List<UserBookingModel> GetBookingsForUser(string username);
        List<AdBookingModel> GetBookingsForEdition(DateTime editionDate);
        AdBookingExtensionModel GetBookingExtension(int extensionId);

        // Add
        int AddBookingExtension(AdBookingExtensionModel extension);
        void AddBookEntries(BookEntryModel[] bookEntries);

        // Update
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void UpdateOnlineAd(int adBookingId, OnlineAdModel onlineAd);
        void CancelAndExpireBooking(int adBookingId);
        
        // Delete
        void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate);

        // Other
        bool IsBookingOnline(int adBookingId);
        bool IsBookingInPrint(int adBookingId);
        bool IsBookingOnlineOnly(int adBookingId);
        int? SubmitBooking(BookingCart getCart);
        void SubmitLineAd(int? adBookingId, LineAdModel lineAdModel);
        void SubmitLineAdEditions(int? adBookingId, DateTime startDate, int insertions, int publicationId, decimal? publicationPrice = null, decimal? editionPrice = null, int? rateId = null);
        void SubmitBookingOrder(BookingOrderResult bookingOrder, int adBookingId);

        // Images
        void AddImage(int adId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId);
        void RemoveImage(int adId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId);
    }
}