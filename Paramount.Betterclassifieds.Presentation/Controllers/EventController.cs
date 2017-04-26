using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Organisers;
using Paramount.Betterclassifieds.Business.Events.Reservations;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Framework;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    public class EventController : ApplicationController, IMappingBehaviour
    {
        public ActionResult ViewEventAd(int id, string titleSlug = "")
        {
            var onlineAdModel = _searchService.GetByAdId(id);
            if (onlineAdModel == null)
            {
                return Url.NotFound().ToRedirectResult();
            }

            var isCurrentUserTheOwner = onlineAdModel.Username == _userManager.GetCurrentUser()?.Username;
            if (onlineAdModel.HasNotStarted() && !isCurrentUserTheOwner)
            {
                return Url.NotFound().ToRedirectResult();
            }

            if (!isCurrentUserTheOwner)
            {
                _bookingManager.IncrementHits(id);
            }

            var eventModel = _eventManager.GetEventDetailsForOnlineAdId(onlineAdModel.OnlineAdId);
            var guestList = _eventManager.BuildGuestList(eventModel.EventId);
            _eventBookingContext.EventUrl = Url.AdUrl(titleSlug, id, onlineAdModel.CategoryAdType);

            var eventViewModel = new EventViewDetailsModel(_httpContext,
                Url, onlineAdModel, eventModel, _clientConfig, guestList);

            return View(eventViewModel);
        }

        [HttpPost]
        public ActionResult ReserveTickets(ReserveTicketsViewModel reserveTicketsViewModel)
        {
            var tickets = reserveTicketsViewModel.Tickets;
            var eventId = reserveTicketsViewModel.EventId;

            if (tickets == null || tickets.Count == 0)
            {
                ModelState.AddModelError("Tickets", "No tickets have been selected");
                return JsonModelErrors(); ;
            }

            var eventModel = _eventManager.GetEventDetails(eventId);
            if (eventModel.IsClosed)
            {
                ModelState.AddModelError("Tickets", "The event is closed and is not accepting any more ticket purchases.");
                return JsonModelErrors(); ;
            }

            if (eventModel.GroupsRequired.GetValueOrDefault() && tickets.Any(t => !t.EventGroupId.HasValue))
            {
                ModelState.AddModelError("Tickets", "The event requires a group to be selected with each ticket.");
                return JsonModelErrors(); ;
            }

            if (!_ticketRequestValidator.IsSufficientTicketsAvailableForRequest(reserveTicketsViewModel.Tickets.Select(t => new TicketReservationRequest(t.EventTicketId.GetValueOrDefault(), t.EventGroupId, t.SelectedQuantity)).ToArray()))
            {
                ModelState.AddModelError("Tickets", "The requested ticket quantity is no longer available. Please reload the page and try again.");
                return JsonModelErrors(); ;
            }

            _eventBookingContext.Clear();
            _eventBookingContext.EventInvitationId = reserveTicketsViewModel.EventInvitationId;

            var sessionId = _httpContext.With(s => s.Session).SessionID;
            var reservations = new List<EventTicketReservation>();
            foreach (var t in tickets)
            {
                reservations.AddRange(_eventTicketReservationFactory.CreateReservations(
                    t.EventTicketId.GetValueOrDefault(),
                    t.SelectedQuantity,
                    sessionId,
                    t.EventGroupId));
            }

            _eventManager.ReserveTickets(sessionId, reservations);
            return Json(new { NextUrl = Url.EventBookTickets().Build() });
        }

        [HttpGet, EnsurePaymentNotInProgress, Authorize]
        public ActionResult BookTickets(bool? paymentCancelled = null)
        {
            var ticketReservations = _eventManager.GetTicketReservations(_httpContext.With(s => s.Session).SessionID).ToList();
            var eventId = ticketReservations.FirstOrDefault().With(t => t.EventTicket.With(r => r.EventId));
            if (eventId == null)
            {
                return Url.NotFound().ToRedirectResult();
            }
            var eventDetails = _eventManager.GetEventDetails(eventId.Value);
            var onlineAdModel = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var remainingTimeToCompleteBooking = _eventManager.GetRemainingTimeForReservationCollection(ticketReservations);

            // Construct the view model
            ApplicationUser applicationUser = _userManager.GetCurrentUser(User);
            UserNetworkModel userNetwork = null;

            if (_eventBookingContext.EventInvitationId.HasValue)
            {
                var invitation = _eventManager.GetEventInvitation(_eventBookingContext.EventInvitationId.Value);
                userNetwork = _userManager.GetUserNetwork(invitation.UserNetworkId);
            }

            var viewModel = new BookTicketsViewModel(onlineAdModel, eventDetails, _clientConfig, _appConfig,
                applicationUser, ticketReservations, userNetwork)
            {
                Reservations = this.MapList<EventTicketReservation, EventTicketReservedViewModel>(ticketReservations)
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

        [HttpPost, EnsurePaymentNotInProgress, Authorize]
        public ActionResult BookTickets(BookTicketsRequestViewModel bookTicketsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return JsonModelErrors(); ;
            }

            var applicationUser = _userManager.GetUserByEmailOrUsername(User.Identity.Name);

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
                        r.IsPublic = viewModel.IsPublic;
                    });
            }

            // Process the booking here!
            var eventBooking = _eventManager.CreateEventBooking(bookTicketsViewModel.EventId.GetValueOrDefault(), applicationUser, currentReservations, barcode => Url.ValidateBarcode(barcode).WithFullUrl());

            // Set the event id and booking id in the session for the consecutive calls
            _eventBookingContext.EventId = bookTicketsViewModel.EventId.GetValueOrDefault();
            _eventBookingContext.EventBookingId = eventBooking.EventBookingId;
            _eventBookingContext.Purchaser = bookTicketsViewModel.FullName;

            if (eventBooking.Status == EventBookingStatus.Active)
            {
                // No payment required so return a redirect to action json object
                return Json(new { NextUrl = Url.EventBooked().Build() });
            }

            return Json(new { NextUrl = Url.EventTicketingMakePayment().Build() });
        }

        [HttpGet, EventBookingRequired]
        public ActionResult CancelEventBooking()
        {
            _eventManager.CancelEventBooking(_eventBookingContext.EventBookingId);
            return View();
        }

        [HttpGet, EventBookingRequired, Authorize]
        public ActionResult EventBooked()
        {
            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.GetValueOrDefault());
            var sessionId = _httpContext.With(h => h.Session).SessionID;

            _eventManager.AdjustRemainingQuantityAndCancelReservations(sessionId, eventBooking.EventBookingTickets);
            _eventBookingManager.WithEventBooking(_eventBookingContext.EventBookingId);

            var viewModel = _eventBookingManager.CreateEventBookedViewModel();
            _eventBookingManager
                .SendTicketBuyerNotification()
                .SendTicketsToAllGuests()
                .SendOrganisersNotification();

            _eventBookingContext.EventBookingComplete = true;
            return View(viewModel);
        }

        [HttpPost, ActionName("assign-group"), EventBookingRequired(AllowCompleted = true)]
        public async Task<ActionResult> AssignGroupToTicket(int eventBookingTicketId, int? eventGroupId)
        {
            if (eventGroupId.HasValue)
            {
                var eventGroup = await _eventManager.GetEventGroup(eventGroupId.Value);
                if (eventGroup.IsFull())
                {
                    // Capacity reached
                    ModelState.AddModelError("eventGroupId", "Max capacity reached for selected group");
                    return Json(ModelState.ToErrors());
                }
            }
            _eventManager.AssignGroupToTicket(eventBookingTicketId, eventGroupId);
            return Json(true);
        }

        [HttpGet, EventBookingRequired, RequireHttps, Authorize]
        public ViewResult MakePayment(int? status)
        {
            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.GetValueOrDefault());
            var viewModel = new MakePaymentViewModel
            {
                TotalCost = eventBooking.TotalCost,
                TotalCostWithoutFees = eventBooking.Cost,
                TotalFees = eventBooking.TransactionFee,
                EventTickets = this.MapList<EventBookingTicket, EventBookingTicketViewModel>(eventBooking.EventBookingTickets.ToList()),
                StripePublishableKey = _appConfig.StripePublishableKey,
                EnablePayPalPayments = _clientConfig.EnablePayPalPayments,
                EnableCreditCardPayments = _clientConfig.EnableCreditCardPayments,
                PaymentFailedMessage = ResponseFriendlyMessage.Get(status)
            };

            return View(viewModel);
        }

        [HttpPost, EventBookingRequired, RequireHttps]
        public ActionResult PayWithPayPal()
        {
            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.GetValueOrDefault());

            // Process paypal payment
            var payPalRequest = new EventBookingPayPalRequestFactory().CreatePaymentRequest(eventBooking,
                eventBooking.EventBookingId.ToString(),
                Url.EventPaymentAuthorisePayPal().WithFullUrl().Build(),
                Url.EventBookTickets().WithRouteValues(new { paymentCancelled = true }).WithFullUrl().Build());

            var response = _payPalService.SubmitPayment(payPalRequest);

            _eventManager.SetPaymentReferenceForBooking(eventBooking.EventBookingId, response.PaymentId, PaymentType.PayPal); // paypal just for now
            _eventBookingContext.EventBookingPaymentReference = response.PaymentId;

            return Json(new { NextUrl = response.ApprovalUrl });
        }

        [HttpGet, EventBookingRequired, RequireHttps]
        public ActionResult AuthorisePayPal(string payerId)
        {
            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.GetValueOrDefault());

            // Call paypal to let them know we completed our end

            var isComplete = _payPalService.CompletePayment(_eventBookingContext.EventBookingPaymentReference, payerId,
                eventBooking.UserId, eventBooking.TotalCost, eventBooking.EventBookingId.ToString(), TransactionTypeName.EventBookingTickets);

            if (!isComplete)
            {
                return Url.EventTicketingMakePayment()
                   .ToRedirectResult(new Dictionary<string, object>
                   {
                        {"status", (int)Business.Payment.ResponseType.PayPalServerError }
                   });
            }

            // Mark booking as paid in our database
            _eventManager.ActivateBooking(_eventBookingContext.EventBookingId, _eventBookingContext.EventInvitationId);

            return Url.EventBooked().ToRedirectResult();
        }

        [HttpPost, EventBookingRequired, RequireHttps, Authorize]
        public ActionResult PayWithCreditCard(StripePaymentViewModel stripePayment)
        {
            var eventBooking = _eventManager.GetEventBooking(_eventBookingContext.EventBookingId.GetValueOrDefault());
            var currentUser = _userManager.GetCurrentUser(this.User);

            var response = _creditCardService.CompletePayment(new StripeChargeRequest
            {
                AmountInCents = eventBooking.TotalCostInCents(),
                Description = TransactionTypeName.EventBookingTickets,
                StripeEmail = stripePayment.StripeEmail,
                StripeToken = stripePayment.StripeToken,
                UserId = currentUser.Username
            });

            if (response.ResponseType != ResponseType.Success)
            {
                return Url.EventTicketingMakePayment()
                    .ToRedirectResult(new Dictionary<string, object>
                    {
                        {"status", (int)response.ResponseType }
                    });
            }

            // Mark booking as paid in our database
            _eventManager.SetPaymentReferenceForBooking(eventBooking.EventBookingId, stripePayment.StripeToken, PaymentType.CreditCard);
            _eventManager.ActivateBooking(_eventBookingContext.EventBookingId, _eventBookingContext.EventInvitationId);

            return Url.EventBooked().ToRedirectResult();
        }

        [HttpGet]
        public ActionResult TicketingPrices()
        {
            return RedirectToActionPermanent("event-pricing", "Help");
        }

#if DEBUG

        /*
        *   The following endpoints are great for debugging purposes only. 
        *   The cshtml files are used for templates only for producing PDFs
        */ 

        public ActionResult Tickets(int id)
        {
            var builder = _eventBookingManager.WithEventBooking(id);

            var viewModels = builder.CreateEventTicketPrintViewModelsForBooking();
            return View("~/Views/Templates/Tickets.cshtml", viewModels.ToList());

        }

        public ActionResult Invoice(int id)
        {
            var eventBooking = _eventManager.GetEventBooking(id);
            var applicationUser = _userManager.GetUserByEmailOrUsername(eventBooking.Email);
            var ad = _searchService.GetByAdOnlineId(eventBooking.Event.OnlineAdId);
            var viewModel = new EventBookingInvoiceViewModel(_clientConfig, eventBooking, applicationUser, ad.Heading);
            return View(viewModel);
        }

#endif

        public ActionResult ValidateBarcode(string barcode)
        {
            var result = _eventBarcodeValidator.ValidateTicket(barcode);
            var viewModel = BarcodeValidationViewModel.FromResult(result);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Invite(long token)
        {
            var invitation = _eventManager.GetEventInvitation(token);
            if (invitation == null)
                return Url.NotFound().ToRedirectResult();

            var eventDetails = _eventManager.GetEventDetails(invitation.EventId);
            var adSearchResult = _searchService.GetByAdOnlineId(eventDetails.OnlineAdId);
            var userNetwork = _userManager.GetUserNetwork(invitation.UserNetworkId);

            var viewModel = new InvitationViewModel(adSearchResult, eventDetails, userNetwork, _clientConfig, invitation);
            return View(viewModel);
        }

        [HttpGet]
        [ActionName("ticket-fields")]
        public ActionResult GetEventTicketFields(int id)
        {
            var fields = this.MapList<EventTicketField, EventTicketFieldViewModel>(
                _eventManager.GetEventTicket(id).With(et => et.EventTicketFields.ToList()));

            return Json(fields);
        }

        [HttpGet]
        [ActionName("session-expired")]
        public ActionResult SessionExpired()
        {
            return View(_eventBookingContext.EventUrl);
        }

        [HttpGet, ActionName("accept-organiser-invite")]
        [Authorize]
        public ActionResult AcceptInvite(AcceptOrganiserInviteRequestVm request)
        {
            if (!ModelState.IsValid)
            {
                _logService.Warn("Invitation view model state invalid. Details: \n" + request.ToJsonString());
                return View(new AcceptOrganiserInviteViewModel { BadRequest = true });
            }

            var eventDetails = _searchService.GetEvent(request.EventId.GetValueOrDefault());
            if (eventDetails == null)
            {
                return Url.NotFound().ToRedirectResult();
            }

            var result = _eventOrganiserService.ConfirmOrganiserInvite(request.EventId.GetValueOrDefault(), request.Token, request.Recipient);

            var vm = new AcceptOrganiserInviteViewModel
            {
                IsSuccessful = result == OrganiserConfirmationResult.Success,
                AlreadyActivated = result == OrganiserConfirmationResult.AlreadyActivated,
                WrongEmail = result == OrganiserConfirmationResult.MismatchedEmail,
                EventName = eventDetails.AdSearchResult.Heading,
                EventUrl = Url.EventDashboard(eventDetails.AdSearchResult.AdId),
                BrandName = _clientConfig.ClientName
            };

            return View(vm);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<Business.Events.EventTicket, EventTicketViewModel>().ReverseMap();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketRequestViewModel>();
            configuration.CreateMap<Business.Events.EventBookingTicket, EventBookingTicketViewModel>();
            configuration.CreateMap<Business.Events.EventTicketReservation, EventTicketReservedViewModel>()
                .ForMember(m => m.Status, options => options.MapFrom(s => s.StatusAsString.Humanize()))
                .ForMember(m => m.TicketName, options => options.MapFrom(s => s.EventTicket.TicketName))
                ;

            configuration.CreateMap<EventGuestDetails, EventGuestListViewModel>();
            configuration.CreateMap<EventTicketField, EventTicketFieldViewModel>();

            // From View Model
            configuration.CreateMap<EventTicketRequestViewModel, EventTicket>();
            configuration.CreateMap<BookTicketsRequestViewModel, RegistrationModel>();
            configuration.CreateMap<EventTicketReservedViewModel, Business.Events.EventTicketReservation>();
        }

        private readonly HttpContextBase _httpContext;
        private readonly IEventBookingContext _eventBookingContext;
        private readonly ISearchService _searchService;
        private readonly IEventManager _eventManager;
        private readonly IClientConfig _clientConfig;
        private readonly IUserManager _userManager;
        private readonly IPayPalService _payPalService;
        private readonly IBookingManager _bookingManager;
        private readonly IEventTicketReservationFactory _eventTicketReservationFactory;
        private readonly IEventBarcodeValidator _eventBarcodeValidator;
        private readonly IApplicationConfig _appConfig;
        private readonly ICreditCardService _creditCardService;
        private readonly IEventBookingManager _eventBookingManager;
        private readonly ITicketRequestValidator _ticketRequestValidator;
        private readonly ILogService _logService;
        private readonly IEventOrganiserService _eventOrganiserService;

        public EventController(ISearchService searchService, IEventManager eventManager, HttpContextBase httpContext, IClientConfig clientConfig, IUserManager userManager, IEventBookingContext eventBookingContext, IPayPalService payPalService, IBookingManager bookingManager, IEventTicketReservationFactory eventTicketReservationFactory, ITemplatingService templatingService, IEventBarcodeValidator eventBarcodeValidator, IApplicationConfig appConfig, ICreditCardService creditCardService, IEventBookingManager eventBookingManager, ITicketRequestValidator ticketRequestValidator, ILogService logService, IMailService mailService, IEventOrganiserService eventOrganiserService)
        {
            _searchService = searchService;
            _eventManager = eventManager;
            _httpContext = httpContext;
            _clientConfig = clientConfig;
            _userManager = userManager;
            _eventBookingContext = eventBookingContext;
            _payPalService = payPalService;
            _bookingManager = bookingManager;
            _eventTicketReservationFactory = eventTicketReservationFactory;
            _eventBarcodeValidator = eventBarcodeValidator;
            _appConfig = appConfig;
            _creditCardService = creditCardService;
            _ticketRequestValidator = ticketRequestValidator;
            _logService = logService;
            _eventOrganiserService = eventOrganiserService;
            _eventBookingManager = eventBookingManager
                .WithTemplateService(templatingService.Init(this))
                .WithMailService(mailService.Initialise(this));
            

        }
    }
}
