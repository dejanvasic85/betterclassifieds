namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System.Runtime.Serialization;
    
    [DataContract]
    public class CompleteFileUploadResponse
    {
        [DataMember]
        public FileUploadStatus UploadStatus { get; set; }
    }
}
