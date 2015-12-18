using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using System;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Presentation.Services;


namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeBookingIdentity]
    public class EditAdController : Controller, IMappingBehaviour
    {
        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            ViewBag.Invalid = false;


            // Fetch the ad booking
            var adBooking = _bookingManager.GetBooking(id);
            var onlineAd = adBooking.OnlineAd;

            var viewModel = new EditAdDetailsViewModel(id, _clientConfig, onlineAd, adBooking, _applicationConfig);

            // Online ad mapping
            this.Map(adBooking.OnlineAd, viewModel);

            if (!adBooking.HasLineAd)
            {
                return View(viewModel);
            }

            // Line ad mapping
            viewModel.IsLineAdIncluded = true;
            viewModel.LineWordsPurchased = adBooking.LineAd.NumOfWords;
            this.Map(adBooking.LineAd, viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(EditAdDetailsViewModel viewModel)
        {
            var adBooking = _searchService.GetByAdId(viewModel.Id);
            viewModel.MaxOnlineImages = _clientConfig.MaxOnlineImages > adBooking.ImageUrls.Length ? _clientConfig.MaxOnlineImages : adBooking.ImageUrls.Length;
            viewModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
            viewModel.ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount;
            viewModel.OnlineAdImages = adBooking.ImageUrls.ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.Updated = false;
                ViewBag.Invalid = true;
                return View(viewModel);
            }

            // Convert to online ad
            var onlineAd = this.Map<EditAdDetailsViewModel, OnlineAdModel>(viewModel);
            onlineAd.SetDescription(viewModel.OnlineAdDescription);

            // Update the online ad
            _bookingManager.UpdateOnlineAd(viewModel.Id, onlineAd);

            if (viewModel.IsLineAdIncluded)
            {
                // Update the line ad
                var lineAd = this.Map<EditAdDetailsViewModel, LineAdModel>(viewModel);
                _bookingManager.UpdateLineAd(viewModel.Id, lineAd);
            }

            // Set the schedule
            if (viewModel.StartDate.HasValue && adBooking.StartDate != viewModel.StartDate)
            {
                _bookingManager.UpdateSchedule(viewModel.Id, viewModel.StartDate.GetValueOrDefault());
            }

            ViewBag.Updated = true;
            ViewBag.Invalid = false;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AssignOnlineImage(int id, Guid documentId)
        {
            _bookingManager.AddOnlineImage(id, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveOnlineImage(int id, Guid documentId)
        {
            _bookingManager.RemoveOnlineImage(id, documentId);
            return Json(true);
        }

        [HttpPost, BookingRequired]
        public ActionResult AssignLineAdImage(int id, Guid documentId)
        {
            _bookingManager.AssignLineAdImage(id, documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveLineAdImage(int id, Guid documentId)
        {
            _bookingManager.RemoveLineAdImage(id, documentId);
            return Json(true);
        }

        [HttpGet]
        public ActionResult EventDashboard(int id)
        {
            var adDetails = _searchService.GetByAdId(id);
            var eventDetails = _eventManager.GetEventDetailsForOnlineAdId(adDetails.OnlineAdId, true);
            var guestList = _eventManager.BuildGuestList(eventDetails.EventId);
            var eventTicketTypes = eventDetails.Tickets;
            var paymentSummary = _eventManager.BuildPaymentSummary(eventDetails.EventId);
            var status = _eventManager.GetEventPaymentRequestStatus(eventDetails.EventId);

            var eventEditViewModel = new EventDashboardViewModel(id, adDetails.NumOfViews, eventDetails, paymentSummary, status,
                this.MapList<EventTicket, EventTicketViewModel>(eventTicketTypes.ToList()),
                this.MapList<EventGuestDetails, EventGuestListViewModel>(guestList.ToList())
                );

            return View(eventEditViewModel);
        }

        [HttpPost]
        public ActionResult EventTicketUpdate(int id, EventTicketViewModel eventTicketViewModel)
        {
            if (eventTicketViewModel.EventTicketId.HasValue)
            {
                _eventManager.UpdateEventTicket(eventTicketViewModel.EventTicketId.GetValueOrDefault(),
                    eventTicketViewModel.TicketName,
                    eventTicketViewModel.Price,
                    eventTicketViewModel.RemainingQuantity);
            }
            else
            {
                _eventManager.CreateEventTicket(eventTicketViewModel.EventId.GetValueOrDefault(),
                    eventTicketViewModel.TicketName,
                    eventTicketViewModel.Price,
                    eventTicketViewModel.RemainingQuantity);
            }
            return Json(new { Updated = true });
        }

        [HttpGet]
        public ActionResult EventGuestListDownloadPdf(int id, int eventId)
        {
            var adDetails = _searchService.GetByAdId(id);
            var eventGuestDetails = _eventManager.BuildGuestList(eventId).ToList();
            var guests = this.MapList<EventGuestDetails, EventGuestListViewModel>(eventGuestDetails);
            var viewModel = new EventGuestListDownloadViewModel { EventName = adDetails.Heading, Guests = guests };
            var html = _templatingService.Generate(viewModel, "EventGuestList");
            var pdf = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(html);
            return File(pdf, ContentType.Pdf, "Guest List.pdf");
        }

        [HttpGet]
        public ActionResult EventPaymentRequest(int id, int eventId)
        {
            var userProfile = _userManager.GetCurrentUser(this.User);
            var paymentSummary = _eventManager.BuildPaymentSummary(eventId);

            var viewModel = new EventPaymentSummaryViewModel
            {
                AdId = id,
                EventId = eventId,
                TotalTicketSalesAmount = paymentSummary.TotalTicketSalesAmount,
                OurFeesPercentage = paymentSummary.SystemTicketFee,
                AmountOwed = paymentSummary.EventOrganiserOwedAmount,
                PreferredPaymentType = userProfile.PreferredPaymentMethod.ToString(),
                PayPalEmail = userProfile.PayPalEmail,
                DirectDebitDetails = new DirectDebitViewModel
                {
                    BankName = userProfile.BankName,
                    BSB = userProfile.BankBsbNumber,
                    AccountNumber = userProfile.BankAccountNumber,
                    AccountName = userProfile.BankAccountName
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EventPaymentRequest(int id, EventPaymentRequestViewModel eventPaymentRequestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { IsValid = false, Errors = ModelState.ToErrors() });
            }

            var mappedPaymentMethod = eventPaymentRequestViewModel.PaymentMethod.CastToEnum<PaymentType>();
            var currentUserId = this.User.Identity.Name;

            _eventManager.CreateEventPaymentRequest(eventPaymentRequestViewModel.EventId.GetValueOrDefault(),
                mappedPaymentMethod,
                eventPaymentRequestViewModel.RequestedAmount.GetValueOrDefault(),
                currentUserId);

            _broadcastManager.SendEmail(new Business.Broadcast.EventPaymentRequest
            {
                AdId = id,
                EventId = eventPaymentRequestViewModel.EventId.GetValueOrDefault(),
                PreferredPaymentMethod = eventPaymentRequestViewModel.PaymentMethod,
                RequestedAmount = eventPaymentRequestViewModel.RequestedAmount.GetValueOrDefault(),
                Username = currentUserId
            }, _clientConfig.SupportEmailList);

            return Json(new { NextUrl = Url.EventDashboard(id).ToString() });
        }

        [HttpPost]
        public ActionResult CloseEvent(int id, int eventId)
        {
            _eventManager.CloseEvent(eventId);
            return Json(new { Closed = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            // To view model
            configuration.CreateMap<OnlineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.OnlineAdImages, options => options.Ignore())
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.OnlineAdDescription, options => options.MapFrom(src => src.HtmlText));

            configuration.CreateMap<LineAdModel, EditAdDetailsViewModel>()
                .ForMember(m => m.Id, options => options.Ignore());

            configuration.CreateMap<EventGuestDetails, EventGuestListViewModel>();
            configuration.CreateMap<EventBookingTicketField, EventTicketFieldViewModel>();

            // From view model
            configuration.CreateMap<EditAdDetailsViewModel, OnlineAdModel>()
               .ForMember(member => member.Images, options => options.Ignore())
               .ForMember(member => member.HtmlText, options => options.MapFrom(src => src.OnlineAdDescription));

            configuration.CreateMap<EditAdDetailsViewModel, LineAdModel>()
                .ForMember(member => member.UsePhoto, options => options.MapFrom(src => src.LineAdImageId.HasValue()));

            configuration.CreateMap<EventTicket, EventTicketViewModel>();
        }

        private readonly ISearchService _searchService;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;
        private readonly IBookingManager _bookingManager;
        private readonly IEventManager _eventManager;
        private readonly ITemplatingService _templatingService;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;

        public EditAdController(ISearchService searchService, IApplicationConfig applicationConfig, IClientConfig clientConfig, IBookingManager bookingManager, IEventManager eventManager, ITemplatingService templatingService, IUserManager userManager, IBroadcastManager broadcastManager)
        {
            _searchService = searchService;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
            _bookingManager = bookingManager;
            _eventManager = eventManager;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
            _templatingService = templatingService.Init(this); // This service is tightly coupled to an mvc controller
        }
    }
}
