namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetMaxChunkSizeResponse
    {
        [DataMember]
        public int ChunkSize { get; set; }
    }
}
