using Paramount.Betterclassifieds.DataService;

namespace Paramount.Services
{
    using System.Data;
    using System.Web.Services;
    using Common.DataTransferObjects.CRM;
    using Common.DataTransferObjects.CRM.Messages;
    using Common.ServiceContracts;

    [WebService(Namespace = "http://paramountit.com.au/webservices/")]
    public class CRMService : ICRMService
    {
        public GetEntityListResponse GetEntityList(GetEntityListRequest request)
        {
            var list = CRMDataService.EntitySelect(string.Empty, request.PageSize, request.PageIndex);
            var response = new GetEntityListResponse { TotalPopulationSize = list.TotalPopulationSize };

            foreach (DataRow item in list.Data.Rows)
            {
                var entity = new ParamountEntity
                                 {
                                     EntityCode = (string)item["EntityCode"],
                                     EntityName = (string)item["EntityName"],
                                     IsActive = bool.Parse(item["Active"].ToString()),
                                     PrimaryContactId = (int)item["PrimaryContactId"],
                                     TimeZone = (int?)item["TimeZone"]
                                 };
                response.Entities.Add(entity);

            }
            return response;
        }

        public GetEntityResponse GetParamountEntity(GetEntityRequest request)
        {
            var entityRow = CRMDataService.EntitySelect(request.EntityCode, null, null);
            var reponse = new GetEntityResponse();
            if (entityRow.Data.Rows.Count == 0)
            {
                return reponse;
            }
            var item = entityRow.Data.Rows[0];

            reponse.Entity = new ParamountEntity
            {
                EntityCode = (string)item["EntityCode"],
                EntityName = (string)item["EntityName"],
                IsActive = bool.Parse(item["Active"].ToString()),
                PrimaryContactId = (int)item["PrimaryContactId"],
                TimeZone = (int?)item["TimeZone"]
            };
            return reponse;
        }

        public GetModuleListResponse GetModuleList(GetModuleListRequest request)
        {
            var response = new GetModuleListResponse();
            var modules = CRMDataService.ModuleSelect(request.ModuleId);

            foreach (DataRow item in modules.Rows)
            {
                response.Modules.Add(new ParamountModule
                                         {
                                             ModuleId = (int)item["ModuleId"],
                                             ModuleDescription = item["Description"].ToString(),
                                             ModuleTitle = item["Title"].ToString()
                                         });
            }
            return response;
        }

        public GetModulesByEntityCodeResponse GetModuleListByEntity(GetModulesByEntityCodeRequest request)
        {
            var response = new GetModulesByEntityCodeResponse();
            var modules = CRMDataService.GetModuleByEntityCode(request.ClientCode);

            foreach (DataRow item in modules.Rows)
            {
                response.Modules.Add(new ParamountModule
                {
                    ModuleId = (int)item["ModuleId"],
                    ModuleDescription = item["Description"].ToString(),
                    ModuleTitle = item["Title"].ToString()
                });
            }
            return response;
        }

        public UpdateCreateEntityResponse UpdateCreateEntity(UpdateCreateEntityRequest request)
        {
            string entityCode;
            if (string.IsNullOrEmpty(request.EntityCode))
            {
                entityCode = CRMDataService.CreateNewEntity(request.EntityName, request.PrimaryContactId,
                                                            request.TimeZone);
            }
            else
            {
                entityCode = CRMDataService.EntityUpdate(request.EntityCode, request.EntityName, request.IsActive,
                                                         request.PrimaryContactId, request.TimeZone);
            }

            return new UpdateCreateEntityResponse { EntityCode = entityCode };
        }

        public UpdateCreateEntityModuleResponse UpdateCreateEnityModule(UpdateCreateEntityModuleRequest request)
        {
            int entityModuleId;
            if (request.EntityModuleId.HasValue)
            {
                entityModuleId = CRMDataService.EntityModuleUpdate(
                    request.ModuleId,
                    request.ClientCode,
                    request.Active,
                    request.StartDate,
                    request.EndDate);
            }
            else
            {
                entityModuleId = CRMDataService.EntityModuleInsert(
                    request.ModuleId,
                    request.ClientCode,
                    request.Active,
                    request.StartDate,
                    request.EndDate);
            }
            return new UpdateCreateEntityModuleResponse { EntityModuleId = entityModuleId };
        }
    }
}
