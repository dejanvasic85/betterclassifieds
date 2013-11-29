using System.Collections.Generic;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IPublicationEditionModel
    {
        int PublicationId { get; set; }
        string PublicationName { get; set; }
        List<IBookEntryModel> Editions { get; set; }
    }
}