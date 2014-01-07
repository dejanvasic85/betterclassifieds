namespace Paramount.DataService
{
    using System.Data;
    using ApplicationBlock.Data;

    public class LocationDataService
    {
        private const string ConfigSection = @"paramount/services";
        public static DataTable GetRegion()
        {
            var df = new DatabaseProxy(Proc.RegionSelect.Name, ConfigSection);
            return df.ExecuteQuery().Tables[0];
        }
    }
}
