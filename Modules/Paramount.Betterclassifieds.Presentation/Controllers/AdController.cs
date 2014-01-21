using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdRepository adRepository;

        public AdController(IAdRepository adRepository)
        {
            this.adRepository = adRepository;
        }

        public ActionResult Index(string title, int id)
        {
            return View();
        }


        public ActionResult Category(string categoryCode){
            return View();
        }
    }
}