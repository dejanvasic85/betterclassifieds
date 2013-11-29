namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IOnlineAdModel 
    {
        int OnlineAdId { get; }
        string Heading { get; }
        string Description { get; }
        string HtmlText { get; }
        decimal? Price { get;}
        int? LocationId { get; }
        int? LocationAreaId { get;}
        string ContactName { get;}
        string ContactType { get; }
        string ContactValue { get; }
        int? NumOfViews { get; }
    }

}