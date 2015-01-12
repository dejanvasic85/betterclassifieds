namespace Paramount.Betterclassifieds.Business.Print
{
    using System;
    using System.Collections.Generic;
    
    public interface IPublicationRepository
    {
        PublicationModel GetPublication(int publicationId);
        bool IsOnlinePublication(int publicationId);
        List<BookEntryModel> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions);
    }
}