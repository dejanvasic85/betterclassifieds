namespace Paramount.Membership.UIController
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using Common.DataTransferObjects.MembershipService.Messages;
    using Services.Proxy;

    public class MembershipServiceController:MembershipProvider
    {
        private int _schemaVersionCheck;
        private bool _enablePasswordRetrieval;
        private bool _enablePasswordReset;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredPasswordLength;
        private int _minRequiredNonalphanumericCharacters;
        private string _passwordStrengthRegularExpression;
        private MembershipPasswordFormat _passwordFormat;

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var groupingId = Guid.NewGuid().ToString();
            var request = new CreateUserRequest
            {
                Username = username,
                Password = password,
                Email = email,
                PasswordQuestion = passwordQuestion,
                PasswordAnswer = passwordAnswer,
                IsApproved = isApproved
            };
            request.SetBaseRequest(groupingId);
            var response = WebServiceHostManager.MembershipServiceClient.CreateUser(request);
            status = response.CreateStatus;
            return response.Membership.ConvertToExternal();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new ChangePasswordRequest() { Username = username, OldPassword  = oldPassword, NewPassword = newPassword };
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.ChangePassword(request).Result;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new ValidateUserRequest{ Username = username, Password = password};
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.ValidateUser(request).Success;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new GetUserByIdRequest{UserId = (Guid) providerUserKey, UserIsOnline = userIsOnline};
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.GetUserById(request).MembershipUser.ConvertToExternal();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new GetUserByUsernameRequest { Username = username, UserIsOnline = userIsOnline };
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.GetUserByUsername(request).MembershipUser.ConvertToExternal();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override string ApplicationName
        {
            get; set;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>
        /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonalphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "MembershipServiceController";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "MembershipServiceController");
            }
            base.Initialize(name, config);

            _schemaVersionCheck = 0;

            _enablePasswordRetrieval = GetBooleanValue(config, "enablePasswordRetrieval", false);
            _enablePasswordReset = GetBooleanValue(config, "enablePasswordReset", true);
            _requiresQuestionAndAnswer = GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            _requiresUniqueEmail = GetBooleanValue(config, "requiresUniqueEmail", true);
            _maxInvalidPasswordAttempts = GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            _passwordAttemptWindow = GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            _minRequiredPasswordLength = GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            _minRequiredNonalphanumericCharacters = GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);

            _passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (_passwordStrengthRegularExpression != null)
            {
                _passwordStrengthRegularExpression = _passwordStrengthRegularExpression.Trim();
                if (_passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(_passwordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                _passwordStrengthRegularExpression = string.Empty;
            }
            if (_minRequiredNonalphanumericCharacters > _minRequiredPasswordLength)
                throw new HttpException("MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength");

            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";

            switch (strTemp)
            {
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed && EnablePasswordRetrieval)
                throw new ProviderException("Provider_can_not_retrieve_hashed_password");

            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");

        }

        internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string sValue = config[valueName];
            if (sValue == null)
            {
                return defaultValue;
            }

            bool result;
            if (bool.TryParse(sValue, out result))
            {
                return result;
            }
            else
            {
                throw new ProviderException(GetErrorParameterString("Value must be boolean", valueName));
            }
        }

        internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string sValue = config[valueName];

            if (sValue == null)
            {
                return defaultValue;
            }

            int iValue;
            if (!Int32.TryParse(sValue, out iValue))
            {
                if (zeroAllowed)
                {
                    throw new ProviderException(GetErrorParameterString("Value must be non negative integer", valueName));
                }

                throw new ProviderException(GetErrorParameterString("Value must be a potive integer", valueName));
            }

            if (zeroAllowed && iValue < 0)
            {
                throw new ProviderException(GetErrorParameterString("Value must be non negative integer", valueName));
            }

            if (!zeroAllowed && iValue <= 0)
            {
                throw new ProviderException(GetErrorParameterString("Value must be non negative integer", valueName));
            }

            if (maxValueAllowed > 0 && iValue > maxValueAllowed)
            {
                throw new ProviderException(GetErrorParameterString(string.Format("value is too big. Max allowed value is {0}", maxValueAllowed.ToString(CultureInfo.InvariantCulture)), valueName));
            }

            return iValue;
        }

        internal static string GetErrorParameterString(string erorMesssage, string paramaterName )
        {
            return string.Format("Error on Parameter: {0} - {1}", paramaterName, erorMesssage);
        }
    }
}