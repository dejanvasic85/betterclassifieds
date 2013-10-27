using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class LineAd
    {
        public int LineAdId { get; set; }
        public int AdDesignId { get; set; }
        public string AdHeader { get; set; }
        public string AdText { get; set; }
        public Nullable<int> NumOfWords { get; set; }
        public Nullable<bool> UsePhoto { get; set; }
        public Nullable<bool> UseBoldHeader { get; set; }
        public Nullable<bool> IsColourBoldHeading { get; set; }
        public Nullable<bool> IsColourBorder { get; set; }
        public Nullable<bool> IsColourBackground { get; set; }
        public Nullable<bool> IsSuperBoldHeading { get; set; }
        public Nullable<bool> IsFooterPhoto { get; set; }
        public string BoldHeadingColourCode { get; set; }
        public string BorderColourCode { get; set; }
        public string BackgroundColourCode { get; set; }
        public string FooterPhotoId { get; set; }
        public Nullable<bool> IsSuperHeadingPurchased { get; set; }
        public virtual AdDesign AdDesign { get; set; }
    }
}
