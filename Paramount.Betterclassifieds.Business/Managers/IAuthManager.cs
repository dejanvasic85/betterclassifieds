namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IAuthManager
    {
        void Login(string username, bool createPersistentCookie, string userData);
        void Logout();
        bool ValidatePassword(string username, string password);
    }
}