using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Booking
{       
    public interface IBookingRepository
    {
        // Fetch
        AdBookingModel GetBooking(int id, bool withOnlineAd = false,
            bool withLineAd = false,
            bool withPublications = false,
            bool withEnquiries = false);

        List<AdBookingModel> GetUserBookings(string username, int takeMax);
        List<BookEntryModel> GetBookEntriesForBooking(int adBookingId);
        [Obsolete("Please use GetUserBookings instead")]
        List<UserBookingModel> GetBookingsForUser(string username);
        List<AdBookingModel> GetBookingsForEdition(DateTime editionDate);
        AdBookingExtensionModel GetBookingExtension(int extensionId);
        OnlineAdModel GetOnlineAd(int adId);

        // Add
        int AddBookingExtension(AdBookingExtensionModel extension);
        void AddBookEntries(BookEntryModel[] bookEntries);

        // Update
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newStartDate = null, DateTime? newEndDate = null, decimal? totalPrice = null);
        void UpdateOnlineAd(int adBookingId, OnlineAdModel onlineAd, bool updateImages = false);
        void UpdateLineAd(int id, LineAdModel lineAd);
        void CancelAndExpireBooking(int adBookingId);
        
        // Delete
        void DeleteBookEntriesForBooking(int adBookingId, DateTime editionDate);

        // Other
        bool IsBookingOnline(int adBookingId);
        bool IsBookingInPrint(int adBookingId);
        bool IsBookingOnlineOnly(int adBookingId);
        int? CreateBooking(BookingCart getCart);
        void CreateLineAd(int? adBookingId, LineAdModel lineAdModel);
        void CreateLineAdEditions(int? adBookingId, DateTime startDate, int insertions, int publicationId, decimal? publicationPrice = null, decimal? editionPrice = null, int? rateId = null);
        void CreateBookingOrder(BookingOrderResult bookingOrder, int adBookingId);

        // Images
        void CreateImage(int adId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId);
        void DeleteImage(int adId, string documentId, int adTypeId = AdTypeCode.OnlineCodeId);

        
    }
}