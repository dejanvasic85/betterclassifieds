using System;

namespace Paramount.Betterclassifieds.Business
{
    public class UserNetworkModel
    {
        public int? UserNetworkId { get; private set; }
        public string UserId { get; private set; }
        public string UserNetworkEmail { get; private set; }
        public bool IsUserNetworkActive { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        
    }
}
