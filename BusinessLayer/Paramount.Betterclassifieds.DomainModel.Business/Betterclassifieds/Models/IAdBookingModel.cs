using System;
using Paramount.DomainModel.Business.Betterclassifieds.Enums;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IAdBookingModel
    {
        int AdBookingId { get; set; }
        DateTime EndDate { get; set; }
        BookingType BookingType { get; set; }
        decimal TotalPrice { get; set; }
        string UserId { get; set; }
        bool IsExpired { get; }
        ILineAdModel LineAd { get; set; }
        BookingStatusType BookingStatus { get; set; }
        string BookReference { get; set; }
        string ExtensionReference { get; }
    }
}