using System;

namespace Paramount.Betterclassifieds.Domain.Membership
{
    public class User
    {
        public Guid? ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public string Username { get; set; }
    }
}
