using Paramount.DomainModel.Business.OnlineClassies.Models;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IAdRepository
    {
        IOnlineAdModel GetOnlineAdByBooking(int bookingId);
        ITutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(ITutorAdModel tutorAdModel);
    }
}