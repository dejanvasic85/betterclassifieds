using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Security;
using Paramount.ApplicationBlock.Configuration;
using Paramount.ApplicationBlock.Data;


namespace BetterClassified.UI.Framework.Security
{
    public class ClassifiedSqlMembershipProvider : SqlMembershipProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            var connectionString = ConfigReader.GetConnectionString("paramount/services", "AppUserConnection");

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
