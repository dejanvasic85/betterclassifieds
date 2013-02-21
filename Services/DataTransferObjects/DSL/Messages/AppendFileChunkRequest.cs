namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class AppendFileChunkRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public byte[] Buffer { get; set; }

        [DataMember(IsRequired = true)]
        public Guid FileId { get; set; }

        [DataMember(IsRequired = true)]
        public int Offset { get; set; }

        public AppendFileChunkRequest()
        {
            this.Buffer = new byte[0];
            this.FileId = Guid.Empty;
        }

        public override string TransactionName
        {
            get { return AuditTransactions.AppendFileChunk; }
        }
    }
}
