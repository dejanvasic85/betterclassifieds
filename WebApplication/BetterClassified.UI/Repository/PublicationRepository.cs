using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BetterclassifiedsCore.DataModel;
using Paramount;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class PublicationRepository : IPublicationRepository, IMappingBehaviour
    {
        public PublicationModel GetPublication(int publicationId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return this.Map<Publication, PublicationModel>(context.Publications.First(publication => publication.PublicationId == publicationId));
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

        public List<BookEntryModel> GetEditionsForPublication(int publicationId, DateTime startDate, int numberOfEditions)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                return this.MapList<Edition, BookEntryModel>(context.Editions
                    .Where(edition => edition.PublicationId == publicationId && edition.EditionDate >= startDate)
                    .Take(numberOfEditions)
                    .ToList());
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Edition, BookEntryModel>();
            configuration.CreateMap<Publication, PublicationModel>();
        }
    }
}