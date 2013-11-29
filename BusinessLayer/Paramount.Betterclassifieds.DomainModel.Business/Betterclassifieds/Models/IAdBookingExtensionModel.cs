using System;
using Paramount.DomainModel.Business.Betterclassifieds.Enums;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IAdBookingExtensionModel
    {
        int AdBookingExtensionId { get; set; }
        int AdBookingId { get; set; }
        int Insertions { get; set; }
        decimal ExtensionPrice { get; set; }
        bool IsOnlineOnly { get; set; }
        ExtensionStatus Status { get; set; }
        string LastModifiedUserId { get; set; }
        DateTime? LastModifiedDate { get; set; }
        bool IsFree { get; }
        bool IsPaid { get; }
    }
}