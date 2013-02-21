namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DownloadChunkResponse
    {
        [DataMember]
        public byte[] FileChunk { get; set; }

        public DownloadChunkResponse()
        {
            FileChunk = new byte[0];
        }
    }
}
