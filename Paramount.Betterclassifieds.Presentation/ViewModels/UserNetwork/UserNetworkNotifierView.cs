using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class UserNetworkNotifierView
    {
        public UserNetworkNotifierView()
        { }

        public UserNetworkNotifierView(int adId, IEnumerable<UserNetworkModel> currentUserNetwork)
        {
            this.AdId = adId;
            this.Users = currentUserNetwork.Select(usr => new UserNetworkEmailView
            {
                Email = usr.UserNetworkEmail,
                FullName = usr.FullName,
                Selected = true
            }).ToArray();
        }
        
        public int AdId { get; set; }
        public UserNetworkEmailView[] Users { get; set; }
    }
}