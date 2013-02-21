namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAllUsersRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int PageSize { get; set; }

        [DataMember(IsRequired = true)]
        public int PageIndex { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
