namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class GetUserByIdRequest : BaseRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember(IsRequired = true)]
        public bool UserIsOnline { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
