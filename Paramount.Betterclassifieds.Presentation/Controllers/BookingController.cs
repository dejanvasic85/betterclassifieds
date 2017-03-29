using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class BookingController : ApplicationController, IMappingBehaviour
    {
        [HttpPost, AuthorizeBookingIdentity]
        public ActionResult StartFromTemplate(int id)
        {
            var existingBooking = _bookingManager.GetBooking(id);

            var cart = _bookingContext.Create(existingBooking);

            return Json(new
            {
                IsCreated = true,
                StartUrl = Url.Action("Step1")
            });
        }

        //
        // GET: /Booking/Step/1 - Category and publications
        [HttpGet, BookingStep(1)]
        public ActionResult Step1()
        {
            // First time we are creating a new booking cart OR an existing one is simply returned...
            var bookingCart = _bookingContext.FetchOrCreate();

            var viewModel = new Step1View(
                _searchService.GetCategories(),
                this.MapList<PublicationModel, PublicationSelectionView>(_searchService.GetPublications()),
                bookingCart.CategoryId,
                bookingCart.SubCategoryId,
                bookingCart.Publications);

            return View(viewModel);
        }

        [HttpPost, BookingStep(1)]
        public ActionResult Step1(Step1View viewModel, IBookingCart bookingCart)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { errorMsg = "Please ensure you select a category before next step." });
            }

            bookingCart.CategoryId = viewModel.CategoryId;
            bookingCart.SubCategoryId = viewModel.SubCategoryId;

            if (viewModel.Publications != null)
                bookingCart.Publications = viewModel.Publications.Where(p => p.IsSelected).Select(p => p.PublicationId).ToArray();

            var category = _searchService.GetCategories().Single(c => c.MainCategoryId == bookingCart.SubCategoryId);
            bookingCart.CategoryAdType = category.CategoryAdType;

            _cartRepository.Save(bookingCart);

            var workflow = new BookingWorkflowController<CategorySelectionStep>(Url, bookingCart);
            var nextUrl = workflow.GetNextStepUrl(new { adType = category.CategoryAdType });
            return Json(new { nextUrl});
        }

        //
        // GET: /Booking/Step/2 - ad details
        [HttpGet, BookingStep(2)]
        public ActionResult Step2(string adType, IBookingCart bookingCart)
        {
            if (adType.HasValue())
            {
                return View("Step2_" + adType);
            }

            var stepTwoModel = this.Map<OnlineAdModel, Step2View>(bookingCart.OnlineAdModel);
            this.Map(bookingCart.LineAdModel, stepTwoModel);

            // Set max number of images available for upload ( available on global client configuration object )
            stepTwoModel.MaxOnlineImages = _clientConfig.MaxOnlineImages;
            stepTwoModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
            stepTwoModel.ConfigDurationDays = _clientConfig.RestrictedOnlineDaysCount;
            stepTwoModel.StartDate = bookingCart.GetStartDateOrMinimum();
            stepTwoModel.PrintInsertions = bookingCart.PrintInsertions.GetValueOrDefault();

            // Map the flag for line ad
            stepTwoModel.IsLineAdIncluded = bookingCart.IsLineAdIncluded;

            // Set convenient contact details for the user
            var applicationUser = _userManager.GetCurrentUser(this.User);
            if (applicationUser != null)
            {
                stepTwoModel.OnlineAdContactName = applicationUser.FirstName;
                stepTwoModel.OnlineAdContactPhone = applicationUser.Phone;
                stepTwoModel.OnlineAdContactEmail = applicationUser.Email;
            }

            if (bookingCart.IsLineAdIncluded)
            {
                // Fetch the editions for the selected publications
                stepTwoModel.UpcomingEditions = _editionManager
                    .GetUpcomingEditions(bookingCart.Publications)
                    .Select(m => new SelectListItem { Text = m.ToString(Constants.DATE_FORMAT), Value = m.ToString(Constants.DATE_FORMAT) });

                stepTwoModel.AvailableInsertions = _editionManager
                    .GetAvailableInsertions()
                    .Select(m => new SelectListItem { Text = m.ToString(), Value = m.ToString() });
            }

            return View(stepTwoModel);
        }

        [HttpPost, ValidateAntiForgeryToken, BookingStep(2)]
        public ActionResult Step2(Step2View viewModel, IBookingCart bookingCart)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MaxOnlineImages = _clientConfig.MaxOnlineImages;
                viewModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;
                viewModel.IsLineAdIncluded = bookingCart.IsLineAdIncluded;
                return View(viewModel);
            }

            // Map online Ad
            this.Map(viewModel, bookingCart.OnlineAdModel);
            bookingCart.OnlineAdModel.SetDescription(viewModel.OnlineAdDescription);

            // Map Line Ad
            this.Map(viewModel, bookingCart.LineAdModel);

            // Map Schedule
            bookingCart.SetSchedule(_clientConfig, viewModel.StartDate.GetValueOrDefault(), viewModel.FirstPrintDateFormatted, viewModel.PrintInsertions);

            // Save and continue
            _cartRepository.Save(bookingCart);

            var currentStep = new BookingWorkflowController<DesignOnlineAdStep>(Url, bookingCart);

            return currentStep.RedirectToNextStep();
        }

        // 
        // GET /Booking/Step/3 - Confirmation
        [HttpGet, BookingStep(3), Authorize]
        public ActionResult Step3(string cancel)
        {
            var bookingCart = _bookingContext.Current();
            bool isPaymentCancelled;
            bool.TryParse(cancel, out isPaymentCancelled);

            bookingCart.TotalPrice = _rateCalculator.Calculate(bookingCart).Total;
            _cartRepository.Save(bookingCart);

            var viewModel = this.Map<BookingCart, Step3View>(bookingCart);
            viewModel.IsPaymentCancelled = isPaymentCancelled;
            var confirmationStep = new BookingWorkflowController<ConfirmationStep>(Url, bookingCart);
            viewModel.PreviousStepUrl = confirmationStep.GetPreviousUrl();

            return View(viewModel);
        }

        [HttpPost, BookingStep(3), Authorize]
        public ActionResult Step3(Step3View viewModel)
        {
            var bookingCart = _bookingContext.Current();

            if (!ModelState.IsValid)
            {
                // Return the mapped object from the booking cart
                this.Map(bookingCart, viewModel);
                this.Map(bookingCart.OnlineAdModel, viewModel);
                return View(viewModel);
            }
            // Complete the booking cart (needs to move on now)
            bookingCart.UserId = _userManager.GetCurrentUser(this.User).Username;

            if (bookingCart.NoPaymentRequired())
            {
                _cartRepository.Save(bookingCart);
                return RedirectToAction("Success");
            }

            var bookingOrder = _rateCalculator.Calculate(bookingCart);
            var request = new AdBookingPayPalRequestFactory().CreatePaymentRequest(bookingOrder,
                bookingCart.BookingReference,
                returnUrl: Url.ActionAbsolute("AuthorisePayment", "Booking").Build(),
                cancelUrl: Url.ActionAbsolute("Step3", "Booking").Build().Append("?cancel=true"));
            var response = _payPalService.SubmitPayment(request);

            bookingCart.PaymentReference = response.PaymentId;
            _cartRepository.Save(bookingCart);
            return Redirect(response.ApprovalUrl);
        }

        [BookingRequired]
        public ActionResult AuthorisePayment(string payerId)
        {
            var bookingCart = _bookingContext.Current();
            _payPalService.CompletePayment(bookingCart.PaymentReference, payerId, bookingCart.UserId, bookingCart.TotalPrice, bookingCart.BookingReference, "Classified Booking");
            return RedirectToAction("Success");
        }

        // 
        // GET /Booking/Success
        [HttpGet, BookingRequired, Authorize, AuthorizeCartIdentity]
        public ActionResult Success()
        {   
            var bookingCart = _bookingContext.Current();
            if (!bookingCart.StartDate.HasValue)
            {
                bookingCart.SetSchedule(_clientConfig, DateTime.Today);
            }

            var bookingOrder = _rateCalculator.Calculate(bookingCart);
            var currentUser = _userManager.GetCurrentUser();
            bookingOrder.SetRecipientDetails(currentUser);
           
            var id = _bookingManager.CreateBooking(bookingCart, bookingOrder);

            // Complete the booking
            _cartRepository.Save(bookingCart);

            // Send the email(s) about the booking
            _mailService.SendListingCompleteEmail(currentUser, id.GetValueOrDefault(), bookingCart);
            
            // Build the view model
            var successView = new SuccessView
            {
                AdId = id.GetValueOrDefault(),
                TitleSlug = Slug.Create(true, bookingCart.OnlineAdModel.Heading),
                IsBookingActive = bookingCart.StartDate <= DateTime.Today,
                CategoryAdType = bookingCart.CategoryAdType,
                UserNetworkNotifierView = new UserNetworkNotifierView(id.GetValueOrDefault(), _userManager.GetUserNetworksForUserId(currentUser.Username)),
                AdUrl = Url.AdUrl(Slug.Create(true, bookingCart.OnlineAdModel.Heading), id.GetValueOrDefault(), bookingCart.CategoryAdType).WithFullUrl()
            };

            _bookingContext.Clear();

            return View(successView);
        }

        [HttpPost, BookingRequired]
        public ActionResult RemoveOnlineImage(Guid documentId)
        {
            // Remove the image from the document repository
            _documentRepository.DeleteDocument(documentId);

            var bookingCart = _bookingContext.Current();
            bookingCart.OnlineAdModel.RemoveImage(documentId.ToString());
            _cartRepository.Save(bookingCart);

            return Json(true);
        }

        [HttpPost, BookingRequired]
        public ActionResult SetLineAdImage(Guid documentId)
        {
            var bookingCart = _bookingContext.Current();
            bookingCart.LineAdModel.AdImageId = documentId.ToString();
            _cartRepository.Save(bookingCart);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveLineAdImage(Guid documentId)
        {
            var bookingCart = _bookingContext.Current();
            bookingCart.LineAdModel.AdImageId = null;
            _cartRepository.Save(bookingCart);

            // Remove it from the document repository
            _documentRepository.DeleteDocument(documentId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult AssignOnlineImage(string documentId, bool removeExisting = false)
        {
            var bookingCart = _bookingContext.Current();

            if (removeExisting)
            {
                bookingCart.OnlineAdModel.Images.Clear();
            }

            // Persist to the booking cart also
            bookingCart.OnlineAdModel.AddImage(documentId);
            _cartRepository.Save(bookingCart);

            return Json(true);
        }

        [HttpPost, BookingRequired]
        public ActionResult GetRate(PricingFactorsView pricingFactors)
        {
            // Map incoming
            // Updates the booking cart and returns the updated price breakdown
            var bookingCart = _bookingContext.Current();
            bookingCart.UpdateByPricingFactors(this.Map<PricingFactorsView, PricingFactors>(pricingFactors));

            // Process
            var bookingRateResult = _rateCalculator.Calculate(bookingCart, pricingFactors.Editions);

            // Return view model
            var viewModel = this.Map<BookingOrderResult, PriceSummaryView>(bookingRateResult);

            return Json(viewModel);
        }

        /// <summary>
        /// Used for displaying list of editions for each publication based on user selection of start date and insertions
        /// </summary>
        [HttpPost, BookingRequired]
        public ActionResult PreviewEditions(DateTime firstEdition, int printInsertions)
        {
            var list = new List<PublicationNameAndEditionListView>();
            foreach (var publicationId in _bookingContext.Current().Publications)
            {
                string publicationName;
                var dates = _editionManager
                    .GetUpcomingEditionsForPublication(publicationId, firstEdition, out publicationName)
                    .Select(e => e.Date.ToString(Constants.DATE_FORMAT))
                    .Take(printInsertions)
                    .ToArray();

                list.Add(new PublicationNameAndEditionListView
                {
                    Publication = publicationName,
                    Editions = dates
                });
            }

            return Json(list.ToArray());
        }

        [HttpGet, BookingRequired]
        public ActionResult GetEventDetails(IBookingCart bookingCart)
        {
            var eventViewModel = this.Map<IBookingCart, EventViewModel>(bookingCart);
            return Json(eventViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateEventDetails(EventViewModel eventViewModel, IBookingCart bookingCart)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();

            this.Map(eventViewModel, bookingCart);

#if !DEBUG
            // Do the timezone stuff before saving
            // Fetches the Timezone information selected for the map / address from google map
            if (eventViewModel.LocationLatitude.HasValue && eventViewModel.LocationLongitude.HasValue)
            {
                
                var result = _locationService.GetTimezone(eventViewModel.LocationLatitude.Value, eventViewModel.LocationLongitude.Value);
                if (result.IsOk())
                {
                    bookingCart.Event.TimeZoneId = result.TimeZoneId;
                    bookingCart.Event.TimeZoneName = result.TimeZoneName;
                    bookingCart.Event.TimeZoneDaylightSavingsOffsetSeconds = result.DstOffset;
                    bookingCart.Event.TimeZoneUtcOffsetSeconds = result.RawOffset;

                    // Work out what the UTC date is for these dates which is based on the events location!
                    var totalOffset = result.RawOffset + result.DstOffset;

                    if (bookingCart.Event.EventStartDate != null)
                        bookingCart.Event.EventStartDateUtc = bookingCart.Event.EventStartDate.Value.AddSeconds(-totalOffset);

                    if (bookingCart.Event.EventEndDate != null)
                        bookingCart.Event.EventEndDateUtc = bookingCart.Event.EventEndDate.Value.AddSeconds(-totalOffset);
                }
            }
#endif
            _cartRepository.Save(bookingCart);

            var designTicketingStep = new BookingWorkflowController<DesignEventStep>(Url, bookingCart);

            if (!eventViewModel.TicketingEnabled)
                designTicketingStep.SkipNextStep();

            return Json(new { NextUrl = designTicketingStep.GetNextStepUrl() });
        }

        [HttpGet, BookingRequired, BookingCategoryTypeRequired("Event")]
        public ActionResult EventTickets(IBookingCart bookingCart)
        {
            if (bookingCart.Event == null)
            {
                return Redirect(Url.Booking(2, bookingCart.CategoryAdType));
            }

            var viewModel = new BookingEventTicketSetupViewModel
            {
                IncludeTransactionFee = bookingCart.With(b => b.Event).IncludeTransactionFee,
                Tickets = this.MapList<EventTicket, BookingEventTicketViewModel>(bookingCart.Event.Tickets.ToList()),
                EventTicketFee = _clientConfig.EventTicketFeePercentage,
                EventTicketFeeCents = _clientConfig.EventTicketFeeCents
            };
            return View("Step2_EventTickets", viewModel);
        }

        [HttpPost, BookingRequired, BookingCategoryTypeRequired("Event")]
        public ActionResult EventTickets(IBookingCart bookingCart, BookingEventTicketSetupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return JsonModelErrors();

            bookingCart.Event.Tickets = this.MapList<BookingEventTicketViewModel, EventTicket>(viewModel.Tickets);
            bookingCart.Event.IncludeTransactionFee = viewModel.IncludeTransactionFee;

            _cartRepository.Save(bookingCart);
            var nextUrl = Url.Action("Step3");

            return Json(new { NextUrl = nextUrl });
        }

        #region Mappings
        public void OnRegisterMaps(IConfiguration configuration)
        {
            // Step 2 view is very flat with OnlineAd Prefix on properties
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<OnlineAdModel, Step2View>().ForMember(member => member.OnlineAdDescription, options => options.MapFrom(src => src.HtmlText));
            configuration.CreateMap<OnlineAdModel, Step3View>();
            configuration.CreateMap<LineAdModel, Step2View>();
            configuration.CreateMap<BookingOrderResult, PriceSummaryView>().ConvertUsing<PriceSummaryViewConverter>();
            configuration.CreateMap<BookingCart, Step3View>().ForMember(m => m.PublicationCount, options => options.MapFrom(src => src.Publications.Length));
            configuration.CreateMap<IBookingCart, EventViewModel>().ConvertUsing(new BookingCartToEventViewConverter(_dateService));
            configuration.CreateMap<EventTicket, BookingEventTicketViewModel>().ReverseMap()
                .ForMember(m => m.RemainingQuantity, options => options.MapFrom(src => src.AvailableQuantity))
                .ForMember(m => m.IsActive, options => options.UseValue(true))
                ;
            configuration.CreateMap<EventTicketField, EventTicketFieldViewModel>()
                .ReverseMap();

            // From ViewModel
            configuration.CreateMap<Step2View, OnlineAdModel>()
                .ForMember(member => member.Images, options => options.Ignore())
                .ForMember(member => member.HtmlText, options => options.MapFrom(src => src.OnlineAdDescription));
            configuration.CreateMap<Step2View, LineAdModel>()
                .ForMember(member => member.WordsPurchased, options => options.MapFrom(src => src.LineAdText.WordCount()))
                .ForMember(member => member.UsePhoto, options => options.MapFrom(src => src.LineAdImageId.HasValue()))
                .ForMember(member => member.UseBoldHeader, options => options.MapFrom(src => src.LineAdHeader.HasValue()));
            configuration.CreateMap<UserNetworkEmailView, UserNetworkModel>();
            configuration.CreateMap<PricingFactorsView, PricingFactors>();
            configuration.CreateMap<EventViewModel, IBookingCart>().ConvertUsing(new EventViewToBookingCartConverter(_dateService, _clientConfig));
        }

        #endregion


        private readonly ISearchService _searchService;
        private readonly IBookCartRepository _cartRepository;
        private readonly IBookingContext _bookingContext;
        private readonly IClientConfig _clientConfig;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserManager _userManager;
        private readonly IRateCalculator _rateCalculator;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IBookingManager _bookingManager;
        private readonly IPayPalService _payPalService;
        private readonly IEditionManager _editionManager;
        private readonly IDateService _dateService;
        private readonly ILocationService _locationService;
        private readonly ILogService _logService;
        private readonly IMailService _mailService;

        public BookingController(
            ISearchService searchService,
            IClientConfig clientConfig,
            IDocumentRepository documentRepository,
            IBookCartRepository cartRepository,
            IUserManager userManager,
            IRateCalculator rateCalculator,
            IApplicationConfig applicationConfig,
            IBookingContext bookingContext,
            IBookingManager bookingManager,
            IPayPalService payPalService,
            IEditionManager editionManager,
            IDateService dateService,
            ILocationService locationService,
            ILogService logService,
            IMailService mailService)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
            _rateCalculator = rateCalculator;
            _applicationConfig = applicationConfig;
            _bookingContext = bookingContext;
            _bookingManager = bookingManager;
            _payPalService = payPalService;
            _editionManager = editionManager;
            _dateService = dateService;
            _locationService = locationService;
            _logService = logService;
            _mailService = mailService.Initialise(this);
        }
    }
}
