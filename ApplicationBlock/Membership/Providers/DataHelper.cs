namespace Paramount.ApplicationBlock.Membership.Providers
{
    public class DataHelper
    {
        // todo : Migrate all data services into this project - perform check in the DB
        public static bool IsValid(string entityCode, string password)
        {
            //var entity = CRMDataService.EntitySelect(entityCode, null, null);
            //if (entity.TotalPopulationSize == 1)
            //{
            //    if ((string)entity.Data.Rows[0]["Password"] == password.Trim())
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return true;
        }
    }
}