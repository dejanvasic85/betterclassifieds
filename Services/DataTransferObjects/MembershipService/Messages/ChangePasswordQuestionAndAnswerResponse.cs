namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ChangePasswordQuestionAndAnswerResponse
    {
        [DataMember(IsRequired = true)]
        public bool Result { get; set; }
    }
}