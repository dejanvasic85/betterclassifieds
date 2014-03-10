using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserByEmail(string email);
    }
}