using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EmailController : Controller
    {
        public EmailController()
        {
        }

        public ActionResult EventOrganiserInvite()
        {
            var fakeVm = new EventOrganiserInviteViewModel
            {
                EventName = "The Great Festival",
                HomeUrl = Url.Home(),
                FullName = "John Doe",
                EventUrl = "http://kandobay.com.au",
                AcceptInvitationUrl = "http://kandobay.com.au"
            };

            return View(fakeVm);
        }
    }
}