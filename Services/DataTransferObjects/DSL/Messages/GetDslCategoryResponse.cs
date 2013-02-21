namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.ServiceModel;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class GetDslCategoryResponse
    {
        [DataMember]
        public DslDocumentCategory DocumentCategory;
    }
}
