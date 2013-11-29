using System;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IUserBookingModel
    {
        int AdBookingId { get; set; }
        string BookingReference { get; set; }
        string CategoryName { get; set; }
        string AdTitle { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string OnlineImageId { get; set; }
        string LineAdImageId { get; set; }
        decimal? TotalPrice { get; set; }
        int? OnlineAdId { get; set; }
        int? LineAdId { get; set; }
        bool AboutToExpire { get; }
        bool Expired { get; }
        bool IsPaid { get; }
        string ImageId { get; }
    }
}