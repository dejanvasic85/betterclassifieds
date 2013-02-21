namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.ServiceModel;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class DeleteDslDocumentRequest
    {
        [DataMember]
        public string DocumentId;
    }
}
