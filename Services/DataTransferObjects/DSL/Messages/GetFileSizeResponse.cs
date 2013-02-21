namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetFileSizeResponse
    {
        [DataMember]
        public long FileSize { get; set; }
    }
}
