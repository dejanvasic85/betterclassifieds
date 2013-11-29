namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface ILineAdModel
    {
        int? LineAdId { get; set; }
        string AdHeader { get; set; }
        string AdText { get; set; }
        int NumOfWords { get; set; }
        bool UsePhoto { get; set; }
        bool UseBoldHeader { get; set; }
        bool IsColourBoldHeading { get; set; }
        bool IsColourBorder { get; set; }
        bool IsColourBackground { get; set; }
        bool IsSuperBoldHeading { get; set; }
        string BoldHeadingColourCode { get; set; }
        string BorderColourCode { get; set; }
        string BackgroundColourCode { get; set; }
        bool IsSuperHeadingPurchased { get; set; }
        int GetWordCount();
    }
}