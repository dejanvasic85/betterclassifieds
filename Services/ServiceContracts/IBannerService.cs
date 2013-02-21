using System.ServiceModel;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner.Messages;

namespace Paramount.Common.ServiceContracts
{
    [ServiceContract]
    public interface IBannerService
    {
        [OperationContract]
        LogBannerAuditResponse LogBannerAudit(LogBannerAuditRequest request);

        [OperationContract]
        GetBannerByGroupIdResponse GetBannerByGroupId(GetBannerByGroupIdRequest request);

        [OperationContract]
        DeleteBannerResponse DeleteBanner(DeleteBannerRequest request);

        [OperationContract]
        GetAllBannerFileTypeResponse GetAllFileTypes(GetAllFileTypeRequest request);
        
        [OperationContract]
        GetNextBannerAdResponse GetNextBannerAd(GetNextBannerAdRequest request);

        [OperationContract]
        CreateBannerResponse CreateBanner(CreateBannerRequest request);

        [OperationContract]
        UpdateBannerResponse UpdateBanner(UpdateBannerRequest request);

        [OperationContract]
        GetBannerGroupResponse GetBannerGroup(GetBannerGroupRequest request);

        [OperationContract]
        GetBannerGroupResponse GetBannerGroups(GetBannerGroupRequest request);

        [OperationContract]
        CreateBannerGroupResponse CreateBannerGroup(CreateBannerGroupRequest request);

        [OperationContract]
        DeleteBannerGroupResponse DeleteBannerGroup(DeleteBannerGroupRequest request);

        [OperationContract]
        UpdateBannerGroupResponse UpdateBannerGroup(UpdateBannerGroupRequest request);

        [OperationContract]
        ServiceInformation GetApplicationInformation();

        [OperationContract]
        GetBannersResponse GetBanners(GetBannersRequest request);

        [OperationContract]
        RebookBannerResponse RebookBanner(RebookBannerRequest request);

        [OperationContract]
        UpdateBannerRenderCountResponse UpdateBannerRenderCount(UpdateBannerRenderCountRequest request);

        [OperationContract]
        UpdateBannerClickCountResponse UpdateBannerClickCount(UpdateBannerClickCountRequest request);
    }
}