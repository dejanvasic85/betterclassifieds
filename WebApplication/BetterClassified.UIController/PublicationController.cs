using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;

namespace BetterClassified.UIController
{
    public  class PublicationController : BaseController
    {
        public PublicationController() { }

        public PublicationController(RepositoryType repositoryType) : base(repositoryType) { }

        #region Create
        
        public void AddPublicationRate(List<int> publicationIdList, List<int> categoryIdList, int rateCardId)
        {
            AddPublicationRate(publicationIdList, categoryIdList, rateCardId, true);
        }

        public void AddPublicationRate(List<int> publicationIdList, List<int> categoryIdList, int rateCardId, bool isClearCurrentRates)
        {
            foreach (int publicationId in publicationIdList)
            {
                foreach (int categoryId in categoryIdList)
                {
                    AddPublicationRate(publicationId, categoryId, rateCardId);
                }
            }
        }

        public void AddPublicationRate(int publicationId, int mainCategoryId, int rateCardId)
        {
            AddPublicationRate(publicationId, mainCategoryId, rateCardId, true);
        }

        public void AddPublicationRate(int publicationId, int mainCategoryId, int rateCardId, bool isClearCurrentRates)
        {
            _dataContext.AddPublicationRate(publicationId, mainCategoryId, rateCardId, isClearCurrentRates);
        }

        #endregion

        #region Read
        
        public IList<Publication> GetPublications()
        {
            return _dataContext.GetPublications();
        }

        public IList<int> GetPublicationsForRatecard(int rateCardId)
        {
            return _dataContext.GetPublicationsForRatecard(rateCardId);
        }

        #endregion
    }
}
