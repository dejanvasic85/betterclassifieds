using System.Linq;
using AutoMapper;
using BetterclassifiedsCore.DataModel;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class RateRepository : IRateRepository, IMappingBehaviour
    {
        public RateModel GetRatecard(int rateId)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                // Fetch data objects
                var rateCard = context.Ratecards.First(rate => rate.RatecardId == rateId);

                // Convert to model
                RateModel model = this.Map<Ratecard, RateModel>(rateCard);

                return model;
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