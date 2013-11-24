using System;

namespace Paramount.Betterclassifieds.Domain.Membership
{
    public class Membership
    {
        public Guid? ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
    }
}
