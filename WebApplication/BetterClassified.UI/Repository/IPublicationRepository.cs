using System;
using System.Collections.Generic;
using System.Linq;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UI.Repository
{
    public interface IPublicationRepository
    {
        Publication GetPublication(int publicationId);
        bool IsOnlinePublication(int publicationId);
        List<Edition> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions);
    }

    public class PublicationRepository : IPublicationRepository
    {
        public Publication GetPublication(int publicationId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return context.Publications.First(publication => publication.PublicationId == publicationId);
            }
        }

        public bool IsOnlinePublication(int publicationId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return context.Publications
                    .First(publication => publication.PublicationId == publicationId)
                    .PublicationType
                    .Code
                    .Trim()
                    .Equals("Online", StringComparison.OrdinalIgnoreCase);
            }
        }

        public List<Edition> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return context.Editions
                    .Where(edition => edition.PublicationId == publicationId && edition.EditionDate >= startDate)
                    .Take(numberOfEditions)
                    .ToList();
            }
        }
    }
}