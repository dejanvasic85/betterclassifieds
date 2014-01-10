namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Web.Security;
    using LinqObjects;

    public class MembershipDataProvider : MembershipProvider, IDisposable
    {
        private MembershipProvider _provider;
        private const string ProviderName = "InternalMembership";
        private readonly string _clientProviderName;

   

        public MembershipDataProvider(string clientCode, string clientProviderName)
        {
            ClientCode = clientCode;
            _clientProviderName = clientProviderName;
            _provider = Membership.Providers[ProviderName];
        }

        public void Dispose()
        {
            _provider = null;
            GC.SuppressFinalize(this);
        }

        public string ClientCode { get; private set; }

        private string GetUsername(string username)
        {
            return string.Format("{0}{1}{2}", ClientCode, char.ConvertFromUtf32(254), username);
        }

        private string GetUserEmail(string username)
        {
            return string.Format("{0}{1}{2}", ClientCode, char.ConvertFromUtf32(254), username);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var user = _provider.CreateUser(GetUsername(username), password, GetUserEmail(email), passwordQuestion, passwordAnswer, isApproved,
                            providerUserKey, out status);
            return user.ConvertToExternal(_clientProviderName);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return _provider.ChangePasswordQuestionAndAnswer(GetUsername(username), password, newPasswordQuestion, newPasswordAnswer);
        }

        public override string GetPassword(string username, string answer)
        {
            return _provider.GetPassword(GetUsername(username), answer);
        }
        
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _provider.ChangePassword(GetUsername(username), oldPassword, newPassword);
        }

        public override void UpdateUser(MembershipUser user)
        {
            _provider.UpdateUser(user.ConvertToInternal(ClientCode));
        }

        public override string ResetPassword(string username, string answer)
        {
            return _provider.ResetPassword(GetUsername(username), answer);
        }

        public override bool ValidateUser(string username, string password)
        {
            return _provider.ValidateUser(GetUsername(username), password);
        }

        public override bool UnlockUser(string userName)
        {
            return _provider.UnlockUser(GetUsername(userName));
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = _provider.GetUser(providerUserKey, userIsOnline);
            return user.ConvertToExternal(_clientProviderName);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _provider.GetUser(GetUsername(username), userIsOnline);
            return user.ConvertToExternal(_clientProviderName);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _provider.GetUserNameByEmail(GetUserEmail(email));
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return _provider.DeleteUser(GetUsername(username), deleteAllRelatedData);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            var users= FindUsersByName(GetUsername(""), pageIndex, pageSize, out totalRecords);


            foreach ( MembershipUser user in users)
            {
                collection.Add(user.ConvertToExternal(_clientProviderName));
            }

            return collection;
        }

        public override int GetNumberOfUsersOnline()
        {
            return _provider.GetNumberOfUsersOnline();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _provider.FindUsersByName(GetUsername(usernameToMatch), pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _provider.FindUsersByEmail(GetUserEmail(emailToMatch), pageIndex, pageSize, out totalRecords);
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _provider.EnablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset
        {
            get { return _provider.EnablePasswordReset ; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _provider.RequiresQuestionAndAnswer; }
        }

        public override string ApplicationName
        {
            get { return _provider.ApplicationName ; }
            set { _provider.ApplicationName  =value ; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _provider.MaxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _provider.PasswordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _provider.RequiresUniqueEmail; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _provider.PasswordFormat; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _provider.MinRequiredPasswordLength ; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _provider.MinRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _provider.PasswordStrengthRegularExpression; }
        }
    }
}