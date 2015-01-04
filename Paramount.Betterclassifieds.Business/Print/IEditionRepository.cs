using System;

namespace Paramount.Betterclassifieds.Business.Print
{
    public interface IEditionRepository
    {
        void DeleteEditionByDate(DateTime editionDate);
    }
}