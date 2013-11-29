using System;
using System.Collections.Generic;
using Paramount.DomainModel.Business.OnlineClassies.Models;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IPublicationRepository
    {
        IPublicationModel GetPublication(int publicationId);
        bool IsOnlinePublication(int publicationId);
        List<IBookEntryModel> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions);
    }
}