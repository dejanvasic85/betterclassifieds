using Paramount.ApplicationBlock.Data;

namespace Paramount.Betterclassified.Utilities.DataService
{
    using Configuration;
    using System.Collections.Specialized;

    public class ParamountSqlRoleProvider : System.Web.Security.SqlRoleProvider
    {
        private const string ConfigurationSource = "paramount/membership";
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            // Update the private connection string field in the base class.
            config["connectionStringName"] = "AppUserConnection";
            
            base.Initialize(name, config);
        }

    }
}
