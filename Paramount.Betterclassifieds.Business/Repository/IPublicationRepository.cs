using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IPublicationRepository
    {
        PublicationModel GetPublication(int publicationId);
        bool IsOnlinePublication(int publicationId);
        List<BookEntryModel> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions);
    }
}