using System;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IBookEntryModel
    {
        int PublicationId { get; set; }
        DateTime EditionDate { get; set; }
        decimal EditionAdPrice { get; set; }
        int AdBookingId { get; set; }
        int BaseRateId { get; set; }
        string RateType { get; set; }
        decimal PublicationPrice { get; set; }
    }
}