using System.Collections.Generic;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannerGroupResponse
    {
        public List<BannerGroupEntity> Groups { get; set; }

        public GetBannerGroupResponse()
        {
            Groups = new List<BannerGroupEntity>();
        }
    }
}