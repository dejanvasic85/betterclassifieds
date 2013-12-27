using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IBookingManager
    {
        IEnumerable<PublicationEditionModel> GenerateExtensionDates(int adBookingId, int numberOfInsertions);
        AdBookingExtensionModel CreateExtension(int adBookingId, int numberOfInsertions, string username, decimal price, ExtensionStatus status, bool isOnlineOnly);
        AdBookingExtensionModel GetExtension(int extensionId);
        void Extend(AdBookingExtensionModel extensionModel, PaymentType paymentType = PaymentType.None);
        void Extend(int adBookingId, int numberOfInsertions);
    }
}