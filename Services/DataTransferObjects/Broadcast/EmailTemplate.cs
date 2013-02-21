namespace Paramount.Common.DataTransferObjects.Broadcast
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EmailTemplate
    {
        [DataMember]
        public string ClientCode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string EmailContent { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Sender { get; set; }
    }
}
