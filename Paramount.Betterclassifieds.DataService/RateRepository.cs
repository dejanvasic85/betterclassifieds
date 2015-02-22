using System.Collections.Generic;
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
                var rateCard = context.Ratecards.Single(rate => rate.RatecardId == rateId);

                // Convert to model
                RateModel model = this.Map<Ratecard, RateModel>(rateCard);

                return model;
            }
        }

        public RateModel[] GetRatesForPublicationCategory(int[] publications, int? subCategoryId)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var rates = new List<RateModel>();

                foreach (var publication in publications)
                {
                    var rateCard = context.Ratecard_FetchForPublicationCategory(publication, subCategoryId).Single();
                    rates.Add(this.Map<Ratecard, RateModel>(rateCard));
                }

                return rates.ToArray();
            }
        }

        /// <summary>
        /// Fetches the first online ad rate that matches the first category
        /// </summary>
        /// <remarks>
        /// Reason we do this is so that we can support 'inheritance'
        /// </remarks>
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