using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;
using BetterClassified.UIController.ViewObjects;

namespace BetterClassified.UIController
{
    public class RateController : BaseController
    {
        public RateController() { }

        public RateController(RepositoryType repositoryType) : base(repositoryType) { }

        #region Create

        public int AddRateCard(BaseRate baseRate, Ratecard rateCard)
        {
            return _dataContext.AddRateCard(baseRate, rateCard);
        }

        #endregion

        #region Read

        public IList<RateCardSearchResultVo> SearchRateCards()
        {
            return _dataContext.SearchRateCards();
        }

        public RateCardVo GetRateCard(int rateCardId)
        {
            return _dataContext.GetRateCard(rateCardId);
        }

        public IList<PublicationRateInfoVo> GetRatesForCategory(int mainCategoryId)
        {
            return _dataContext.GetRatesForCategory(mainCategoryId);
        }
        
        #endregion

        #region Update

        public void UpdateRateCard(int RateCardId, RateCardVo rateCardVo)
        {
            rateCardVo.RateCardId = RateCardId;
            _dataContext.UpdateRateCard(rateCardVo);
        }

        #endregion

        #region Delete

        public void DeleteRateCard(int rateCardId)
        {
            DeleteRateCard(rateCardId, true);
        }

        public void DeleteRateCard(int rateCardId, bool isCascaded)
        {
            _dataContext.DeleteRateCard(rateCardId, isCascaded);
        }
        #endregion
    }
}
