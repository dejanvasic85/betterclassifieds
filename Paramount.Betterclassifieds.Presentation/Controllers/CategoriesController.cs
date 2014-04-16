using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult GetCategoryOptions(bool includeAllOption = true)
        {
            var list = _manager.GetTopLevelCategories().Select(c => new SelectListItem { Text = c.Title, Value = c.CategoryId.ToString() }).ToList();
            if (includeAllOption)
                list.Insert(0, new SelectListItem { Text = "All Categories", Value = string.Empty });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("CategoryCtrlProfile");
        }
    }
}
