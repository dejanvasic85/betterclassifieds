using System;

namespace Paramount.Betterclassifieds.Business
{
    public class UserNetworkModel
    {
        public UserNetworkModel()
        { }

        public UserNetworkModel(string userId, string userNetworkEmail, string fullName, bool active = true)
        {
            this.UserId = userId;
            this.UserNetworkEmail = userNetworkEmail;
            this.FullName = fullName;
            this.LastModifiedDate = DateTime.Now;
            this.LastModifiedDateUtc = DateTime.UtcNow;
            this.IsUserNetworkActive = active;
        }
        
        public int? UserNetworkId { get; private set; }
        public string UserId { get; private set; }
        public string FullName { get; private set; }
        public string UserNetworkEmail { get; private set; }
        public bool IsUserNetworkActive { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }
        public DateTime? LastModifiedDateUtc { get; set; }

    }
}
