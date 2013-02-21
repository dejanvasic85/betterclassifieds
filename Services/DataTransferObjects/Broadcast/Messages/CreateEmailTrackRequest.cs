namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateEmailTrackRequest : BaseRequest
    {
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public string ClientPage { get; set; }
        [DataMember]
        public string Browser { get; set; }
        [DataMember]
        public string EmailBroadcastEntryId { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Postcode { get; set; }
        [DataMember]
        public string TimeZone { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.CreateEmailTrack; }
        }
    }
}