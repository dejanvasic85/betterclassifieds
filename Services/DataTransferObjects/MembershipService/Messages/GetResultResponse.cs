namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetResultResponse
    {
        [DataMember(IsRequired = true)]
        public bool Result { get; set; }
    }
}
