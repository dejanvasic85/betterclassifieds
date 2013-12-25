using BetterClassified.Models;

namespace BetterClassified.Repository
{
    public interface IAdRepository
    {
        OnlineAdModel GetOnlineAdByBooking(int bookingId);
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
    }
}