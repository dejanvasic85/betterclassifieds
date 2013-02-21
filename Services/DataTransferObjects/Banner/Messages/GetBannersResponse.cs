using System.Collections.Generic;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannersResponse:BaseResponse
    {
        public List<BannerEntity> BannerCollection { get; set; }

        public GetBannersResponse()
        {
            BannerCollection = new List<BannerEntity>();
        }
    }
}