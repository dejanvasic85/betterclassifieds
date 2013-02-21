namespace Paramount.Common.DataTransferObjects.UserAccountService
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccountProfile
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Username { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime CreateDate { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsApproved { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsLockedOut { get; set; }

        [DataMember(IsRequired = true)]
        public int TotalCount { get; set; }
    }
}
