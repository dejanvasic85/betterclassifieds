using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
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
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventController : Controller, IMappingBehaviour
    {
        public ActionResult ViewEventAd(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetByAdId(id);

            if (onlineAdModel == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            _bookingManager.IncrementHits(id);

            var eventModel = _eventManager.GetEventDetailsForOnlineAdId(onlineAdModel.OnlineAdId);
            var eventViewModel = new EventViewDetailsModel(_httpContext,
                this.Url, onlineAdModel, eventModel, _clientConfig);

            return View(eventViewModel);
        }

        [HttpPost]
        public ActionResult ReserveTickets(List<EventTicketRequestViewModel> tickets)
        {
            if (tickets == null || tickets.Count == 0)
            {
                ModelState.AddModelError("Tickets", "No tickets have been selected");
                return Json(new { Errors = ModelState.ToErrors() });
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
            return Json(new { NextUrl = Url.Action("BookTickets", "Event") });
        }

        [HttpGet]
        public ActionResult BookTickets(bool? paymentCancelled = null)
        {
            var ticketReservations = _eventManager.GetTicketReservations(_httpContext.With(s => s.Session).SessionID).ToList();
            var eventId = ticketReservations.FirstOrDefault().With(t => t.EventTicket.With(r => r.EventId));
            if (eventId == null)
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
            viewModel.PaymentCancelled = paymentCancelled;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookTickets(BookTicketsRequestViewModel bookTicketsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Errors = ModelState.ToErrors() });
            }

            ApplicationUser applicationUser;
            if (!User.Identity.IsAuthenticated)
            {
                var registrationModel = this.Map<BookTicketsRequestViewModel, RegistrationModel>(bookTicketsViewModel);
                var result = _userManager.LoginOrRegister(registrationModel, bookTicketsViewModel.Password);

                if (result.LoginResult == LoginResult.BadUsernameOrPassword)
                {
                    return Json(new { LoginFailed = true });
                }

                applicationUser = result.ApplicationUser;
            }
            else
            {
                applicationUser = _userManager.GetUserByEmailOrUsername(User.Identity.Name);
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
            _eventBookingContext.Purchaser = bookTicketsViewModel.FullName;
            if (bookTicketsViewModel.SendEmailToGuests)
            {
                _eventBookingContext.EmailGuestList = bookTicketsViewModel.Reservations.Select(e => e.GuestEmail).ToArray();
            }

            if (eventBooking.Status == EventBookingStatus.Active)
            {
                // No payment required so return a redirect to action json object
                return Json(new { NextUrl = Url.Action("EventBooked", "Event") });
            }

            // Process paypal payment
            var payPalRequest = new EventBookingPayPalRequestFactory().CreatePaymentRequest(eventBooking,
                eventBooking.EventBookingId.ToString(),
                Url.ActionAbsolute("AuthorisePayPal", "Event").Build(),
                Url.ActionAbsolute("BookTickets", "Event", new { paymentCancelled = true }).Build());

            var response = _paymentService.SubmitPayment(payPalRequest);

            _eventManager.SetPaymentReferenceForBooking(eventBooking.EventBookingId, response.PaymentId, PaymentType.PayPal); // paypal just for now
            _eventBookingContext.EventBookingPaymentReference = response.PaymentId;

            return Json(new { NextUrl = response.ApprovalUrl });
        }

        public ActionResult AuthorisePayPal(string payerId)
        {
            if (!_eventBookingContext.EventId.HasValue || !_eventBookingContext.EventBookingId.HasValue)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.Value);

            // Mark booking as paid in our database
            _eventManager.EventBookingPaymentCompleted(_eventBookingContext.EventBookingId, PaymentType.PayPal);

            // Call paypal to let them know we completed our end
            _paymentService.CompletePayment(_eventBookingContext.EventBookingPaymentReference, payerId,
                eventBooking.UserId, eventBooking.TotalCost, eventBooking.EventBookingId.ToString(), TransactionTypeName.EventBookingTickets);

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
            try
            {
                _eventManager.AdjustRemainingQuantityAndCancelReservations(sessionId, eventBooking.EventBookingTickets);

                var ticketHtml = _templatingService.Generate(EventTicketPrintViewModel.Create(Url, _barcodeManager, adDetails, eventDetails, eventBooking), "Tickets");
                var ticketPdfData = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(ticketHtml);

                var viewModel = new EventBookedViewModel(adDetails, eventDetails, eventBooking, this.Url, _clientConfig, _httpContext);
                var eventTicketsBookedNotification = this.Map<EventBookedViewModel, EventTicketsBookedNotification>(viewModel)
                    .WithTickets(ticketPdfData);

                if (eventBooking.TotalCost > 0)
                {
                    var applicationUser = _userManager.GetUserByEmailOrUsername(eventBooking.Email);
                    var invoiceViewModel = new EventBookingInvoiceViewModel(_clientConfig, eventBooking, applicationUser, adDetails.Heading);
                    var invoiceHtml = _templatingService.Generate(invoiceViewModel, "Invoice");
                    var invoicePdf = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(invoiceHtml);
                    eventTicketsBookedNotification.WithInvoice(invoicePdf);
                }

                _broadcastManager.Queue(eventTicketsBookedNotification, eventBooking.Email);
                _eventManager.CreateEventTicketsDocument(eventBooking.EventBookingId, ticketPdfData, ticketsSentDate: DateTime.Now);

                if (_eventBookingContext.EmailGuestList != null && _eventBookingContext.EmailGuestList.Length > 0)
                {
                    foreach (var guest in _eventBookingContext.EmailGuestList)
                    {
                        var eventUrl = Url.AdUrl(adDetails.HeadingSlug, adDetails.AdId, includeSchemeAndProtocol: true, routeName: "Event");
                        var notification = new EventGuestNotificationFactory().Create(_clientConfig, eventDetails, adDetails, eventUrl, _eventBookingContext.Purchaser, guest);
                        _broadcastManager.Queue(notification, guest);
                    }
                }

                return View(viewModel);
            }
            finally
            {
                _eventBookingContext.Clear();
            }
        }

#if DEBUG

        /*
        *   The following endpoints are great for debugging purposes only. 
        *   The cshtml files are used for templates only for producing PDFs
        */

        public ActionResult Tickets(int id)
        {
            var eventBooking = _eventManager.GetEventBooking(id);
            var eventDetails = eventBooking.Event;
            var ad = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);

            var data = EventTicketPrintViewModel.Create(Url, _barcodeManager, ad, eventDetails, eventBooking);
            return View(data.ToList());
        }

        public ActionResult Invoice(int id)
        {
            var eventBooking = _eventManager.GetEventBooking(id);
            var applicationUser = _userManager.GetUserByEmailOrUsername(eventBooking.Email);
            var ad = _searchService.GetByAdOnlineId(eventBooking.Event.OnlineAdId);
            var viewModel = new EventBookingInvoiceViewModel(_clientConfig, eventBooking, applicationUser, ad.Heading);
            return View(viewModel);
        }

        public ActionResult GuestList(int id)
        {
            var eventGuestDetails = _eventManager.BuildGuestList(id).ToList();
            var guests = this.MapList<EventGuestDetails, EventGuestListViewModel>(eventGuestDetails);
            var viewModel = new EventGuestListDownloadViewModel { EventName = "Event Guest List", Guests = guests };

            return View("~/Views/EditAd/EventGuestList.cshtml", viewModel);
        }

#endif

        public ActionResult ValidateBarcode(string barcode)
        {
            var result = _barcodeManager.ValidateTicket(barcode);
            var viewModel = BarcodeValidationViewModel.FromResult(result);
            return View(viewModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Business.Events.EventTicket, EventTicketViewModel>().ReverseMap();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketRequestViewModel>();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketReservedViewModel>()
                .ForMember(m => m.Status, options => options.MapFrom(s => s.StatusAsString.Humanize()))
                .ForMember(m => m.Price, options => options.MapFrom(s => s.Price.GetValueOrDefault() + s.TransactionFee.GetValueOrDefault()))
                .ForMember(m => m.TicketName, options => options.MapFrom(s => s.EventTicket.TicketName))
                ;

            configuration.CreateMap<EventGuestDetails, EventGuestListViewModel>();

            // From View Model
            configuration.CreateMap<EventTicketRequestViewModel, EventTicket>();
            configuration.CreateMap<BookTicketsRequestViewModel, RegistrationModel>();
            configuration.CreateMap<EventTicketReservedViewModel, Business.Events.EventTicketReservation>();
            configuration.CreateMap<EventBookedViewModel, EventTicketsBookedNotification>()
                .ForMember(m => m.DocumentType, options => options.Ignore());

        }

        private readonly HttpContextBase _httpContext;
        private readonly IEventBookingContext _eventBookingContext;
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;
        private readonly IClientConfig _clientConfig;
        private readonly IUserManager _userManager;
        private readonly IPaymentService _paymentService;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IBookingManager _bookingManager;
        private readonly IEventTicketReservationFactory _eventTicketReservationFactory;
        private readonly ITemplatingService _templatingService;
        private readonly IEventBarcodeManager _barcodeManager;

        public EventController(ISearchService searchService, IEventManager eventManager, HttpContextBase httpContext, IClientConfig clientConfig, IUserManager userManager, IEventBookingContext eventBookingContext, IPaymentService paymentService, IBroadcastManager broadcastManager, IBookingManager bookingManager, IEventTicketReservationFactory eventTicketReservationFactory, ITemplatingService templatingService, IEventBarcodeManager barcodeManager)
        {
            _searchService = searchService;
            _eventManager = eventManager;
            _httpContext = httpContext;
            _clientConfig = clientConfig;
            _userManager = userManager;
            _eventBookingContext = eventBookingContext;
            _paymentService = paymentService;
            _broadcastManager = broadcastManager;
            _bookingManager = bookingManager;
            _eventTicketReservationFactory = eventTicketReservationFactory;
            _templatingService = templatingService;
            _barcodeManager = barcodeManager;
            _templatingService = templatingService.Init(this); // This service is tightly coupled to an mvc controller
        }

    }
}
