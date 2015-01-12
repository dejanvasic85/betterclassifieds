using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}