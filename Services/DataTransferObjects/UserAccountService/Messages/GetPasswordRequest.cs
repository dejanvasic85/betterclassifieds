using System;

namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetPasswordRequest : BaseRequest
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Answer { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
