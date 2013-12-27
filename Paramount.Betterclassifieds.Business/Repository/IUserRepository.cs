using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IUserRepository
    {
        ApplicationUser GetClassifiedUser(string username);
    }
}