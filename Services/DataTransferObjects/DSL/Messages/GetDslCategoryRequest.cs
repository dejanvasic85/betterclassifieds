namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;
   
    [DataContract]
    public class GetDslCategoryRequest
    {
        [DataMember]
        public DslDocumentCategoryType DslCategoryType { get; set; }
    }
}
