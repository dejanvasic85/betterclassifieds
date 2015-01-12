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
        void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None);
        void Extend(int adBookingId, int numberOfInsertions, bool? isOnlineOnly = null, ExtensionStatus extensionStatus = ExtensionStatus.Complete, int price = 0, string username = "admin", PaymentType payment = PaymentType.None);
        void IncrementHits(int id);
        void SubmitAdEnquiry(AdEnquiry enquiry);

        // Booking cart management
        BookingCart GetCart(Func<BookingCart> creator = null);
        void SaveBookingCart(BookingCart bookingCart);
        int? CompleteCurrentBooking(BookingCart bookingCart);
    }
}