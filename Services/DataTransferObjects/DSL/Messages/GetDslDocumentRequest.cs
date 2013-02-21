namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.ServiceModel;

    [MessageContract]
    public class GetDslDocumentRequest
    {
        [MessageHeader(MustUnderstand = true, Name = "DocumentId")]
        public Guid DocumentId;

        [MessageHeader(MustUnderstand = true, Name = "EntityCode")]
        public string EntityCode;

        [MessageHeader(MustUnderstand = true, Name = "FileLength")]
        public int FileLength;

        [MessageBodyMember(Order = 1, Name = "FileData")]
        public System.IO.Stream FileData;
    }
}
