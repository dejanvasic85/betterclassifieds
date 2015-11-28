using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UIController.Repository
{
    public partial class LinqDaoRepository : IDataRepository
    {
        #region Create
        public void AddPublicationRate(int publicationId, int mainCategoryId, int rateCardId, bool isClearCurrentRates)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                db.usp_PublicationRateCard__Add(publicationId, mainCategoryId, rateCardId, isClearCurrentRates);
            }
        }
        #endregion

        #region Read
        
        public IList<Publication> GetPublications()
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                return db.Publications.ToList();
            }
        }

        public IList<int> GetPublicationsForRatecard(int rateCardId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var query = from p in db.usp_Publication__SelectForRateCard(rateCardId)
                            select p.PublicationId;
                return query.ToList();
            }
        }
        #endregion
    }
}
