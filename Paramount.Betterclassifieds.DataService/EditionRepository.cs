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
        private readonly IDbContextFactory _dbContextFactory;

        public EditionRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void DeleteEditionByDate(DateTime editionDate)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var editionsForDeletion = context.Editions.Where(e => e.EditionDate == editionDate).ToList();
                context.Editions.DeleteAllOnSubmit(editionsForDeletion);
                context.SubmitChanges();
            }
        }

        public List<DateTime> GetUpcomingEditions(DateTime minEditionDate, DateTime minDeadlineDate, int max = 50, params int[] publicationIds)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var ids = string.Join(",", publicationIds);
                var editions = context.Editions_GetUpcomingForPublications(ids, minEditionDate, minDeadlineDate)
                    .Select(r => r.EditionDate.GetValueOrDefault())
                    .Take(max)
                    .ToList();

                return editions;
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {

        }
    }
}