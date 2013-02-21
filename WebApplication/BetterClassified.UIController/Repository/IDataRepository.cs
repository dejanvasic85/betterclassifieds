using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;
using BetterClassified.UIController.ViewObjects;

namespace BetterClassified.UIController.Repository
{
    public interface IDataRepository
    {
        #region AppSettings
        object GetAppSetting(string settingName);
        #endregion

        #region LineAds
        void AddLineAdColourCode(LineAdColourCode lineAdColourCode);
        void AddLineAdTheme(LineAdTheme lineAdTheme);
        LineAdVo GetLineAd(int lineAdId);
        LineAdVo GetLineAdByBookingId(int adBookingId);
        IList<LineAdColourCode> GetLineAdColourCodes();
        IList<LineAdTheme> GetLineAdThemes();
        LineAdTheme GetLineAdTheme(int lineAdTheme);
        LineAdColourVo GetLineAdColour(int lineAdColourId);
        string GetLineAdBorderColourSuggestion(string headerColour, string backgroundColour);
        string GetLineAdHeaderColourSuggestion(string borderColour, string backgroundColour);
        string GetLineAdBackgroundColourSuggestion(string headerColour, string borderColour);
        void UpdateLineAd(int lineAdId, string adHeader, string adText, int numOfWords, bool isSuperBoldHeader, string headerColour, string borderColour, string backgroundColour);
        void UpdateLineAdColourCodeOrder(Dictionary<int, int> lineAdColourOrders);
        void UpdateLineAdColour(LineAdColourVo lineAdColourVo);
        void UpdateLineAdTheme(int lineAdThemeId, string headerColourCode, string borderColourCode, string backgroundColourCode);
        void DisableLineAdColourCode(int lineAdColourId);
        void DisableLineAdTheme(int lineAdThemeId);
        
        #endregion

        #region Publications
        void AddPublicationRate(int publicationId, int mainCategoryId, int rateCardId, bool isClearCurrentRates);
        IList<Publication> GetPublications();
        IList<int> GetPublicationsForRatecard(int rateCardId);
        #endregion

        #region Categories
        IList<MainCategory> GetMainCategories();
        IList<int?> GetMainCategoriesForRateCard(int rateCardId);
        #endregion

        #region Ratecard
        int AddRateCard(BaseRate baseRate, Ratecard rateCard);
        IList<RateCardSearchResultVo> SearchRateCards();
        RateCardVo GetRateCard(int rateCardId);
        IList<PublicationRateInfoVo> GetRatesForCategory(int mainCategoryId);
        void UpdateRateCard(RateCardVo rateCardVo);
        void DeleteRateCard(int rateCardId, bool isCascaded);
        #endregion
    }
}
