using System;
using System.Runtime.Serialization;

namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    [DataContract]
    public class GetPasswordResponse : BaseRequest
    {
        [DataMember]
        public string Password { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
