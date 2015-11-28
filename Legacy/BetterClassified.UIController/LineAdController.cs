using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BetterClassified.UIController.Repository;
using BetterclassifiedsCore.DataModel;
using System.Reflection;
using BetterClassified.UIController.ViewObjects;

namespace BetterClassified.UIController
{
    public class LineAdController : BaseController
    {
        public LineAdController() { }

        public LineAdController(RepositoryType repositoryType) : base(repositoryType) { }

        #region Create

        public void AddLineAdColourCode(LineAdColourCode lineAdColourCode)
        {
            _dataContext.AddLineAdColourCode(lineAdColourCode);
        }

        public void AddLineAdTheme(LineAdTheme lineAdTheme)
        {
            _dataContext.AddLineAdTheme(lineAdTheme);
        }

        #endregion

        #region Read

        public LineAdVo GetLineAd(int lineAdId)
        {
            return _dataContext.GetLineAd(lineAdId);
        }

        public LineAdVo GetLineAdByBookingId(int adBookingId)
        {
            return _dataContext.GetLineAdByBookingId(adBookingId);
        }

        public IList<LineAdColourCode> GetLineAdColourCodes()
        {
            return _dataContext.GetLineAdColourCodes();
        }

        public IList<LineAdTheme> GetLineAdThemes()
        {
            return _dataContext.GetLineAdThemes();
        }

        public LineAdTheme GetLineAdTheme(int lineAdThemeId)
        {
            return _dataContext.GetLineAdTheme(lineAdThemeId);
        }

        public LineAdColourVo GetLineAdColour(int lineAdColourId)
        {
            return _dataContext.GetLineAdColour(lineAdColourId);
        }

        public string GetLineAdBorderColourSuggestion(string headerColour, string backgroundColour)
        {
            return _dataContext.GetLineAdBorderColourSuggestion(headerColour, backgroundColour);
        }

        public string GetLineAdHeaderColourSuggestion(string borderColour, string backgroundColour)
        {
            return _dataContext.GetLineAdHeaderColourSuggestion(borderColour, backgroundColour);
        }

        public string GetLineAdBackgroundColourSuggestion(string headerColour, string borderColour)
        {
            return _dataContext.GetLineAdBackgroundColourSuggestion(headerColour, borderColour);
        }

        #endregion

        #region Update

        public void UpdateLineAd(int lineAdId, string adHeader, int numOfWords, string adText, bool isSuperBoldHeader, string headerColour, string borderColour, string backgroundColour)
        {
            _dataContext.UpdateLineAd(lineAdId, adHeader, adText, numOfWords, isSuperBoldHeader, headerColour, borderColour, backgroundColour);
        }

        public void UpdateLineAdColourOrder(Dictionary<int, int> lineAdColourOrders)
        {
            _dataContext.UpdateLineAdColourCodeOrder(lineAdColourOrders);
        }

        public void UpdateLineAdColour(LineAdColourVo lineAdColourVo)
        {
            _dataContext.UpdateLineAdColour(lineAdColourVo);
        }

        public void UpdateLineAdTheme(int lineAdThemeId, string headerColourCode, string borderColourCode, string backgroundColourCode)
        {
            _dataContext.UpdateLineAdTheme(lineAdThemeId, headerColourCode, borderColourCode, backgroundColourCode);
        }

        #endregion

        #region Delete / Disable

        public void DisableLineAdColourCode(int lineAdColourId)
        {
            _dataContext.DisableLineAdColourCode(lineAdColourId);
        }

        public void DisableLineAdTheme(int lineAdThemeId)
        {
            _dataContext.DisableLineAdTheme(lineAdThemeId);
        }

        #endregion
    }
}
