namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.ServiceModel;

    [MessageContract]
    public class CreateDslDocumentResponse
    {
        [MessageHeader(MustUnderstand = true, Name = "DocumentId")]
        public Guid DocumentId { get; set; }
    }
}
