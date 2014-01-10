namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Data;
    using Paramount.ApplicationBlock.Data;

    public class CRMDataService
    {
        private const string ConfigSection = @"paramount/services";
        private const string ConnectionKey = "";

        internal static string GetNewEntityId()
        {
            var df = new DatabaseProxy(Proc.EntityGetNewId.Name, ConfigSection, ConnectionKey);
            df.AddParameter(new OutputParameter(Proc.EntityGetNewId.OutParams.EntityCode, OutputType.VarChar));
            return (string)df.ExecuteNonQueryWithOutput()[Proc.EntityGetNewId.OutParams.EntityCode];
        }

        public static DataTable GetModuleByEntityCode(string entityCode)
        {
            var df = new DatabaseProxy(Proc.ModuleSelectByEntity.Name, ConfigSection, ConnectionKey);
            df.AddParameter(Proc.ModuleSelectByEntity.Params.EntityCode, entityCode, StringType.VarChar);
            return df.ExecuteQuery().Tables[0];
        }

        public static string CreateNewEntity(string entityName, int primaryContactId, int? timeZone)
        {
            var df = new DatabaseProxy(Proc.EntityInsert.Name, ConfigSection, ConnectionKey);

            df.AddParameter(new OutputParameter(Proc.EntityInsert.Params.EntityCode, OutputType.VarChar));
            df.AddParameter(Proc.EntityInsert.Params.EntityName, entityName, StringType.VarChar);
            df.AddParameter(Proc.EntityInsert.Params.PrimaryContactId, primaryContactId);
            if (timeZone.HasValue)
            {
                df.AddParameter(Proc.EntityInsert.Params.TimeZone, timeZone.Value);
            }

            var entityCode = (string)df.ExecuteQueryWithOutput().Output[Proc.EntityInsert.Params.EntityCode];

            return entityCode;
        }

        public static string EntityUpdate(string entityCode, string entityName, bool isActive, int primaryContactId, int? timeZone)
        {
            var df = new DatabaseProxy(Proc.EntityUpdate.Name, ConfigSection, ConnectionKey);

            df.AddParameter(Proc.EntityUpdate.Params.EntityCode, entityCode, StringType.VarChar);
            df.AddParameter(Proc.EntityUpdate.Params.EntityName, entityName, StringType.VarChar);
            df.AddParameter(Proc.EntityUpdate.Params.PrimaryContactId, primaryContactId);
            df.AddParameter(Proc.EntityUpdate.Params.Active, isActive);
            if (timeZone.HasValue)
            {
                df.AddParameter(Proc.EntityInsert.Params.TimeZone, timeZone.Value);
            }

            var returnEntityCode = (string)df.ExecuteQuery().Tables[0].Rows[0][Proc.EntityUpdate.Params.EntityCode];
            return returnEntityCode;
        }

        public static PagedData EntitySelect(string entityCode, int? pageSize, int? pageIndex)
        {
            var df = new DatabaseProxy(Proc.EntitySelect.Name, ConfigSection, ConnectionKey);
            if (entityCode.HasValue())
            {
                df.AddParameter(Proc.EntitySelect.Params.EntityCode, entityCode, StringType.VarChar);
            }
            if (pageSize.HasValue)
            {
                df.AddParameter(Proc.EntitySelect.Params.PageSize, pageSize.Value);
            }

            if (pageIndex.HasValue)
            {
                df.AddParameter(Proc.EntitySelect.Params.PageIndex, pageIndex.Value);
            }

            df.AddParameter(new OutputParameter(Proc.EntitySelect.Params.TotalPopulationSize, OutputType.Int));
            var result = df.ExecuteQueryWithOutput();
            return new PagedData(result.DataSet.Tables[0], (int)result.Output[Proc.EntitySelect.Params.TotalPopulationSize]);
        }

        public static DataTable ModuleSelect(int? moduleId)
        {
            var df = new DatabaseProxy(Proc.ModuleSelect.Name, ConfigSection, ConnectionKey);
            if (moduleId.HasValue)
            {
                df.AddParameter(Proc.ModuleSelect.Params.ModuleId, moduleId.Value);
            }

            return df.ExecuteQuery().Tables[0];
        }

        public static int EntityModuleUpdate(int moduleId, string entityCode, bool active, DateTime? startDate, DateTime? endDate)
        {
            var df = new DatabaseProxy(Proc.EntityModuleUpdate.Name, ConfigSection, ConnectionKey);
            df.AddParameter(Proc.EntityModuleUpdate.Params.ModuleId, moduleId);
            df.AddParameter(Proc.EntityModuleUpdate.Params.Active, active);
            if (endDate.HasValue)
            {
                df.AddParameter(Proc.EntityModuleUpdate.Params.EndDate, endDate.Value);
            }

            if (startDate.HasValue)
            {
                df.AddParameter(Proc.EntityModuleUpdate.Params.StartDate, startDate.Value);
            }

            df.AddParameter(Proc.EntityModuleUpdate.Params.EntityCode, entityCode, StringType.VarChar);

            return (int)df.ExecuteQuery().Tables[0].Rows[0][Proc.EntityModuleUpdate.Params.EntityModuleId];
        }

        public static int EntityModuleInsert(int moduleId, string entityCode, bool active, DateTime? startDate, DateTime? endDate)
        {
            var df = new DatabaseProxy(Proc.EntityModuleInsert.Name, ConfigSection, ConnectionKey);
            df.AddParameter(Proc.EntityModuleInsert.Params.ModuleId, moduleId);
            df.AddParameter(Proc.EntityModuleInsert.Params.Active, active);
            df.AddParameter(Proc.EntityModuleInsert.Params.EntityCode, entityCode, StringType.VarChar);

            if (endDate.HasValue)
            {
                df.AddParameter(Proc.EntityModuleInsert.Params.EndDate, endDate.Value);
            }

            if (startDate.HasValue)
            {
                df.AddParameter(Proc.EntityModuleInsert.Params.StartDate, startDate.Value);
            }

            return (int)df.ExecuteQuery().Tables[0].Rows[0][Proc.EntityModuleInsert.Params.EntityModuleId];
        }

    }
}
