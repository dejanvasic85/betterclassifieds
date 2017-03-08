using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;
using System;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [RoutePrefix("email")]
    public class EmailController : Controller
    {
        public EmailController()
        {
        }

        [Route("view/organiser-invite")]
        public ActionResult ViewEventOrganiserInvite()
        {
            var fakeVm = new EventOrganiserInviteViewModel
            {
                EventName = "The Great Festival",
                HomeUrl = Url.Home().WithFullUrl(),
                FullName = "John Doe",
                EventUrl = "http://kandobay.com.au",
                AcceptInvitationUrl = "http://kandobay.com.au"
            };

            return View("EventOrganiserInvite", fakeVm);
        }

        [Route("view/ticket-buyer")]
        public ActionResult ViewEventTicketBuyerNotification()
        {
            var fakeVm = new EventTicketBuyerViewModel
            {
                Address = "1 hello world",
                EventName = "The Great Festival",
                EventUrl = "http://events.com.au",
                StartDate = DateTime.Now.AddMonths(1).ToString("MMMM d yyyy hh:mm")
            };

            return View("EventTicketBuyer", fakeVm);
        }
    }
}