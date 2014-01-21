using System.Collections.Generic;
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

        public List<OnlineAdModel> GetLatestAds(int takeLast = 10)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var ads = context.OnlineAds.OrderByDescending(o => o.OnlineAdId).Take(10).ToList();

                // Map to the models
                return this.MapList<OnlineAd, OnlineAdModel>(ads);
            }
        }

        public List<OnlineAdModel> GetOnlineAdsByCategory(List<int> categoryIds, int index = 0, int pageSize = 25)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var ads = context.OnlineClassies.Where(c =>  categoryIds.Contains(c.MainCategoryId.Value)).Skip(index * pageSize).Take(pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineClassie, OnlineAdModel>(ads);
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
        }
    }
}