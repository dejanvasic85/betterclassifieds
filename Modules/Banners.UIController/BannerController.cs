using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Banners.UIController.ViewObjects;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.Common.DataTransferObjects.Banner.Messages;
using Paramount.Services.Proxy;
using CodeDescription = Paramount.Common.DataTransferObjects.CodeDescription;

namespace Paramount.Banners.UIController
{
    public static class BannerController
    {
        public static BannerGroup GetBannerGroup(string bannerGroupId)
        {
            return GetBannerGroup(bannerGroupId, Guid.Empty.ToString());
        }

        public static BannerGroup GetBannerGroup(string bannerGroupId, string batchGroup)
        {
            var request = new GetBannerGroupRequest { BannerGroupId = new Guid(bannerGroupId) };
            request.SetBaseRequest(batchGroup);
            GetBannerGroupResponse response = WebServiceHostManager.BannerServiceClient.GetBannerGroup(request);
            return response.Groups[0].Convert();
        }

        public static List<BannerGroup> GetBannerGroups()
        {
            var request = new GetBannerGroupRequest();
            request.SetBaseRequest();
            GetBannerGroupResponse response = WebServiceHostManager.BannerServiceClient.GetBannerGroups(request);
            return response.Convert();
        }

        public static List<BannerGroup> GetBannerGroups(string group)
        {
            var request = new GetBannerGroupRequest();
            request.SetBaseRequest();
            GetBannerGroupResponse response = WebServiceHostManager.BannerServiceClient.GetBannerGroups(request);
            return response.Convert();
        }

        public static bool AddBannerGroup(BannerGroup bannerGroup)
        {
            var request = new CreateBannerGroupRequest { BannerGroup = bannerGroup.Convert() };
            request.BannerGroup.GroupId = Guid.NewGuid();
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.CreateBannerGroup(request);
            return true;
        }

        public static bool UpdateBannerGorup(BannerGroup bannerGroup)
        {
            var request = new UpdateBannerGroupRequest { BannerGroup = bannerGroup.Convert() };
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.UpdateBannerGroup(request);
            return true;
        }

        public static List<Banner> GetBanners(Guid groupId, DateTime from, DateTime to)
        {
            var request = new GetBannersRequest { StartDateTime = from, EndDate = to };
            request.SetBaseRequest();
            GetBannersResponse response = WebServiceHostManager.BannerServiceClient.GetBanners(request);
            return response.Convert();
        }

        public static Banner GetBanner(Guid id)
        {
            var request = new GetBannersRequest { BannerId = id, StartDateTime = DateTime.MinValue, EndDate = DateTime.MaxValue };
            request.SetBaseRequest();
            GetBannersResponse response = WebServiceHostManager.BannerServiceClient.GetBanners(request);
            return (response.Convert())[0];
        }

        public static bool SaveBanner(Banner banner)
        {
            if (banner.BannerId == Guid.Empty)
            {
                banner.BannerId = Guid.NewGuid();
            }
            else
            {
                UpdateBanner(banner);
            }
            var request = new CreateBannerRequest { Banner = banner.Convert() };
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.CreateBanner(request);
            return true;
        }

        public static bool UpdateBanner(Banner banner)
        {
            var request = new UpdateBannerRequest { Banner = banner.ConvertModify() };
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.UpdateBanner(request);
            return true;
        }

        public static void LogBannerAudit(BannerAudit audit, string groupingId)
        {
            var request = new LogBannerAuditRequest { Audit = audit.Convert()};
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.LogBannerAudit(request);
        }

       

        public static ServiceInformation GetServiceInformation()
        {
            return WebServiceHostManager.BannerServiceClient.GetApplicationInformation();
        }

        public static void DeleteBanner(Guid bannerId)
        {
            var deleteBannerRequest = new DeleteBannerRequest { Banner = new BannerEntity { BannerId = bannerId } };
            deleteBannerRequest.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.DeleteBanner(deleteBannerRequest);
        }

        public static void RebookBanner(Banner banner)
        {
            var request = new RebookBannerRequest { RebookDetail = banner.ConvertRebook() };
            request.SetBaseRequest();
            WebServiceHostManager.BannerServiceClient.RebookBanner(request);
        }

        public static Banner GetNextBanner(Guid groupId, BannerParameters bannerParams)
        {
            var request = new GetNextBannerAdRequest { GroupId = groupId, Parameters = bannerParams.Convert() };
            request.SetBaseRequest();

            var response = WebServiceHostManager.BannerServiceClient.GetNextBannerAd(request);
            return response.Banner.Convert();
        }

        

     
    }
}