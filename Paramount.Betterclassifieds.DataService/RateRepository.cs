using System;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class RateRepository : IRateRepository, IMappingBehaviour
    {
        public RateModel GetRatecard(int rateId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Fetch data objects
                var rateCard = context.Ratecards.First(rate => rate.RatecardId == rateId);

                // Convert to model
                RateModel model = this.Map<Ratecard, RateModel>(rateCard);

                return model;
            }
        }

        /// <summary>
        /// Fetches the first online ad rate that matches the 
        /// </summary>
        public OnlineAdRate GetOnlineRateForCategories(params int?[] categories)
        {
            using (var context = DataContextFactory.CreateClassifiedEntitiesContext())
            {
                foreach (var categoryId in categories)
                {
                    var onlineAdRate = context.OnlineAdRates.FirstOrDefault(rate => rate.CategoryId == categoryId);
                    if (onlineAdRate != null)
                        return onlineAdRate;
                }
                
                // Return the root object
                return context.OnlineAdRates.FirstOrDefault(r => r.CategoryId == null);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Ratecard, RateModel>()
                .ForMember(member => member.FreeWords, options => options.MapFrom(source => source.MeasureUnitLimit))
                .ForMember(member => member.RatePerWord, options => options.MapFrom(source => source.RatePerMeasureUnit));
        }
    }
}