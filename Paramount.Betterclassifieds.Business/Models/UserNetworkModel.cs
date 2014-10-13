using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Models
{
    public class UserNetworkModel
    {
        public int? UserNetworkId { get; set; }
        public string UserId { get; set; }
        public string UserNetworkEmail { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsUserNetworkActive { get; set; }
    }
}
