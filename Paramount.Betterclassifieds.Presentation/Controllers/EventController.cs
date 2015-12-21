using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Monads;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventController : Controller, IMappingBehaviour
    {
        private readonly HttpContextBase _httpContext;
        private readonly EventBookingContext _eventBookingContext;
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;
        private readonly IClientConfig _clientConfig;
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;
        private readonly IPaymentService _paymentService;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IBookingManager _bookingManager;
        private readonly IEventTicketReservationFactory _eventTicketReservationFactory;
        
        public EventController(ISearchService searchService, IEventManager eventManager, HttpContextBase httpContext, IClientConfig clientConfig, IUserManager userManager, IAuthManager authManager, EventBookingContext eventBookingContext, IPaymentService paymentService, IBroadcastManager broadcastManager, IBookingManager bookingManager, IEventTicketReservationFactory eventTicketReservationFactory)
        {
            _searchService = searchService;
            _eventManager = eventManager;
            _httpContext = httpContext;
            _clientConfig = clientConfig;
            _userManager = userManager;
            _authManager = authManager;
            _eventBookingContext = eventBookingContext;
            _paymentService = paymentService;
            _broadcastManager = broadcastManager;
            _bookingManager = bookingManager;
            _eventTicketReservationFactory = eventTicketReservationFactory;
        }

        public ActionResult ViewEventAd(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetByAdId(id);

            if (onlineAdModel == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            _bookingManager.IncrementHits(id);

            var eventModel = _eventManager.GetEventDetailsForOnlineAdId(onlineAdModel.OnlineAdId);
            var eventViewModel = this.Map<Business.Events.EventModel, EventViewDetailsModel>(eventModel);
            this.Map(onlineAdModel, eventViewModel);
            this.Map(_clientConfig, eventViewModel);

            return View(eventViewModel);
        }

        [HttpPost]
        public ActionResult ReserveTickets(List<EventTicketRequestViewModel> tickets)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, Errors = ModelState.ToErrors() });
            }
            var sessionId = _httpContext.With(s => s.Session).SessionID;

            var reservations = new List<EventTicketReservation>();
            foreach (var t in tickets)
            {
                reservations.AddRange(_eventTicketReservationFactory.CreateReservations(
                    t.EventTicketId.GetValueOrDefault(),
                    t.SelectedQuantity,
                    sessionId));
            }

            _eventManager.ReserveTickets(sessionId, reservations);

            return Json(new { IsValid = true, Redirect = Url.Action("BookTickets") });
        }

        [HttpGet]
        public ActionResult BookTickets()
        {
            var ticketReservations = _eventManager.GetTicketReservations(_httpContext.With(s => s.Session).SessionID).ToList();
            var eventId = ticketReservations.FirstOrDefault().With(t => t.EventTicket.With(r => r.EventId));
            if (!eventId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var eventDetails = _eventManager.GetEventDetails(eventId.Value);
            var onlineAdModel = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var remainingTimeToCompleteBooking = _eventManager.GetRemainingTimeForReservationCollection(ticketReservations);

            // Construct the view model
            ApplicationUser applicationUser = null;
            if (this.User.Identity.IsAuthenticated)
            {
                applicationUser = _userManager.GetCurrentUser(this.User);
            }
            var viewModel = new BookTicketsViewModel(onlineAdModel, eventDetails, _clientConfig, applicationUser, ticketReservations)
            {
                Reservations = this.MapList<EventTicketReservation, EventTicketReservedViewModel>(ticketReservations),
            };

            if (remainingTimeToCompleteBooking <= TimeSpan.Zero)
            {
                viewModel.OutOfTime = true;
            }
            else
            {
                viewModel.ReservationExpiryMinutes = remainingTimeToCompleteBooking.Minutes;
                viewModel.ReservationExpirySeconds = remainingTimeToCompleteBooking.Seconds;
            }


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookTickets(BookTicketsRequestViewModel bookTicketsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { ValidationFailed = true, Errors = ModelState.ToErrors() });
            }

            ApplicationUser applicationUser;
            if (!User.Identity.IsAuthenticated)
            {
                // Check if user exists and their password
                var user = _userManager.GetUserByEmail(bookTicketsViewModel.Email);
                var username = string.Empty;
                if (user != null)
                {
                    var loginResult = _authManager.ValidatePassword(user.Username, bookTicketsViewModel.Password);
                    if (!loginResult)
                        return Json(new { LoginFailed = true });

                    applicationUser = user;
                }
                else
                {
                    // Email doesn't exist so create and confirm the registration
                    // Todo we need to send 'another' confirmation and allow the user to login with first confirmation
                    var registration = this.Map<BookTicketsRequestViewModel, RegistrationModel>(bookTicketsViewModel);
                    var registrationResult = _userManager.RegisterUser(registration, bookTicketsViewModel.Password);
                    _userManager.ConfirmRegistration(registrationResult.Registration.RegistrationId.GetValueOrDefault(), registrationResult.Registration.Token);
                    username = registrationResult.Registration.Username;
                    applicationUser = _userManager.GetUserByEmail(bookTicketsViewModel.Email);
                }

                _authManager.Login(username);
            }
            else
            {
                applicationUser = _userManager.GetUserByEmailOrUsername(User.Identity.Name);
                if (!_authManager.ValidatePassword(applicationUser.Username, bookTicketsViewModel.Password))
                    return Json(new { LoginFailed = true });
            }

            // Convert from view model
            var currentReservations = _eventManager.GetTicketReservations(_httpContext.With(ctx => ctx.Session).SessionID).ToArray();

            // We need to capture the guest information per reservation / ticket
            foreach (var reservationViewModel in bookTicketsViewModel.Reservations)
            {
                var viewModel = reservationViewModel;

                currentReservations.SingleOrDefault(r => r.EventTicketReservationId == reservationViewModel.EventTicketReservationId)
                    .Do(r =>
                    {
                        r.GuestEmail = viewModel.GuestEmail;
                        r.GuestFullName = viewModel.GuestFullName;
                        r.TicketFields = viewModel.TicketFields.Select(vm => new EventBookingTicketField { FieldName = vm.FieldName, FieldValue = vm.FieldValue }).ToList();
                    });
            }

            // Process the booking here!
            var eventBooking = _eventManager.CreateEventBooking(bookTicketsViewModel.EventId.GetValueOrDefault(), applicationUser, currentReservations);

            // Set the event id and booking id in the session for the consecutive calls
            _eventBookingContext.EventId = bookTicketsViewModel.EventId.GetValueOrDefault();
            _eventBookingContext.EventBookingId = eventBooking.EventBookingId;

            if (eventBooking.Status == EventBookingStatus.Active)
            {

                // No payment required so return a redirect to action json object
                return Json(new { Successful = true, Redirect = Url.Action("EventBooked") });
            }

            // Check the payment type and process
            var paymentType = bookTicketsViewModel.TotalCost == 0
                ? PaymentType.None
                : bookTicketsViewModel.PaymentMethod.CastToEnum<PaymentType>();

            if (paymentType == PaymentType.CreditCard)
            {
                // Todo Process credit card 
                return Json(new { Successful = true, Redirect = Url.Action("EventBooked") });
            }

            // Process paypal payment
            var payPalRequest = new EventBookingPayPalRequestFactory().CreatePaymentRequest(eventBooking,
                eventBooking.EventBookingId.ToString(),
                Url.ActionAbsolute("AuthorisePayPal", "Event").Build(),
                Url.ActionAbsolute("CancelEventBooking", "Event").Build());

            var response = _paymentService.SubmitPayment(payPalRequest);

            _eventManager.SetPaymentReferenceForBooking(eventBooking.EventBookingId, response.PaymentId, paymentType);
            _eventBookingContext.EventBookingPaymentReference = response.PaymentId;

            return Json(new { Successful = true, Redirect = response.ApprovalUrl, IsPayPal = true });
        }

        public ActionResult AuthorisePayPal(string payerId)
        {
            if (!_eventBookingContext.EventId.HasValue || !_eventBookingContext.EventBookingId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }

            // Mark booking as paid in our database
            _eventManager.EventBookingPaymentCompleted(_eventBookingContext.EventBookingId, PaymentType.PayPal);

            // Call paypal to let them know we completed our end
            _paymentService.CompletePayment(_eventBookingContext.EventBookingPaymentReference, payerId);

            return RedirectToAction("EventBooked");
        }

        public ActionResult CancelEventBooking()
        {
            if (!_eventBookingContext.EventId.HasValue || !_eventBookingContext.EventBookingId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }

            _eventManager.CancelEventBooking(_eventBookingContext.EventBookingId);
            return View();
        }

        public ActionResult EventBooked()
        {
            if (!_eventBookingContext.EventId.HasValue || !_eventBookingContext.EventBookingId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.Value);
            var eventDetails = eventBooking.Event;
            var adDetails = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);

            var sessionId = _httpContext.With(h => h.Session).SessionID;
            _eventManager.AdjustRemainingQuantityAndCancelReservations(sessionId, eventBooking.EventBookingTickets);
            _eventBookingContext.Clear(); // Kill the session at this point so this endpoint will return a 404 next time the user tries

            var ticketPdfData = GenerateTickets(EventTicketPrintViewModel.Create(adDetails, eventDetails, eventBooking));
            var viewModel = new EventBookedViewModel(adDetails, eventDetails, eventBooking, this.Url);
            var eventTicketsBookedNotification = this.Map<EventBookedViewModel, EventTicketsBookedNotification>(viewModel).WithTickets(ticketPdfData);
            _broadcastManager.SendEmail(eventTicketsBookedNotification, eventBooking.Email);
            _eventManager.CreateEventTicketsDocument(eventBooking.EventBookingId, ticketPdfData, ticketsSentDate: DateTime.Now);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GenerateTickets(int id)
        {
            var eventBooking = _eventManager.GetEventBooking(id);
            var eventDetails = eventBooking.Event;
            var onlineAd = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);

            var ticketPdfData = GenerateTickets(EventTicketPrintViewModel.Create(onlineAd, eventDetails, eventBooking));
            var documentId = _eventManager.CreateEventTicketsDocument(id, ticketPdfData);

            return Json(new { documentId });
        }

        public ActionResult Tickets(int id)
        {
            var eventBooking = _eventManager.GetEventBooking(id);
            var eventDetails = eventBooking.Event;
            var onlineAd = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);

            var data = EventTicketPrintViewModel.Create(onlineAd, eventDetails, eventBooking);
            return View(data.ToList());
        }

        private byte[] GenerateTickets(IEnumerable<EventTicketPrintViewModel> data)
        {
            using (var writer = new StringWriter())
            {
                this.ViewData.Model = data;
                var result = ViewEngines.Engines.FindPartialView(this.ControllerContext, "Tickets");
                var viewContext = new ViewContext(this.ControllerContext, result.View, this.ViewData, this.TempData, writer);
                result.View.Render(viewContext, writer);
                result.ViewEngine.ReleaseView(this.ControllerContext, result.View);
                return new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(writer.GetStringBuilder().ToString());
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            #region To View Model

            // To View Model
            configuration.CreateMap<Business.Events.EventModel, EventViewDetailsModel>()
                .ForMember(member => member.EventStartDate, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToLongDateString()))
                .ForMember(member => member.EventStartTime, options => options.ResolveUsing(src => src.EventStartDate.GetValueOrDefault().ToString("hh:mm tt")))
                .ForMember(member => member.EventEndDate, options => options.ResolveUsing(src => src.EventEndDate.GetValueOrDefault().ToLongDateString()))
                ;

            configuration.CreateMap<Business.Search.AdSearchResult, EventViewDetailsModel>()
                .ForMember(m => m.Posted, options => options.PreCondition(src => src.BookingDate.HasValue))
                .ForMember(m => m.Posted, options => options.MapFrom(src => src.BookingDate.Value.Humanize(false, null)))
                .ForMember(m => m.Title, options => options.MapFrom(src => src.Heading))
                .ForMember(m => m.OrganiserName, options => options.MapFrom(src => src.ContactName))
                .ForMember(m => m.OrganiserPhone, options => options.MapFrom(src => src.ContactPhone))
                .ForMember(m => m.Views, options => options.MapFrom(src => src.NumOfViews))
                .ForMember(m => m.EventPhoto, options => options.MapFrom(src => src.PrimaryImage))

                ;

            configuration.CreateMap<Business.Events.EventTicket, EventTicketViewModel>().ReverseMap();
            configuration.CreateMap<Business.IClientConfig, EventViewDetailsModel>().ForMember(m => m.MaxTicketsPerBooking, options => options.MapFrom(src => src.EventMaxTicketsPerBooking));
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketRequestViewModel>();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketReservedViewModel>()
                .ForMember(m => m.Status, options => options.MapFrom(s => s.StatusAsString.Humanize()))
                .ForMember(m => m.Price, options => options.MapFrom(s => s.EventTicket.Price))
                .ForMember(m => m.TicketName, options => options.MapFrom(s => s.EventTicket.TicketName))
                ;
            #endregion

            #region From View model

            // From View Model
            configuration.CreateMap<EventTicketRequestViewModel, EventTicket>();
            configuration.CreateMap<BookTicketsRequestViewModel, RegistrationModel>();
            configuration.CreateMap<EventTicketReservedViewModel, Business.Events.EventTicketReservation>();
            configuration.CreateMap<EventBookedViewModel, EventTicketsBookedNotification>()
                .ForMember(m => m.DocumentType, options => options.Ignore());

            #endregion
        }
    }
}
