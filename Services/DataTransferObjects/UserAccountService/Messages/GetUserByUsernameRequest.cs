using System;

namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserByUsernameRequest : BaseRequest
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember(IsRequired = true)]
        public bool UserIsOnline { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
