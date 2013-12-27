namespace Paramount.Betterclassifieds.Business.Repository
{
    using Models;

    public interface IAdRepository
    {
        OnlineAdModel GetOnlineAdByBooking(int bookingId);
        TutorAdModel GetTutorAd(int onlineAdId);
        void UpdateTutor(TutorAdModel tutorAdModel);
    }
}