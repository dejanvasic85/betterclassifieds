using System;

namespace Paramount.Common.DataTransferObjects.UserAccountWebService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UnSubscribeUserRequest : BaseRequest
    {
        [DataMember]
        public string UserName { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
