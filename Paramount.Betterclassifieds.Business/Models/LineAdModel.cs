namespace Paramount.Betterclassifieds.Business.Models
{
    public class LineAdModel : Ad
    {
        public int? LineAdId { get; set; }
        public string AdHeader { get; set; }
        public string AdText { get; set; }
        public int NumOfWords { get; set; }
        public bool UsePhoto { get; set; }
        public bool UseBoldHeader { get; set; }
        public bool IsColourBoldHeading { get; set; }
        public bool IsColourBorder { get; set; }
        public bool IsColourBackground { get; set; }
        public bool IsSuperBoldHeading { get; set; }
        public string BoldHeadingColourCode { get; set; }
        public string BorderColourCode { get; set; }
        public string BackgroundColourCode { get; set; }
        public bool IsSuperHeadingPurchased { get; set; }

        public int GetWordCount()
        {
            if (!string.IsNullOrEmpty(AdText))
                return AdText.Split(' ').Length;
            return 0;
        }
    }
}