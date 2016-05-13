using System;

namespace Paramount.Betterclassifieds.Business
{
    public class UserNetworkModel
    {
        public UserNetworkModel()
        { }

        public UserNetworkModel(string userId, string userNetworkEmail, string fullName, bool active = true)
        {
            UserId = userId;
            UserNetworkEmail = userNetworkEmail;
            FullName = fullName;
            LastModifiedDate = DateTime.Now;
            LastModifiedDateUtc = DateTime.UtcNow;
            IsUserNetworkActive = active;
        }
        
        public int? UserNetworkId { get; private set; }
        public string UserId { get; private set; }
        public string FullName { get; private set; }
        public string UserNetworkEmail { get; private set; }
        public bool IsUserNetworkActive { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }
        public DateTime? LastModifiedDateUtc { get; set; }

        public string FirstName
        {
            get
            {
                if (FullName.IsNullOrEmpty())
                    return string.Empty;

                return FullName.Split(' ')[0];
            }
        }

        public string Surname
        {
            get
            {
                if (FullName.IsNullOrEmpty())
                    return string.Empty;

                var sections =  FullName.Split(' ');
                if (sections.Length < 2)
                    return string.Empty;

                return sections[1];
            }
        }
    }
}
