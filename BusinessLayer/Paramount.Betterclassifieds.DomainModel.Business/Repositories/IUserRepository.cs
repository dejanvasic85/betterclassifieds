using Paramount.DomainModel.Business.OnlineClassies.Models;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IUserRepository
    {
        IApplicationUser GetClassifiedUser(string username);
    }
}