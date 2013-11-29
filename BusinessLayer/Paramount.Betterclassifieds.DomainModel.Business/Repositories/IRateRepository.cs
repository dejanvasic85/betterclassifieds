using Paramount.DomainModel.Business.OnlineClassies.Models;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IRateRepository
    {
        IRateModel GetRatecard(int rateId);
    }
}