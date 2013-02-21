using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannerByGroupIdResponse
    {
        public IList<BannerEntity> BannerCollection { get; set; }

        public GetBannerByGroupIdResponse()
        {
            this.BannerCollection = new Collection<BannerEntity>();
        }
    }
}