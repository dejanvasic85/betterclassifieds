using System;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class AdRepository : IAdRepository, IMappingBehaviour
    {
        public TutorAdModel GetTutorAd(int onlineAdId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                return this.Map<TutorAd, TutorAdModel>(context.TutorAds.FirstOrDefault(onlinead => onlinead.OnlineAdId == onlineAdId));
            }
        }

        public void UpdateTutor(TutorAdModel tutorAdModel)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Fetch original
                var tutorAd = context.TutorAds.Single(t => t.TutorAdId == tutorAdModel.TutorAdId);
                // Map new property changes
                this.Map(tutorAdModel, tutorAd);
                // Commit the changes
                context.SubmitChanges();
            }
        }

        public void CreateAdEnquiry(AdEnquiry adEnquiry)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var dbEnquiry = new OnlineAdEnquiry(); 
                this.Map(adEnquiry, dbEnquiry);                
                dbEnquiry.CreatedDate = DateTime.Now;
                dbEnquiry.EnquiryTypeId = 1;
                dbEnquiry.Active = true;
                context.OnlineAdEnquiries.InsertOnSubmit(dbEnquiry);
                context.SubmitChanges();
            }
        }

        public string GetAdvertiserEmailForAd(int adId)
        {
            using (var classifiedDb = DataContextFactory.CreateClassifiedContext())
            using (var memberDb = DataContextFactory.CreateMembershipContext())
            {
                var username = classifiedDb.AdBookings.Single(bk => bk.AdBookingId == adId).UserId;
                var userId = memberDb.aspnet_Users.Single(u => u.UserName == username).UserId;
                return memberDb.aspnet_Memberships.Single(m => m.UserId == userId).Email;
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("AdRepositoryMaps");

            // From Db
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<OnlineClassie, OnlineAdModel>();
            configuration.CreateMap<TutorAd, TutorAdModel>();

            // To Db
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(member => member.AdDesignId, options => options.Ignore());
            configuration.CreateMap<TutorAdModel, TutorAd>().ForMember(member => member.OnlineAd, options => options.Ignore());
            configuration.CreateMap<AdEnquiry, OnlineAdEnquiry>()
                .ForMember(member => member.EnquiryText, options => options.MapFrom(source => source.Question))
                .ForMember(member => member.OnlineAd, options => options.Ignore());
        }
    }
}