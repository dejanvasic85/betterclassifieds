using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Presentation.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class CategoriesController : Controller, IMappingBehaviour
    {
        private readonly ICategoryManager _manager;

        public CategoriesController(ICategoryManager manager)
        {
            _manager = manager;
        }

        [OutputCache(Duration = 10000)]
        public ActionResult GetTopLevel()
        {
            var categories = _manager.GetTopLevelCategories();

            var models = this.MapList<Category, CategoryViewModel>(categories);
            models.Insert(0, new CategoryViewModel { CategoryId  = null, CategoryName = "All Categories"});
            
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("CategoryCtrlProfile");
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(member => member.CategoryName, options => options.MapFrom(source => source.Title));
        }
    }
}
