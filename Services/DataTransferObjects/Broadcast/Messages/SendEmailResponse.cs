namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class SendEmailResponse
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public Guid BroadcastId
        {
            get;
            set;
        }
    }
}