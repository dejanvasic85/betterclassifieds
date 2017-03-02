using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult EventOrganiserInvite()
        {
            var fakeVm = new EventOrganiserInviteViewModel
            {
                EventName = "The Great Festival",
                Url = Url.Home(),
                FullName = "John Doe",
                SiteAddress = "whitelabel.com.au",
                EventUrl = "http://kandobay.com.au",
                AcceptInvitationUrl = "http://kandobay.com.au"
            };

            return View(fakeVm);
        }
    }
}