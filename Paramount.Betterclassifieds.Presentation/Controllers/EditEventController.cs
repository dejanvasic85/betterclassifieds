using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class EditEventController : Controller
    {
        private readonly EventManager _eventManager;

        public EditEventController(EventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpPost]
        public ActionResult RenameTicket(int ticketId, string newName)
        {
            _eventManager.RenameTicket(ticketId, newName);
            return Json(true);
        }
    }
}