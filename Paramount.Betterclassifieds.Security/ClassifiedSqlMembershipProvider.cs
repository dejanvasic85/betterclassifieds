using System.Configuration;
using System.Reflection;
using System.Web.Security;

namespace Paramount.Betterclassifieds.Security
{
    public class ClassifiedSqlMembershipProvider : SqlMembershipProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            //var connectionString = ConfigReader.GetConnectionString("paramount/services", "AppUserConnection");
            var connectionString = ConfigurationManager.ConnectionStrings["AppUserConnection"].ConnectionString;

            // Set private property of Membership provider.
            var baseType = GetType().BaseType;
            if (baseType != null)
            {
                FieldInfo connectionStringField = baseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
                if (connectionStringField != null) connectionStringField.SetValue(this, connectionString);
            }
        }
    }
}