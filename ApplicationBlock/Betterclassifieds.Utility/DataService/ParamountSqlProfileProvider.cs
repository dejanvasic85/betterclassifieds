using Paramount.ApplicationBlock.Data;

namespace Paramount.Betterclassified.Utilities.DataService
{
    using Configuration;
    using System.Collections.Specialized;

    public class ParamountSqlProfileProvider : System.Web.Profile.SqlProfileProvider
    {
        private const string ConfigurationSource = "paramount/membership";
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            // Update the private connection string field in the base class.
            //var connectionString = ConfigReader.GetConnectionString(ConfigurationSource, "AppUserConnection"); //"my new connection string value that I get from a custom decryption class not shown here"			// Set private property of Membership provider.
            config["connectionStringName"] = "AppUserConnection";

            base.Initialize(name, config);
        }

    }
}
