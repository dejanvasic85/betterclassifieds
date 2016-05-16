using System;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class AdRepository : IAdRepository, IMappingBehaviour
    {
        private readonly IDbContextFactory _dbContextFactory;

        public AdRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public OnlineAdModel GetOnlineAd(int adId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var ad = from adBooking in context.AdBookings
                    where adBooking.AdBookingId == adId
                    join design in context.AdDesigns on adBooking.AdId equals design.AdId
                    join onlineAd in context.OnlineAds on design.AdDesignId equals onlineAd.AdDesignId
                    select onlineAd;

                return this.Map<OnlineAd, OnlineAdModel>(ad.Single());
            }
        }

        public void UpdateOnlineAd(OnlineAdModel onlineAd)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                // Fetch original
                var original = context.OnlineAds.Single(o => o.OnlineAdId == onlineAd.OnlineAdId);
                // Map new property changes
                this.Map(onlineAd, original);
                // Commit the changes
                context.SubmitChanges();
            }
        }

        public void CreateAdEnquiry(AdEnquiry adEnquiry)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                int? enquiryId = null;
                context.OnlineAdEnquiry_Create(adEnquiry.AdId, adEnquiry.FullName, adEnquiry.Email, adEnquiry.Question, adEnquiry.Phone, ref enquiryId);
                adEnquiry.EnquiryId = enquiryId;
            }
        }

        public string GetAdvertiserEmailForAd(int adId)
        {
            using (var classifiedDb = _dbContextFactory.CreateClassifiedContext())
            using (var memberDb = _dbContextFactory.CreateMembershipContext())
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

            // To Db
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(member => member.Location, options => options.Ignore())
                .ForMember(member => member.LocationArea, options => options.Ignore())
                .ForMember(member => member.AdDesign, options => options.Ignore())
                ;
            
            configuration.CreateMap<AdEnquiry, OnlineAdEnquiry>()
                .ForMember(member => member.EnquiryText, options => options.MapFrom(source => source.Question))
                .ForMember(member => member.OnlineAd, options => options.Ignore());
        }
    }
}