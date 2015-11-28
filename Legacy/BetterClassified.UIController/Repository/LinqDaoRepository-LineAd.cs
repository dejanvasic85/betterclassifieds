using System;
using System.Collections.Generic;
using System.Linq;
using BetterClassified.UIController.ViewObjects;
using BetterclassifiedsCore.DataModel;
using Paramount.Common.UI;
using Paramount.Common.UI.WebContent;

namespace BetterClassified.UIController.Repository
{
    public partial class LinqDaoRepository : IDataRepository
    {
        public void AddLineAdColourCode(LineAdColourCode lineAdColourCode)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                db.LineAdColourCodes.InsertOnSubmit(lineAdColourCode);
                db.SubmitChanges();
            }
        }

        public void AddLineAdTheme(LineAdTheme lineAdTheme)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                // Fetch the colour names
                var headerColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == lineAdTheme.HeaderColourCode).FirstOrDefault();
                lineAdTheme.HeaderColourName = headerColour == null ? null : headerColour.LineAdColourName;

                var borderColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == lineAdTheme.BorderColourCode).FirstOrDefault();
                lineAdTheme.BorderColourName = borderColour == null ? null : borderColour.LineAdColourName;

                var backgroundColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == lineAdTheme.BackgroundColourCode).FirstOrDefault();
                lineAdTheme.BackgroundColourName = backgroundColour == null ? null : backgroundColour.LineAdColourName;

                db.LineAdThemes.InsertOnSubmit(lineAdTheme);
                db.SubmitChanges();
            }
        }

        public LineAdVo GetLineAd(int lineAdId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAd = from l in db.LineAds
                             where l.LineAdId == lineAdId
                             select new LineAdVo
                             {
                                 AdDesignid = l.AdDesignId,
                                 AdHeader = l.AdHeader,
                                 AdText = l.AdText,
                                 HeaderColourCode = l.BoldHeadingColourCode,
                                 BackgroundColourCode = l.BackgroundColourCode,
                                 BorderColourCode = l.BorderColourCode,
                                 FooterPhotoId = l.FooterPhotoId,
                                 IsSuperBoldHeading = l.IsSuperBoldHeading.GetValueOrDefault(false),
                                 LineAdId = l.LineAdId,
                                 LineAdBookingView = new LineAdBookingView
                                                         {
                                                             LineAdId = l.LineAdId,
                                                             BoldHeading = l.UseBoldHeader.GetValueOrDefault(false),
                                                             ColourBackground = l.IsColourBackground.GetValueOrDefault(false),
                                                             ColourHeading = l.IsColourBoldHeading.GetValueOrDefault(false),
                                                             ColourBorder = l.IsColourBorder.GetValueOrDefault(false),
                                                             MainImage = l.UsePhoto.GetValueOrDefault(false),
                                                             SecondaryImage = l.IsFooterPhoto.GetValueOrDefault(false),
                                                             NumberOfUnits = l.NumOfWords.GetValueOrDefault(0),
                                                             SuperBoldHeading = l.IsSuperHeadingPurchased.GetValueOrDefault(false)
                                                         }
                             };

                return lineAd.FirstOrDefault();
            }
        }

        public LineAdVo GetLineAdByBookingId(int adBookingId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdVos = from la in db.LineAds
                           join ds in db.AdDesigns on la.AdDesignId equals ds.AdDesignId
                           join bk in db.AdBookings on ds.AdId equals bk.AdId
                           where bk.AdBookingId == adBookingId
                           select new LineAdVo
                                      {
                                          AdDesignid = la.AdDesignId,
                                          AdHeader = la.AdHeader,
                                          AdText = la.AdText,
                                          BackgroundColourCode =  la.BackgroundColourCode,
                                          BorderColourCode = la.BorderColourCode,
                                          FooterPhotoId = la.FooterPhotoId,
                                          HeaderColourCode = la.BoldHeadingColourCode,
                                          LineAdId = la.LineAdId,
                                          IsSuperBoldHeading = la.IsSuperBoldHeading.GetValueOrDefault(false),
                                          LineAdBookingView = new LineAdBookingView
                                          {
                                              LineAdId = la.LineAdId,
                                              BoldHeading = la.UseBoldHeader.GetValueOrDefault(false),
                                              ColourBackground = la.IsColourBackground.GetValueOrDefault(false),
                                              ColourHeading = la.IsColourBoldHeading.GetValueOrDefault(false),
                                              ColourBorder = la.IsColourBorder.GetValueOrDefault(false),
                                              MainImage = la.UsePhoto.GetValueOrDefault(false),
                                              SecondaryImage = la.IsFooterPhoto.GetValueOrDefault(false),
                                              NumberOfUnits = la.NumOfWords.GetValueOrDefault(0),
                                              SuperBoldHeading = la.IsSuperHeadingPurchased.GetValueOrDefault(false)
                                          }
                                      };

                return lineAdVos.FirstOrDefault();
            }
        }

        public IList<LineAdColourCode> GetLineAdColourCodes()
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                return db.LineAdColourCodes.Where(i => i.IsActive == true).OrderBy(i => i.SortOrder).ToList();
            }
        }

        public IList<LineAdTheme> GetLineAdThemes()
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                return db.LineAdThemes.Where(lat => lat.IsActive == true).OrderBy(lat => lat.ThemeName).ToList();
            }
        }

        public LineAdTheme GetLineAdTheme(int lineAdThemeId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                return db.LineAdThemes.Where(lat => lat.LineAdThemeId == lineAdThemeId).FirstOrDefault();
            }
        }

        public LineAdColourVo GetLineAdColour(int lineAdColourId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdColour = db.LineAdColourCodes.Where(lacc => lacc.LineAdColourId == lineAdColourId).FirstOrDefault();

                if (lineAdColour == null)
                    return null;

                return new LineAdColourVo
                {
                    LineAdColourId = lineAdColour.LineAdColourId,
                    LineAdColourName = lineAdColour.LineAdColourName,
                    ColourCode = lineAdColour.ColourCode,
                    Cyan = lineAdColour.Cyan == null ? 0 : (int)lineAdColour.Cyan,
                    Yellow = lineAdColour.Yellow == null ? 0 : (int)lineAdColour.Yellow,
                    Magenta = lineAdColour.Magenta == null ? 0 : (int)lineAdColour.Magenta,
                    KeyCode = lineAdColour.KeyCode == null ? 0 : (int)lineAdColour.KeyCode,
                    IsActive = Convert.ToBoolean(lineAdColour.IsActive),
                    SortOrder = lineAdColour.SortOrder == null ? 0 : (int)lineAdColour.SortOrder,
                    CreatedDate = lineAdColour.CreatedDate == null ? DateTime.MinValue : (DateTime)lineAdColour.CreatedDate,
                    CreatedByUser = lineAdColour.CreatedByUser
                };
            }
        }

        public string GetLineAdBorderColourSuggestion(string headerColour, string backgroundColour)
        {
            string colourSuggestion = string.Empty;

            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                List<LineAdTheme> relatedThemes = new List<LineAdTheme>();

                var relatedTheme = db.usp_LineAdTheme__FetchBorderColour(string.IsNullOrEmpty(headerColour) ? null : headerColour,
                    string.IsNullOrEmpty(backgroundColour) ? null : backgroundColour).FirstOrDefault();


                if (relatedTheme != null)
                {

                    // Grab the first theme border colour
                    colourSuggestion = string.Format(
                        WebContentManager.GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "BorderSuggestion.Text"),
                        relatedTheme.BorderColourName,
                        relatedTheme.BorderColourCode);
                }
            }

            return colourSuggestion;
        }

        public string GetLineAdHeaderColourSuggestion(string borderColour, string backgroundColour)
        {
            string colourSuggestion = string.Empty;

            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                List<LineAdTheme> relatedThemes = new List<LineAdTheme>();

                var relatedTheme = db.usp_LineAdTheme__FetchHeaderColour(string.IsNullOrEmpty(borderColour) ? null : borderColour,
                    string.IsNullOrEmpty(backgroundColour) ? null : backgroundColour).FirstOrDefault();


                if (relatedTheme != null)
                {

                    // Grab the first theme border colour
                    colourSuggestion = string.Format(
                        WebContentManager.GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "HeaderSuggestion.Text"),
                        relatedTheme.HeaderColourName,
                        relatedTheme.HeaderColourCode);
                }
            }

            return colourSuggestion;
        }

        public string GetLineAdBackgroundColourSuggestion(string headerColour, string borderColour)
        {
            string colourSuggestion = string.Empty;

            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                List<LineAdTheme> relatedThemes = new List<LineAdTheme>();

                var relatedTheme = db.usp_LineAdTheme__FetchBackgroundColour(string.IsNullOrEmpty(headerColour) ? null : headerColour,
                    string.IsNullOrEmpty(borderColour) ? null : borderColour).FirstOrDefault();


                if (relatedTheme != null)
                {

                    // Grab the first theme border colour
                    colourSuggestion = string.Format(
                        WebContentManager.GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "BackgroundSuggestion.Text"),
                        relatedTheme.BackgroundColourName,
                        relatedTheme.BackgroundColourCode);
                }
            }

            return colourSuggestion;
        }

        public void UpdateLineAd(int lineAdId, string adHeader, string adText, int numOfWords, bool isSuperBoldHeader, string headerColour, string borderColour, string backgroundColour)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAd = db.LineAds.Where(la => la.LineAdId == lineAdId).FirstOrDefault();
                if (lineAd != null)
                {
                    lineAd.AdHeader = adHeader;
                    lineAd.AdText = adText;
                    lineAd.BoldHeadingColourCode = headerColour;
                    lineAd.BackgroundColourCode = backgroundColour;
                    lineAd.BorderColourCode = borderColour;
                    lineAd.UseBoldHeader = !string.IsNullOrEmpty(adHeader);
                    lineAd.IsColourBackground = !string.IsNullOrEmpty(backgroundColour);
                    lineAd.IsColourBoldHeading = !string.IsNullOrEmpty(headerColour);
                    lineAd.IsColourBorder = !string.IsNullOrEmpty(borderColour);
                    lineAd.IsSuperBoldHeading = isSuperBoldHeader;
                    lineAd.NumOfWords = numOfWords;
                }
                db.SubmitChanges();
            }
        }

        public void UpdateLineAdTheme(int lineAdThemeId, string headerColourCode, string borderColourCode, string backgroundColourCode)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdTheme = db.LineAdThemes.Where(lat => lat.LineAdThemeId == lineAdThemeId).Single();
                // Header
                lineAdTheme.HeaderColourCode = headerColourCode;
                var headerColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == headerColourCode).FirstOrDefault();
                lineAdTheme.HeaderColourName = headerColour == null ? null : headerColour.LineAdColourName;
                // Border
                lineAdTheme.BorderColourCode = borderColourCode;
                var borderColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == borderColourCode).FirstOrDefault();
                lineAdTheme.BorderColourName = borderColour == null ? null : borderColour.LineAdColourName;
                // Background
                lineAdTheme.BackgroundColourCode = backgroundColourCode;
                var backgroundColour = db.LineAdColourCodes.Where(lacc => lacc.ColourCode == backgroundColourCode).FirstOrDefault();
                lineAdTheme.BackgroundColourName = backgroundColour == null ? null : backgroundColour.LineAdColourName;

                db.SubmitChanges();
            }
        }

        public void UpdateLineAdColour(LineAdColourVo lineAdColourVo)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdColour = db.LineAdColourCodes.Where(lacc => lacc.LineAdColourId == lineAdColourVo.LineAdColourId).Single();

                lineAdColour.LineAdColourName = lineAdColourVo.LineAdColourName;
                lineAdColour.ColourCode = lineAdColourVo.ColourCode;
                lineAdColour.Cyan = lineAdColourVo.Cyan;
                lineAdColour.Yellow = lineAdColourVo.Yellow;
                lineAdColour.Magenta = lineAdColourVo.Magenta;
                lineAdColour.KeyCode = lineAdColourVo.KeyCode;
                lineAdColour.IsActive = lineAdColourVo.IsActive;
                lineAdColour.SortOrder = lineAdColour.SortOrder;
                lineAdColour.CreatedDate = lineAdColour.CreatedDate;
                lineAdColour.CreatedByUser = lineAdColour.CreatedByUser;
                db.SubmitChanges();
            }
        }

        public void UpdateLineAdColourCodeOrder(Dictionary<int, int> lineAdColourOrders)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                foreach (var pair in lineAdColourOrders)
                {
                    var lineAdColourCode = db.LineAdColourCodes.Where(lacc => lacc.LineAdColourId == pair.Key).FirstOrDefault();
                    if (lineAdColourCode != null)
                    {
                        lineAdColourCode.SortOrder = pair.Value;
                    }
                }

                db.SubmitChanges();
            }
        }

        public void DisableLineAdTheme(int lineAdThemeId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdTheme = db.LineAdThemes.Where(lat => lat.LineAdThemeId == lineAdThemeId).FirstOrDefault();
                if (lineAdTheme != null)
                {
                    lineAdTheme.IsActive = false;
                }
                db.SubmitChanges();
            }
        }

        public void DisableLineAdColourCode(int lineAdColourId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var lineAdColourCode = db.LineAdColourCodes.Where(lacc => lacc.LineAdColourId == lineAdColourId).FirstOrDefault();
                if (lineAdColourCode != null)
                {
                    lineAdColourCode.IsActive = false;
                    db.SubmitChanges();
                }
            }
        }
    }
}
