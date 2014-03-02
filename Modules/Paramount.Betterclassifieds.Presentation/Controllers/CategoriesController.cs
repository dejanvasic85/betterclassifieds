using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Presentation.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class CategoriesController : ApiController, IMappingBehaviour
    {
        public CategoriesController()
        {
            
        }

        // GET api/Categories
        public IEnumerable<CategoryViewModel> GetTopLevel()
        {
            //var categories = _manager.GetTopLevelCategories();

            //var models = this.MapList<Category, CategoryViewModel>(categories);

            // return models;
            return new List<CategoryViewModel>
                {
                    new CategoryViewModel { CategoryId = null, CategoryName = "All Categories"},
                    new CategoryViewModel { CategoryId = 1, CategoryName = "Test"},
                };
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("CategoryCtrlProfile");
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(member => member.CategoryName, options => options.MapFrom(source => source.Title));
        }
    }
}
