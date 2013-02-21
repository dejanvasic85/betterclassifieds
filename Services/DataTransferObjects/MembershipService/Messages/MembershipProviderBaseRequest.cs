namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class MembershipProviderBaseRequest:BaseRequest 
    {
        [DataMember]
        public string ProviderName { get; set; }
    }
}