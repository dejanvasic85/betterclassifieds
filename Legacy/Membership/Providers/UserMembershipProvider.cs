//using System.Collections.Specialized;

//namespace Paramount.ApplicationBlock.Membership.Providers
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Web.Security;
//    using Exceptions;
//    using System.Configuration;
//    public class UserMembershipProvider : SqlMembershipProvider
//    {
//        private const string DefaultDomain = "/";
//        public string Domain
//        {
//            get { return this.ApplicationName; }
//            set { this.ApplicationName = value; }
//        }

       
//        public UserMembershipProvider():base ()
//        {
           
//        }
//        internal string UsernameAndDomain(string username)
//        {
//            return string.IsNullOrEmpty(this.Domain) ? username : string.Format("{0}\\{1}", this.Domain, username);
//        }

//        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
//        {
//           return base.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey,
//                            out status);
            
//        }

//        public override void Initialize(string name, NameValueCollection config)
//        {
//            config.Remove("applicationName");
//            if (!string.IsNullOrEmpty(this.Domain))
//            {
//                config.Add("applicationName", this.Domain);
//            }
//            else
//            {
//                config.Add("applicationName", ConfigurationManager.AppSettings["domian"]);
//            }
//            base.Initialize(name, config);
//        }
//    }
//}
