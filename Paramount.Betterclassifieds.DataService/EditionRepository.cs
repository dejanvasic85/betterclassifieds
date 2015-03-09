using System.Data.Linq;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Print;
    using System.Collections.Generic;

    public class EditionRepository : IEditionRepository, IMappingBehaviour
    {
        public void DeleteEditionByDate(DateTime editionDate)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var editionsForDeletion = context.Editions.Where(e => e.EditionDate == editionDate).ToList();
                context.Editions.DeleteAllOnSubmit(editionsForDeletion);
                context.SubmitChanges();
            }
        }

        public List<DateTime> GetUpcomingEditions(DateTime minEditionDate, DateTime minDeadlineDate, params int[] publicationIds)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var ids = string.Join(",", publicationIds);
                var editions = context.Editions_GetUpcomingForPublications(ids, minDeadlineDate, minDeadlineDate)
                    .Select(r => r.EditionDate.GetValueOrDefault())
                    .ToList();

                return editions;
            }
        }
        
        public void OnRegisterMaps(IConfiguration configuration)
        {
            
        }
    }
}