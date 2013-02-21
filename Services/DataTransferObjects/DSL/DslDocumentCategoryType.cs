namespace Paramount.Common.DataTransferObjects.DSL
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;
 
    public enum DslDocumentCategoryType
    {
        General = 1,
        Logos = 2,
        Permanent = 3,
        OnlineAd = 4,
        LineAd = 5,
        BannerAd = 6
    }

    [DataContract]
    public class DslDocumentCategory
    {
        [DataMember]
        public int DocumentCategoryId { get; set; }
        [DataMember]
        public DslDocumentCategoryType Code { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int? ExpiryPurgeDays { get; set; }
        [DataMember]
        public List<string> AcceptedFileTypes { get; set; }
        [DataMember]
        public int? MaximumFileSize { get; set; }
    }
}
