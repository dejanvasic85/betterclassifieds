using System;

namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;
    using Paramount.ApplicationBlock.Membership;

    [DataContract]
    public class UpdateUserRequest : BaseRequest
    {
        [DataMember]
        public ParamountMembershipUser User { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
