using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingManager
    {
        IEnumerable<PublicationEditionModel> GenerateExtensionDates(int adBookingId, int numberOfInsertions);
        AdBookingExtensionModel CreateExtension(int adBookingId, int numberOfInsertions, string username, decimal price, ExtensionStatus status, bool isOnlineOnly);
        AdBookingExtensionModel GetExtension(int extensionId);
        AdBookingModel GetBooking(int id);
        IEnumerable<AdBookingModel> GetBookingsForUser(string username, int takeMax);

        void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None);
        void Extend(int adBookingId, int numberOfInsertions, bool? isOnlineOnly = null, ExtensionStatus extensionStatus = ExtensionStatus.Complete, int price = 0, string username = "admin", PaymentType payment = PaymentType.None);
        void IncrementHits(int id);
        void SubmitAdEnquiry(AdEnquiry enquiry);
        int? CreateBooking(BookingCart bookingCart, BookingOrderResult bookingOrder);
        bool AdBelongsToUser(int adId, string username);
        void AddOnlineImage(int adId, Guid documentId);
        void RemoveOnlineImage(int adId, Guid documentId);
        void UpdateOnlineAd(int adId, OnlineAdModel onlineAd);
        void UpdateLineAd(int id, LineAdModel lineAd);
        void AssignLineAdImage(int id, Guid documentId);
        void RemoveLineAdImage(int id, Guid documentId);

        void CancelAd(int adId);
        void UpdateSchedule(int id, DateTime newStartDate);
        OnlineAdModel GetOnlineAd(int adId);
    }
}