namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ChangePasswordQuestionAndAnswerRequest : MembershipProviderBaseRequest
    {
        [DataMember]
        public string Username { get; set;}

        [DataMember]
        public string Password { get; set;}

        [DataMember]
        public string NewPasswordQuestion { get; set;}

        [DataMember]
        public string NewPasswordAnswer{ get; set;}

        public override string TransactionName
        {
            get { return AuditTransactions.ChangePasswordQuestionAndAnswerRequest; }
        }
    }
}