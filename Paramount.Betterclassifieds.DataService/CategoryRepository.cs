using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class CategoryRepository : ICategoryRepository, IMappingBehaviour
    {
        public List<Category> GetCategories()
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var categories = context.MainCategories.ToList();

                return this.MapList<MainCategory, Category>(categories);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From DB
            configuration.CreateMap<MainCategory, Category>()
                .ForMember(member => member.CategoryId, options => options.MapFrom(source => source.MainCategoryId));
        }
    }
}