namespace Paramount.Betterclassifieds.Business
{
    public class RegistrationOrLoginResult
    {
        public RegistrationOrLoginResult(LoginResult loginResult)
            : this(loginResult, null)
        {
            
        }

        public RegistrationOrLoginResult(LoginResult loginResult, ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
            LoginResult = loginResult;
        }

        public ApplicationUser ApplicationUser { get; private set; }
        public LoginResult LoginResult { get; private set; }
    }
}