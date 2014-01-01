using System;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IEditionRepository
    {
        void DeleteEditionByDate(DateTime editionDate);
    }
}