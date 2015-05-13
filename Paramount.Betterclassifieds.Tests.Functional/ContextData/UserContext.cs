using System;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class UserContext
    {
        public DateTime StartRegistrationTime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class AdContext
    {
        public int AdId { get; set; }
    }
}