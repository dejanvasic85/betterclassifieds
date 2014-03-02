using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}