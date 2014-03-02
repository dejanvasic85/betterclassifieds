using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface ICategoryManager
    {
        List<Category> GetCategories();
        List<Category> GetTopLevelCategories();
    }

    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public List<Category> GetTopLevelCategories()
        {
            return GetCategories().Where(c => c.ParentId.IsNullValue()).ToList();
        }
    }
}