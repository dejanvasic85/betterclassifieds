using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}