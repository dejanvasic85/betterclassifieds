using System;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
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

        public OnlineAdRate GetOnlineRateByCategory(int? categoryId)
        {
            // Todo - Simple fetch by category Id.
            // however, if no results we should try finding by parent?

            throw new NotImplementedException();
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Ratecard, RateModel>()
                .ForMember(member => member.FreeWords, options => options.MapFrom(source => source.MeasureUnitLimit))
                .ForMember(member => member.RatePerWord, options => options.MapFrom(source => source.RatePerMeasureUnit));
        }
    }
}