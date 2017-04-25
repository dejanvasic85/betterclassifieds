using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;
using System;
using System.Linq;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [RoutePrefix("email")]
    public class EmailController : Controller
    {
        private readonly ISearchService _searchService;

        public EmailController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [Route("events/organisers/day")]
        public ActionResult SendDailySummaryToOrganisers(string dateTime = "")
        {
            var targetDate = dateTime.TryParseDateOrDefault(DateTime.Today);

            // Retrieve all the current events
            var eventsToNotify = _searchService.GetEvents();
            
            return Json(new {EventsQueued = eventsToNotify.Count(), TargetDate = targetDate});
        }

        #region Test View Endpoints

        [Route("view/organiser-invite")]
        public ActionResult ViewEventOrganiserInvite()
        {
            var fakeVm = new EventOrganiserInviteEmail
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
            var fakeVm = new EventTicketBuyerEmail
            {
                Address = "1 hello world",
                EventName = "The Great Festival",
                EventUrl = "http://events.com.au",
                StartDate = DateTime.Now.AddMonths(1).ToString("MMMM d yyyy hh:mm")
            };

            return View("EventTicketBuyer", fakeVm);
        }

        [Route("view/welcome")]
        public ActionResult Welcome()
        {
            return View("Welcome", new WelcomeEmail
            {
                BrandName = "BlandBrand",
                Email = "Foo@Bar.com",
                HomeUrl = "http://kandobay.com.au",
                Username = "foo bar"
            });
        }

        [Route("view/forgot-password")]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword", new ForgotPasswordEmail()
            {
                Email = "Foo@Bar.com",
                LoginUrl = "http://kandobay.com.au",
                NewPassword = "Password123"
            });
        }

        [Route("view/confirm-registration")]
        public ActionResult ConfirmRegistration()
        {
            return View("RegistrationConfirmation", new RegisrationConfirmationEmail
            {
                Email = "foo@bar.com",
                Token = "Token123"
            });
        }

        [Route("view/listing-complete")]
        public ActionResult ListingComplete()
        {
            return View("ListingCompleteView", new ListingCompleteEmail
            {
                Heading = "Hello 123",
                Id = 123,
                Email = "foo@bar.com",
                ListingUrl = "http://listing123",
                ListingDate = DateTime.Today.AddDays(1).AddHours(20)
            });
        }

        [Route("view/tickets")]
        public ActionResult Tickets()
        {
            return View("EventTicketGuest", new EventTicketGuestEmail
            {
                BarcodeImgUrl = "https://betterclassifieds.local/img/dfcce154-6ae4-43a9-aa96-67322a512dd5/200/200",
                BuyerName = "Frank Costanza",
                EventLocation = "The Moon",
                EventName =  "Moon Party",
                EventStartDateTime = DateTime.Today.AddDays(1).ToLongDateString(),
                EventUrl = "www.google.com.au",
                IsGuestTheBuyer = true,
            });
        }

        #endregion  
    }
}