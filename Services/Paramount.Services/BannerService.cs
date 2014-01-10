using System.Linq;
using Paramount.Betterclassifieds.DataService;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.Banner.Messages;
using Paramount.Common.ServiceContracts;
using System;

namespace Paramount.Services
{
    public class BannerService : BaseService,IBannerService
    {
        public LogBannerAuditResponse LogBannerAudit(LogBannerAuditRequest request)
        {

            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.AddAudit(request.Audit);
                database.Commit();
            }
            return new LogBannerAuditResponse();
        }

        public GetBannerByGroupIdResponse GetBannerByGroupId(GetBannerByGroupIdRequest request)
        {
            var response = new GetBannerByGroupIdResponse();
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                response.BannerCollection = database.Find(banner => banner.StartDateTime >= request.FromDate && banner.EndDateTime <= request.ToDate && banner.ClientCode == request.ClientCode && (request.GroupId== Guid.Empty || request.GroupId == banner.BannerGroupId)).ToList();
            }
            return response;
        }

        public GetBannersResponse GetBanners(GetBannersRequest request )
        {
            var response = new GetBannersResponse();

            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                if (request.BannerId.HasValue)
                {
                    response.BannerCollection.Add(database.Single(a => a.BannerId == request.BannerId));
                }
                else
                {
                    response.BannerCollection.AddRange(database.Find(a=>a.StartDateTime  >= request.StartDateTime &&  a.EndDateTime <= request.EndDate));
                }
              }

            return response;
        }

        public DeleteBannerResponse DeleteBanner(DeleteBannerRequest request)
        {
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.Delete(request.Banner );
                database.Commit();
            }
            return new DeleteBannerResponse();
        }

        public GetAllBannerFileTypeResponse GetAllFileTypes(GetAllFileTypeRequest request)
        {
            
            var response = new GetAllBannerFileTypeResponse();
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                var t=from a in database.GetAllFileTypes() select new CodeDescription {Code = a.Code, Description = a.Title};
                response.BannerFileTypes.AddRange(t );
            }

            return response;
        }

        public GetNextBannerAdResponse GetNextBannerAd(GetNextBannerAdRequest request)
        {
            
            var response = new GetNextBannerAdResponse();
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                response.Banner = database.GetNextBanner(request.GroupId, request.Parameters );
            }
            return response;
        }

        public CreateBannerResponse CreateBanner(CreateBannerRequest request)
        {
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.Add(request.Banner);
                database.Commit();
            }

            return new CreateBannerResponse();
        }

        public UpdateBannerResponse UpdateBanner(UpdateBannerRequest request)
        {
            
             using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.Update(request.Banner);
                database.Commit();
            }

            return  new UpdateBannerResponse();
        }
        
        public RebookBannerResponse RebookBanner(RebookBannerRequest request)
        {
            //var response = new RebookBannerResponse();
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.ReBookBanner(request.RebookDetail);
                database.Commit();
            }
            return new RebookBannerResponse();
        }

        public UpdateBannerRenderCountResponse UpdateBannerRenderCount(UpdateBannerRenderCountRequest request)
        {
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.UpdateBannerRequestCount(request.BannerId);
                database.Commit();
            }

            return new UpdateBannerRenderCountResponse {ServiceInfo = new ServiceInformation()};
        }

        public UpdateBannerClickCountResponse UpdateBannerClickCount(UpdateBannerClickCountRequest request)
        {
            
            using (var database = new BannerDataProvider(request.ClientCode))
            {
                database.UpdateBannerClickCount(request.BannerId);
                database.Commit();
            }
            return new UpdateBannerClickCountResponse { ServiceInfo = new ServiceInformation() };
        }


        public GetBannerGroupResponse GetBannerGroup(GetBannerGroupRequest request)
        {
            
            var response = new GetBannerGroupResponse();
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                response.Groups.Add(  database.First(a => a.BannerGroupId == request.BannerGroupId));
            }

            return response;
        }

        public GetBannerGroupResponse GetBannerGroups(GetBannerGroupRequest request)
        {
            
            var response = new GetBannerGroupResponse();
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                response.Groups=  database.GetAll().ToList();
            }

            return response;
        }

        public CreateBannerGroupResponse CreateBannerGroup(CreateBannerGroupRequest request)
        {
            
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                database.Add(request.BannerGroup);
                database.Commit();
            }

            return new CreateBannerGroupResponse();
        }

        public DeleteBannerGroupResponse DeleteBannerGroup(DeleteBannerGroupRequest request)
        {
            
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                database.Delete(request.BannerGroup);
                database.Commit();
            }
            return new DeleteBannerGroupResponse();
        }

        public UpdateBannerGroupResponse UpdateBannerGroup(UpdateBannerGroupRequest request)
        {
            
            using (var database = new BannerGroupDataProvider(request.ClientCode))
            {
                database.Update(request.BannerGroup);
                database.Commit();
            }
            return new UpdateBannerGroupResponse();
        }
    }
}