namespace Paramount.Common.UIController.ViewObjects
{
    using System;

    public class UserAccountProfileView
    {
        public string Email { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string LoweredUserName { get { return Username.ToLower(); } }
        public string LoweredEmail { get { return Email.ToLower(); } }

        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
