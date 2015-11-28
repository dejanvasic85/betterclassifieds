namespace Paramount.ApplicationBlock.Membership.Providers
{
    using System.Security.Principal;

    public class ParamountIdentity:IIdentity
    {
        private readonly string _name;
        private readonly bool _isAuthenticated;
        private readonly string _domain;

        public ParamountIdentity(string name, string domain)
        {
            _name = name;
            _isAuthenticated = true;
            _domain = domain;
        }

        private const string Authentication = "ParamountEntity";

        public string Name
        {
            get { return _name; }
        }

        public string Domain { get { return _domain; } }

        public string AuthenticationType
        {
            get { return Authentication; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }
    }
}