using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Print
{
    public interface IEditionRepository
    {
        void DeleteEditionByDate(DateTime editionDate);
        List<DateTime> GetUpcomingEditions(DateTime minEditionDate, DateTime minDeadlineDate, params int[] publicationIds);
    }
}