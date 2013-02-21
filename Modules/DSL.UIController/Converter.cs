namespace Paramount.DSL.UIController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ViewObjects;
    using Paramount.Common.DataTransferObjects.DSL;

    public static class Converter
    {
        public static DslDocumentCategoryType Convert(this DslDocumentCategoryTypeView categoryTypeView)
        {
            switch (categoryTypeView)
            {
                case DslDocumentCategoryTypeView.BannerAd:
                    return DslDocumentCategoryType.BannerAd;
                case DslDocumentCategoryTypeView.General:
                    return DslDocumentCategoryType.General;
                case DslDocumentCategoryTypeView.LineAd:
                    return DslDocumentCategoryType.LineAd;
                case DslDocumentCategoryTypeView.Logos:
                    return DslDocumentCategoryType.Logos;
                case DslDocumentCategoryTypeView.OnlineAd:
                    return DslDocumentCategoryType.OnlineAd;
                case DslDocumentCategoryTypeView.Permanent:
                    return DslDocumentCategoryType.Permanent;
                default:
                    throw new NotSupportedException("Invalid category type view.");
            }
        }

        public static DslDocumentCategoryTypeView Convert(this DslDocumentCategoryType categoryType)
        {
            switch (categoryType)
            {
                case DslDocumentCategoryType.BannerAd:
                    return DslDocumentCategoryTypeView.BannerAd;
                case DslDocumentCategoryType.General:
                    return DslDocumentCategoryTypeView.General;
                case DslDocumentCategoryType.LineAd:
                    return DslDocumentCategoryTypeView.LineAd;
                case DslDocumentCategoryType.Logos:
                    return DslDocumentCategoryTypeView.Logos;
                case DslDocumentCategoryType.OnlineAd:
                    return DslDocumentCategoryTypeView.OnlineAd;
                case DslDocumentCategoryType.Permanent:
                    return DslDocumentCategoryTypeView.Permanent;
                default:
                    throw new NotSupportedException("Invalid category type view.");
            }
        }

        public static DslDocumentCategoryView Convert(this DslDocumentCategory documentCategory)
        {
            return new DslDocumentCategoryView
            {
                AcceptedFileTypes = documentCategory.AcceptedFileTypes.ToArray(),
                Code = documentCategory.Code.Convert(),
                DocumentCategoryId = documentCategory.DocumentCategoryId,
                Title = documentCategory.Title,
                ExpiryPurgeDays = documentCategory.ExpiryPurgeDays,
                MaximumFileSize = documentCategory.MaximumFileSize
            };
        }
    }
}
