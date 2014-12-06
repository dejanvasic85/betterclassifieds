using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UIController.Repository
{
    public partial class LinqDaoRepository : IDataRepository
    {
        #region Read

        public IList<MainCategory> GetMainCategories()
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                return db.MainCategories.ToList().OrderBy(c => c.Title).ToList();
            }
        }

        public IList<int?> GetMainCategoriesForRateCard(int rateCardId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var query = from mc in db.usp_MainCategory__SelectForRateCard(rateCardId)
                            select mc.MainCategoryId;

                return query.ToList();
            }
        }
        #endregion
    }
}
