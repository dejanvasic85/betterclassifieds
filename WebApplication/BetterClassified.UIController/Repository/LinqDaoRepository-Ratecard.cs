using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;
using BetterClassified.UIController.ViewObjects;

namespace BetterClassified.UIController.Repository
{
    public partial class LinqDaoRepository : IDataRepository
    {
        #region Create

        public int AddRateCard(BaseRate baseRate, Ratecard rateCard)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                baseRate.Ratecards.Add(rateCard);
                db.BaseRates.InsertOnSubmit(baseRate);
                db.SubmitChanges();
                return rateCard.RatecardId;
            }
        }
        #endregion

        #region Read
        public IList<RateCardSearchResultVo> SearchRateCards()
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var query = from r in db.usp_RateCard__Search()
                            select new RateCardSearchResultVo { 
                                RateCardId = r.RatecardId,
                                RateCardName = r.Title,
                                MinimumCharge = r.MinCharge == null ? 0 : (decimal)r.MinCharge,
                                MaximumCharge = r.MaxCharge == null ? 0 : (decimal)r.MaxCharge,
                                CreatedDate = r.CreatedDate == null ? DateTime.MinValue : (DateTime)r.CreatedDate,
                                CreatedByUser = r.CreatedByUser,
                                PublicationCount = r.PublicationCount == null ? 0 : (int)r.PublicationCount
                            };

                return query.ToList();
            }
        }

        public RateCardVo GetRateCard(int rateCardId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var rateCard = from r in db.Ratecards
                               where r.RatecardId == rateCardId
                               select new RateCardVo
                               {
                                 RateCardId = r.RatecardId,
                                 RateCardName = r.BaseRate.Title,
                                 MinCharge = r.MinCharge == null ? 0 : (decimal)r.MinCharge,
                                 MaxCharge = r.MaxCharge,
                                 RatePerMeasureUnit = r.RatePerMeasureUnit,
                                 MeasureUnitLimit = r.MeasureUnitLimit,
                                 PhotoCharge=r.PhotoCharge,
                                 BoldHeading = r.BoldHeading,
                                 OnlineEditionBundle = r.OnlineEditionBundle,
                                 LineAdSuperBoldHeading = r.LineAdSuperBoldHeading,
                                 LineAdColourHeading=r.LineAdColourHeading,
                                 LineAdColourBorder = r.LineAdColourBorder,
                                 LineAdColourBackground = r.LineAdColourBackground,
                                 LineAdExtraImage = r.LineAdExtraImage,
                                 CreatedDate = r.CreatedDate,
                                 CreatedByUser = r.CreatedByUser
                               };

                return rateCard.FirstOrDefault();
            }
        }

        public IList<PublicationRateInfoVo> GetRatesForCategory(int mainCategoryId)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var publicationRateInfos = (from r in db.Ratecards
                                            join pr in db.PublicationRates on r.RatecardId equals pr.RatecardId
                                            join pc in db.PublicationCategories on pr.PublicationCategoryId equals pc.PublicationCategoryId
                                            join pat in db.PublicationAdTypes on pr.PublicationAdTypeId equals pat.PublicationAdTypeId
                                            join at in db.AdTypes on pat.AdTypeId equals at.AdTypeId
                                            join p in db.Publications on pat.PublicationId equals p.PublicationId
                                            where pc.MainCategoryId == mainCategoryId && p.Active == true
                                            orderby p.SortOrder
                                            select new PublicationRateInfoVo
                                            {
                                                PublicationName = p.Title,
                                                AdTypeId = pat.AdTypeId.Value,
                                                AdTypeCode = at.Code,
                                                MinimumAmount = r.MinCharge.HasValue ? r.MinCharge.Value : 0,
                                                MaximumAmount = r.MaxCharge.HasValue ? r.MaxCharge.Value : 0,
                                                LineAdUnitAmount = r.RatePerMeasureUnit.HasValue ? r.RatePerMeasureUnit.Value : 0,
                                                LineAdFreeUnits = r.MeasureUnitLimit.HasValue ? r.MeasureUnitLimit.Value : 0,
                                                LineAdMainPhotoAmount = r.PhotoCharge.HasValue ? r.PhotoCharge.Value : 0,
                                                LineAdHeaderAmount = r.BoldHeading.HasValue ? r.BoldHeading.Value : 0,
                                                LineAdSuperBoldHeaderAmount = r.LineAdSuperBoldHeading.HasValue ? r.LineAdSuperBoldHeading.Value : 0,
                                                LineAdColourBoldHeaderAmount = r.LineAdColourHeading.HasValue ? r.LineAdColourHeading.Value : 0,
                                                LineAdColourBackgroundAmount = r.LineAdColourBackground.HasValue ? r.LineAdColourBackground.Value : 0,
                                                LineAdColourBorderAmount = r.LineAdColourBorder.HasValue ? r.LineAdColourBorder.Value : 0,
                                                LineAdFooterPhotoAmount = r.LineAdExtraImage.HasValue ? r.LineAdExtraImage.Value : 0
                                            }).ToList();

                return publicationRateInfos;
            }
        }

        #endregion

        #region Update

        public void UpdateRateCard(RateCardVo rateCardVo)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var rateCard = db.Ratecards.Where(r => r.RatecardId == rateCardVo.RateCardId).FirstOrDefault();
                if (rateCard != null)
                {
                    rateCard.BaseRate.Title = rateCardVo.RateCardName;
                    rateCard.MinCharge = rateCardVo.MinCharge;
                    rateCard.MaxCharge = rateCardVo.MaxCharge;
                    rateCard.RatePerMeasureUnit = rateCardVo.RatePerMeasureUnit;
                    rateCard.MeasureUnitLimit = rateCardVo.MeasureUnitLimit;
                    rateCard.PhotoCharge = rateCardVo.PhotoCharge;
                    rateCard.OnlineEditionBundle = rateCardVo.OnlineEditionBundle;
                    rateCard.LineAdSuperBoldHeading = rateCardVo.LineAdSuperBoldHeading;
                    rateCard.LineAdColourHeading = rateCardVo.LineAdColourHeading;
                    rateCard.LineAdColourBorder = rateCardVo.LineAdColourBorder;
                    rateCard.LineAdColourBackground = rateCardVo.LineAdColourBackground;
                    rateCard.LineAdExtraImage = rateCardVo.LineAdExtraImage;
                }

                db.SubmitChanges();
            }
        }

        #endregion

        #region Delete

        public void DeleteRateCard(int rateCardId, bool isCascaded)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                db.usp_RateCard__Delete(rateCardId, isCascaded);
            }
        }
        #endregion
    }
}
