using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Practices.ObjectBuilder2;
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

        public List<OnlineListingModel> SearchOnlineListing(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                List<OnlineAdView> ads = context.spSearchOnlineAdFREETEXT(searchterm, categoryIds.Join(",", true),
                    locationIds.Join(",",true), areaIds.Join(",", true)).Skip(index * pageSize).Take(pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineAdView, OnlineListingModel>(ads);
            }
        }


        
        public List<OnlineAdModel> GetOnlineAdsByCategoryAndLocation(List<int> categoryIds, int? locationId,  int index = 0, int pageSize = 25)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var ads = context.OnlineClassies.Where(c => categoryIds.Contains(c.MainCategoryId.Value) && (locationId.HasValue && c.LocationId == locationId))
                    .OrderByDescending(o => o.OnlineAdId).Skip(index * pageSize).Take(pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineClassie, OnlineAdModel>(ads);
            }
        }

        public List<OnlineAdModel> GetOnlineAdsByLocation(int locationId, int index = 0, int pageSize = 25)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var ads = context.OnlineClassies.Where(c => c.LocationId == locationId)
                    .OrderByDescending(o => o.OnlineAdId).Skip(index * pageSize).Take(pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineClassie, OnlineAdModel>(ads);
            }
        }

        public List<OnlineAdModel> GetOnlineAdsByCategory(List<int> categoryIds, int index = 0, int pageSize = 25)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var ads = context.OnlineClassies.Where(c => categoryIds.Contains(c.MainCategoryId.Value) || (c.ParentId.HasValue && categoryIds.Contains(c.ParentId.Value) ))
                    .OrderByDescending(o => o.OnlineAdId).Skip(index * pageSize).Take(pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineClassie, OnlineAdModel>(ads);
            }
        }


        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("AdRepositoryMaps");

            // From Db
            configuration.CreateMap<OnlineAd, OnlineAdModel>();
            configuration.CreateMap<OnlineAdView, OnlineListingModel>();
            configuration.CreateMap<OnlineClassie, OnlineAdModel>();

            configuration.CreateMap<OnlineAdView, OnlineAdModel>();
            configuration.CreateMap<TutorAd, TutorAdModel>();

            // To Db
            configuration.CreateMap<OnlineAdModel, OnlineAd>()
                .ForMember(member => member.AdDesignId, options => options.Ignore());
            configuration.CreateMap<TutorAdModel, TutorAd>().ForMember(member => member.OnlineAd, options => options.Ignore());
        }
    }

    internal static class Extensions
    {
        public static string Join(this  IEnumerable<string> inputs, string seperator, bool nullIfEmpty = false)
        {
            var returnValue = string.Join(seperator, inputs.EmptyIfNull());

            return string.IsNullOrEmpty(returnValue) && nullIfEmpty ? null : returnValue;
        }

        public static string Join(this  IEnumerable<int> inputs, string seperator, bool nullIfEmpty = false)
        {
            var returnValue = string.Join(seperator, inputs.EmptyIfNull());

            return string.IsNullOrEmpty(returnValue) && nullIfEmpty ? null : returnValue;
        }
    }
}