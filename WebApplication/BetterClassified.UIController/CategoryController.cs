using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UIController
{
    public class CategoryController : BaseController
    {
        public CategoryController() { }

        public CategoryController(RepositoryType repositoryType) : base(repositoryType) { }

        public IList<MainCategory> GetMainCategories()
        {
            return _dataContext.GetMainCategories();
        }

        public IList<int?> GetMainCategoriesForRatecard(int rateCardId)
        {
            return _dataContext.GetMainCategoriesForRateCard(rateCardId);
        }
    }
}
