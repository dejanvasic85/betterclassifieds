﻿using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Presentation.Services;
using System;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    [AuthorizeBookingIdentity]
    public class EditAdController : ApplicationController, IMappingBehaviour
    {
        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Updated = false;
            ViewBag.Invalid = false;


            // Fetch the ad booking
            var adBooking = _bookingManager.GetBooking(id);
            var onlineAd = adBooking.OnlineAd;

            if (adBooking.CategoryAdType.HasValue())
            {
                // Use convention E.g. EventDetails
                return RedirectToAction(adBooking.CategoryAdType + "Details", "EditAd", new { id });
            }

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
            if (adBooking.CategoryAdType.HasValue())
            {
                return RedirectToAction("NotFound", "Error");
            }

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
        public ActionResult AssignOnlineImage(int id, Guid documentId, bool removeExisting)
        {
            _bookingManager.AddOnlineImage(id, documentId, removeExisting);
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
            var eventDetails = _eventManager.GetEventDetailsForOnlineAdId(adDetails.OnlineAdId, includeBookings: true);
            var guestList = _eventManager.BuildGuestList(eventDetails.EventId);
            var eventTicketTypes = eventDetails.Tickets;
            var paymentSummary = _eventManager.BuildPaymentSummary(eventDetails.EventId);
            var status = _eventManager.GetEventPaymentRequestStatus(eventDetails.EventId);
            var applicationUser = _userManager.GetCurrentUser();

            var eventEditViewModel = new EventDashboardViewModel(id, adDetails.NumOfViews, adDetails.Heading, eventDetails, paymentSummary, status,
                this.MapList<EventTicket, EventTicketViewModel>(eventTicketTypes.ToList()),
                this.MapList<EventGuestDetails, EventGuestListViewModel>(guestList.ToList()));

            eventEditViewModel.RequiresEventOrganiserConfirmation = applicationUser.RequiresEventOrganiserConfirmation;

            return View(eventEditViewModel);
        }

        [HttpGet, ActionName("event-ticketing")]
        [Route("event-dashboard/{id}/event/{eventId}/ticketing")]
        public ActionResult ManageEventTicketing(int id, int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            if (eventDetails == null)
            {
                return Url.NotFound().ToRedirectResult();
            }

            var guestList = _eventManager.BuildGuestList(eventId);
            var vm = new ManageTicketsViewModel
            {
                Id = id,
                EventId = eventId,
                TicketSettings = new TicketSettingsViewModel(eventDetails),
                Tickets = this.MapList<EventTicket, EventTicketViewModel>(eventDetails.Tickets.ToList()),
            };

            var groupedGuests = guestList.GroupBy(g => g.TicketId);
            foreach (var ticketGroup in groupedGuests)
            {
                vm.Tickets.Single(t => t.EventTicketId == ticketGroup.Key).SoldQty = ticketGroup.Count();
            }

            return View(vm);
        }

        [HttpGet]
        [ActionName("edit-ticket")]
        [Route("event-dashboard/{id}/event-ticket/{ticketId}")]
        public ActionResult EditTicket(int id, int ticketId)
        {
            // Ensure tickets have not been purchased
            var ticket = _eventManager.GetEventTicket(ticketId);
            if (ticket == null)
                return Url.NotFound().ToRedirectResult();

            ViewBag.Id = id;
            ViewBag.BackToTicketsUrl = Url.EventTicketManagement(id, ticket.EventId.GetValueOrDefault());
            var viewModel = this.Map<EventTicket, EventTicketViewModel>(ticket);
            var guests = _eventManager.BuildGuestList(ticket.EventId).Where(t => t.TicketId == ticketId);
            viewModel.SoldQty = guests.Count();

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("edit-ticket")]
        [Route("event-dashboard/{id}/event-ticket/{ticketId}")]
        public ActionResult EditTicket(int id, int ticketId, UpdateEventTicketViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();;

            var eventTicket = viewModel.EventTicket;
            if (eventTicket.SoldQty == 0)
            {
                // Double check if anyone in the meantime purchased a ticket and the organiser wants to change something
                var guestCount = _eventManager.BuildGuestList(viewModel.EventTicket.EventId.GetValueOrDefault()).Count(t => t.TicketId == ticketId);
                if (guestCount > 0)
                {
                    ModelState.AddModelError("GuestCountIncreased", "Looks like someone purchased this ticket in the meantime.");
                    return JsonModelErrors();;
                }
            }

            _eventManager.UpdateEventTicket(ticketId, eventTicket.TicketName,
                eventTicket.Price, eventTicket.RemainingQuantity,
                this.MapList<EventTicketFieldViewModel, EventTicketField>(viewModel.EventTicket.EventTicketFields.ToList()));

            return Json(viewModel.EventTicket);
        }
            
        [HttpPost]
        [Route("event-dashboard/{id}/event/{eventId}/edit-ticket-settings")]
        public ActionResult EditTicketSettings(int id, int eventId, TicketSettingsViewModel ticketSettingsViewModel)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();;

            _eventManager.UpdateEventTicketSettings(eventId, 
                ticketSettingsViewModel.IncludeTransactionFee,
                ticketSettingsViewModel.ClosingDate,
                ticketSettingsViewModel.OpeningDate);

            return Json(true);
        }

        [HttpPost]
        public ActionResult AddTicket(int id, NewEventTicketViewModel vm)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();;

            var ticket = _eventManager.CreateEventTicket(vm.EventId.GetValueOrDefault(),
                vm.TicketName,
                vm.Price.GetValueOrDefault(),
                vm.AvailableQuantity,
                vm.EventTicketFields?.Select(f => new EventTicketField
                {
                    FieldName = f.FieldName,
                    IsRequired = f.IsRequired
                }));

            return Json(ticket);
        }

        [HttpGet]
        public ActionResult DownloadGuestListPdf(int id, int eventId)
        {
            var viewModel = GetEventGuestList(id, eventId);
            var html = _templatingService.Generate(viewModel, "~/Views/Templates/EventGuestList.cshtml");
            var pdf = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(html);
            return File(pdf, ContentType.Pdf, $"{viewModel.EventName} - Guest List.pdf");
        }

        [HttpGet]
        public ActionResult DownloadGuestListCsv(int id, int eventId)
        {
            var viewModel = GetEventGuestList(id, eventId);
            var availableTickets = _eventManager.GetEventDetails(eventId).Tickets;
            var csvGenerator = new Business.Csv.CsvGenerator<EventGuestListViewModel>(viewModel.Guests, new EventGuestListCsvLineProvider(availableTickets));
            var csvData = csvGenerator.GetBytes();
            return File(csvData, ContentType.Csv, $"{viewModel.EventName} - Guest List.csv");
        }

        [HttpGet]
        public ActionResult DownloadGuestListExcel(int id, int eventId)
        {
            var viewModel = GetEventGuestList(id, eventId);
            var availableTickets = _eventManager.GetEventDetails(eventId).Tickets;
            var excelBytes = new Services.ExcelGuestGeneratorService(availableTickets, viewModel.Guests).GetBytes();
            return File(excelBytes, ContentType.Excel, $"{viewModel.EventName} - Guest list.xlsx");
        }

        private EventGuestListDownloadViewModel GetEventGuestList(int id, int eventId)
        {
            var adDetails = _searchService.GetByAdId(id);
            var eventGuestDetails = _eventManager.BuildGuestList(eventId).ToList();
            var guests = this.MapList<EventGuestDetails, EventGuestListViewModel>(eventGuestDetails);
            var viewModel = new EventGuestListDownloadViewModel { EventName = adDetails.Heading, Guests = guests };
            return viewModel;
        }

        [HttpGet]
        public ActionResult EventPaymentRequest(int id, int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            if (!eventDetails.IsClosed)
            {
                return Redirect(Url.EventDashboard(id));
            }

            var userProfile = _userManager.GetCurrentUser(this.User);
            var paymentSummary = _eventManager.BuildPaymentSummary(eventId);

            var viewModel = new EventPaymentSummaryViewModel
            {
                AdId = id,
                EventId = eventId,
                TotalTicketSalesAmount = paymentSummary.TotalTicketSalesAmount,
                OurFees = paymentSummary.EventOrganiserFeesTotalFeesAmount,
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

        [HttpPost] // Json
        public ActionResult EventPaymentRequest(int id, EventPaymentRequestViewModel eventPaymentRequestViewModel)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();

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

        [HttpPost] // Json
        public ActionResult CloseEvent(int id, int eventId)
        {
            _eventManager.CloseEvent(eventId);
            return Json(new { Closed = true });
        }

        [HttpGet]
        public ActionResult EventDetails(int id)
        {
            ViewBag.Id = id; // Will simply initialise the ad design service in Javascript with an AdId
            return View();
        }

        [HttpGet]
        public ActionResult GetEventDetails(int id)
        {
            var adDetails = _searchService.GetByAdId(id);
            var eventDetails = _eventManager.GetEventDetailsForOnlineAdId(adDetails.OnlineAdId);
            var adText = AdText.FromHtmlEncoded(adDetails.HtmlText);

            var viewModel = new EventViewModel
            {
                CanEdit = _eventManager.IsEventEditable(eventDetails.EventId),
                EventId = eventDetails.EventId.GetValueOrDefault(),
                Title = adDetails.Heading,
                Description = adText.HtmlText, // Serve back html for editing
                EventStartDate = eventDetails.EventStartDate.GetValueOrDefault(),
                EventEndDate = eventDetails.EventEndDate.GetValueOrDefault(),
                VenueName = eventDetails.VenueName,
                Location = eventDetails.Location,
                LocationLatitude = eventDetails.LocationLatitude,
                LocationLongitude = eventDetails.LocationLongitude,
                LocationFloorPlanDocumentId = eventDetails.LocationFloorPlanDocumentId,
                LocationFloorPlanFilename = eventDetails.LocationFloorPlanFilename,
                OrganiserName = adDetails.ContactName,
                OrganiserPhone = adDetails.ContactPhone,
                AdStartDate = _dateService.ConvertToString(adDetails.StartDate),
                EventPhoto = adDetails.PrimaryImage,
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateEventDetails(int id, EventViewModel viewModel)
        {
            var adText = AdText.FromHtml(viewModel.Description);

            _eventManager.UpdateEventDetails(id,
                viewModel.EventId,
                viewModel.Title,
                adText.Plaintext,
                adText.HtmlTextEncoded,
                viewModel.EventStartDate,
                viewModel.EventEndDate,
                viewModel.Location,
                viewModel.LocationLatitude,
                viewModel.LocationLongitude,
                viewModel.OrganiserName,
                viewModel.OrganiserPhone,
                _dateService.ConvertFromString(viewModel.AdStartDate),
                viewModel.LocationFloorPlanDocumentId,
                viewModel.LocationFloorPlanFilename,
                new Address
                {
                    Country = viewModel.Country,
                    Postcode = viewModel.Postcode,
                    State = viewModel.State,
                    StreetName = viewModel.StreetName,
                    StreetNumber = viewModel.StreetNumber,
                    Suburb = viewModel.Suburb
                });

            return Json(true);
        }

        [HttpGet, ActionName("add-guest")]
        public async Task<ActionResult> AddGuest(int id, int eventId)
        {
            var eventModel = _eventManager.GetEventDetails(eventId);
            var groups = await _eventManager
                .GetEventGroupsAsync(eventId, eventModel.With(e => e.Tickets.FirstOrDefault()).With(e => e.EventTicketId.GetValueOrDefault()));

            var viewModel = new AddEventGuestViewModel
            {
                Id = id,
                EventId = eventId,
                EventTickets = this.MapList<EventTicket, EventTicketViewModel>(eventModel.Tickets.Where(t => t.RemainingQuantity > 0).ToList()),
                TicketFields = eventModel
                    .With(e => e.Tickets.FirstOrDefault()) // the first one will be selected in the UI by default
                    .With(t => t.EventTicketFields.Select(tf => new EventTicketFieldViewModel { FieldName = tf.FieldName, IsRequired = tf.IsRequired }))
                    .With(etf => etf.ToList()),
                EventGroups = this.MapList<EventGroup, EventGroupViewModel>(groups.Where(gr => gr.IsAvailable()).ToList())
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("add-guest")]
        public ActionResult AddGuest(int id, AddEventGuestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(ModelState.ToErrors());

            var eventTicket = _eventManager.GetEventTicket(viewModel.With(vm => vm.SelectedTicket).With(t => t.EventTicketId.GetValueOrDefault()));
            var reservation = _ticketReservationFactory.CreateFreeReservation(_httpContext.Session?.SessionID, viewModel.SelectedGroup.With(g => g.EventGroupId), eventTicket);

            if (reservation.Status != EventTicketReservationStatus.Reserved)
            {
                ModelState.AddModelError("SelectedTicket", "The selected ticket could not be reserved");
                return Json(ModelState.ToErrors());
            }

            var currentUser = _userManager.GetCurrentUser();
            reservation.GuestFullName = viewModel.GuestFullName;
            reservation.GuestEmail = viewModel.GuestEmail;
            reservation.TicketFields = viewModel.With(vm => vm.TicketFields)
                .With(tf => new EventBookingTicketField { FieldName = tf.FieldName, FieldValue = tf.FieldValue })
                .With(l => l.ToList());

            var eventBooking = _eventManager.CreateEventBooking(
                viewModel.EventId.GetValueOrDefault(),
                currentUser,
                new[] { reservation },
                barcode => Url.ValidateBarcode(barcode).WithFullUrl());

            _eventManager.AdjustRemainingQuantityAndCancelReservations(_httpContext.Session?.SessionID, eventBooking.EventBookingTickets);

            var purchaserNotification = _eventNotificationBuilder.WithEventBooking(eventBooking.EventBookingId).CreateTicketPurchaserNotification();
            _broadcastManager.Queue(purchaserNotification, viewModel.GuestEmail);

            var guestNotification = _eventNotificationBuilder.CreateEventGuestNotifications().Single();
            _broadcastManager.Queue(guestNotification, guestNotification.GuestEmail);

            return Json(true);
        }

        [HttpGet, ActionName("edit-guest")]
        public ActionResult EditGuest(int id, int ticketNumber)
        {
            var eventBookingTicket = _eventManager.GetEventBookingTicket(ticketNumber);
            if (eventBookingTicket == null)
                return Url.NotFound().ToRedirectResult();

            var eventTicket = _eventManager.GetEventTicket(eventBookingTicket.EventTicketId);
            var eventBooking = _eventManager.GetEventBooking(eventBookingTicket.EventBookingId);

            var groups = Task.Run(() => _eventManager.GetEventGroupsAsync(eventTicket.EventId.GetValueOrDefault(), eventTicket.EventTicketId))
                .Result
                .Where(g => g.IsAvailable());

            var vm = new EditGuestViewModel(id, eventTicket, eventBooking, eventBookingTicket, groups);
            return View(vm);
        }

        // Json
        [HttpPost, ActionName("edit-guest")]
        public ActionResult EditGuest(int id, EditGuestViewModel vm)
        {
            if (vm.GroupId.HasValue && vm.CurrentGroupId != vm.GroupId)
            {
                var eventGroup = Task.Run(() => _eventManager.GetEventGroup(vm.GroupId.Value)).Result;
                if (!eventGroup.IsAvailable())
                    ModelState.AddModelError("groupId", "The selected group is no longer available");
            }

            if (!ModelState.IsValid)
                return JsonModelErrors();;

            // Fetch the existing fields and merge
            var fields = vm.Fields.Select(f => new EventBookingTicketField
            {
                FieldValue = f.FieldValue,
                FieldName = f.FieldName
            });

            var eventBookingTicket = _eventManager.UpdateEventBookingTicket(vm.EventBookingTicketId,
                 vm.GuestFullName,
                 vm.GuestEmail,
                 vm.GroupId,
                 fields,
                 barcode => Url.ValidateBarcode(barcode).WithFullUrl());

            if (vm.SendTransferEmail)
            {
                var builder = _eventNotificationBuilder.WithEventBooking(vm.EventBookingId);

                // Send the new guest an email
                builder.CreateEventGuestNotifications(vm.GuestEmail)
                    .ForEach(notification => { _broadcastManager.Queue(notification, notification.GuestEmail); });

                // Send the old guest a transfer email
                var transferNotification = builder.CreateEventTransferEmail(vm.TicketName, vm.GuestEmail, vm.GuestFullName);

                _broadcastManager.Queue(transferNotification, vm.OriginalGuestEmail);
            }

            return Json(new { eventBookingTicketId = eventBookingTicket.EventBookingTicketId });
        }

        // Json
        [HttpPost, ActionName("remove-guest")]
        public ActionResult RemoveGuest(int id, int eventBookingTicketId, bool sendEmailToGuestAboutRemoval)
        {
            var eventBookingTicket = _eventManager.CancelEventBookingTicket(eventBookingTicketId);

            if (sendEmailToGuestAboutRemoval)
            {
                var adDetails = _searchService.GetByAdId(id);
                var eventModel = _eventManager.GetEventDetailsForOnlineAdId(adDetails.OnlineAdId);

                _broadcastManager.Queue(new EventGuestNotificationFactory().CreateGuestRemovedNotification(
                    new UrlHelper(_httpContext.Request.RequestContext), adDetails, eventModel, eventBookingTicket),
                    to: eventBookingTicket.GuestEmail);
            }

            return Json(new
            {
                NextUrl = Url.RemoveGuestComplete(id).ToString()
            });
        }

        // Json
        [HttpPost, ActionName("resend-guest-email")]
        public ActionResult ResendGuestEmail(int id, int eventBookingTicketId)
        {
            var eventBookingTicket = _eventManager.GetEventBookingTicket(eventBookingTicketId);
            var notification = _eventNotificationBuilder
                .WithEventBooking(eventBookingTicket.EventBookingId)
                .CreateEventGuestResendNotifications()
                .Single(g => g.GuestEmail == eventBookingTicket.GuestEmail);

            _broadcastManager.Queue(notification, eventBookingTicket.GuestEmail);
            return Json(true);
        }

        [HttpGet, ActionName("remove-guest-complete")]
        public ActionResult RemoveGuestComplete(int id)
        {
            var vm = new RemoveGuestCompleteViewModel { AdId = id };
            return View(vm);
        }

        [HttpGet, ActionName("manage-groups")]
        public async Task<ActionResult> ManageGroups(int id, int eventId)
        {
            var eventDetails = _eventManager.GetEventDetails(eventId);
            var tickets = eventDetails.With(e => e.Tickets);
            var currentGroups = await _eventManager.GetEventGroupsAsync(eventId);

            var manageGroupsViewModel = new ManageGroupsViewModel
            {
                Id = id,
                EventId = eventId,
                EventGroups = this.MapList<EventGroup, EventGroupViewModel>(currentGroups.ToList()),
                Tickets = this.MapList<EventTicket, EventTicketViewModel>(tickets.ToList()),
                GroupsRequired = eventDetails.GroupsRequired.GetValueOrDefault()
            };

            foreach (var gr in manageGroupsViewModel.EventGroups)
            {
                var ticketsForGroup = await _eventManager.GetEventTicketsForGroup(gr.EventGroupId.GetValueOrDefault());
                gr.AvailableTickets = ticketsForGroup.Select(this.Map<EventTicket, EventTicketViewModel>).ToList();
            }

            return View(manageGroupsViewModel);
        }

        [HttpPost] // Json
        public ActionResult AddEventGroup(int id, CreateEventGroupViewModel @group)
        {
            _eventManager.AddEventGroup(@group.EventId,
                @group.GroupName,
                @group.MaxGuests,
                @group.AvailableTickets.Select(a => a.EventTicketId),
                _userManager.GetCurrentUser().Username,
                @group.IsDisabled);

            return Json(new { @group });
        }

        [HttpPost]
        public ActionResult ToggleEventGroupStatus(int id, int eventGroupId, bool isDisabled)
        {
            _eventManager.SetEventGroupStatus(eventGroupId, isDisabled);
            return Json(true);
        }

        [HttpPost]
        public ActionResult UpdateEventGroupSettings(int id, int eventId, bool groupsRequired)
        {
            _eventManager.UpdateEventGroupSettings(eventId, groupsRequired);
            return Json(true);
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
            configuration.CreateMap<EventGroup, EventGroupViewModel>();
            EventGroupViewModelFactory.GetMapping(configuration);
            configuration.CreateMap<EventTicket, EventTicketViewModel>();

            // From view model
            configuration.CreateMap<EditAdDetailsViewModel, OnlineAdModel>()
               .ForMember(member => member.Images, options => options.Ignore())
               .ForMember(member => member.HtmlText, options => options.MapFrom(src => src.OnlineAdDescription));

            configuration.CreateMap<EditAdDetailsViewModel, LineAdModel>()
                .ForMember(member => member.UsePhoto, options => options.MapFrom(src => src.LineAdImageId.HasValue()));

            configuration.CreateMap<EventTicket, EventTicketViewModel>().ReverseMap();
            configuration.CreateMap<EventTicketField, EventTicketFieldViewModel>().ReverseMap();
        }

        private readonly ISearchService _searchService;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;
        private readonly IBookingManager _bookingManager;
        private readonly IEventManager _eventManager;
        private readonly ITemplatingService _templatingService;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IDateService _dateService;
        private readonly IEventTicketReservationFactory _ticketReservationFactory;
        private readonly HttpContextBase _httpContext;
        private readonly IEventNotificationBuilder _eventNotificationBuilder;

        public EditAdController(ISearchService searchService, IApplicationConfig applicationConfig, IClientConfig clientConfig, IBookingManager bookingManager, IEventManager eventManager, ITemplatingService templatingService, IUserManager userManager, IBroadcastManager broadcastManager, IDateService dateService, IEventTicketReservationFactory ticketReservationFactory, HttpContextBase httpContext, IEventNotificationBuilder eventNotificationBuilder)
        {
            _searchService = searchService;
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
            _bookingManager = bookingManager;
            _eventManager = eventManager;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
            _dateService = dateService;
            _ticketReservationFactory = ticketReservationFactory;
            _httpContext = httpContext;
            _templatingService = templatingService.Init(this); // This service is tightly coupled to an mvc controller
            _eventNotificationBuilder = eventNotificationBuilder.WithTemplateService(_templatingService);
        }
    }
}
