namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;

    using Business;
    using Business.Booking;
    using Business.Print;
    using Business.Broadcast;
    using Business.Search;
    using Business.DocumentStorage;
    using Business.Payment;
    using ViewModels;

    public class BookingController : Controller, IMappingBehaviour
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";

        private readonly ISearchService _searchService;
        private readonly IBookCartRepository _cartRepository;
        private readonly IBookingContext _bookingContext;
        private readonly IClientConfig _clientConfig;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserManager _userManager;
        private readonly IRateCalculator _rateCalculator;
        private readonly IBroadcastManager _broadcastManager;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IBookingManager _bookingManager;
        private readonly IPaymentService _paymentService;
        private readonly IEditionManager _editionManager;
        private readonly IDateService _dateService;

        public BookingController(
            ISearchService searchService,
            IClientConfig clientConfig,
            IDocumentRepository documentRepository,
            IBookCartRepository cartRepository,
            IUserManager userManager,
            IRateCalculator rateCalculator,
            IBroadcastManager broadcastManager,
            IApplicationConfig applicationConfig,
            IBookingContext bookingContext,
            IBookingManager bookingManager,
            IPaymentService paymentService,
            IEditionManager editionManager,
            IDateService dateService)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
            _rateCalculator = rateCalculator;
            _broadcastManager = broadcastManager;
            _applicationConfig = applicationConfig;
            _bookingContext = bookingContext;
            _bookingManager = bookingManager;
            _paymentService = paymentService;
            _editionManager = editionManager;
            _dateService = dateService;
        }

        [HttpPost, AuthorizeBookingIdentity]
        public ActionResult StartFromTemplate(int id)
        {
            var existingBooking = _bookingManager.GetBooking(id);

            var cart = _bookingContext.NewFromTemplate(existingBooking);

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
            var bookingCart = _bookingContext.Current();

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
            bookingCart.ViewName = category.ViewMap;

            bookingCart.CompleteStep(1);
            _cartRepository.Save(bookingCart);

            return Json(Url.Action("Step2", new { adType = category.ViewMap }));
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
                    .Select(m => new SelectListItem { Text = m.ToString(DATE_FORMAT), Value = m.ToString(DATE_FORMAT) });

                stepTwoModel.AvailableInsertions = _editionManager
                    .GetAvailableInsertions()
                    .Select(m => new SelectListItem { Text = m.ToString(), Value = m.ToString() });
            }

            return View(stepTwoModel);
        }

        [HttpPost, ValidateAntiForgeryToken, BookingStep(2)]
        public ActionResult Step2(Step2View viewModel)
        {
            var bookingCart = _bookingContext.Current();

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
            bookingCart.SetSchedule(_clientConfig, viewModel.StartDate.Value, viewModel.FirstPrintDateFormatted, viewModel.PrintInsertions);

            // Save and continue
            bookingCart.CompleteStep(2);
            _cartRepository.Save(bookingCart);

            return RedirectToAction("Step3");
        }

        // 
        // GET /Booking/Step/3 - Confirmation
        [HttpGet, BookingStep(3), Authorize]
        public ActionResult Step3(string cancel)
        {
            bool isPaymentCancelled;
            bool.TryParse(cancel, out isPaymentCancelled);

            var bookingCart = _bookingContext.Current();
            bookingCart.TotalPrice = _rateCalculator.Calculate(bookingCart).Total;
            _cartRepository.Save(bookingCart);

            var viewModel = this.Map<BookingCart, Step3View>(bookingCart);
            viewModel.IsPaymentCancelled = isPaymentCancelled;
            viewModel.PreviousStep = Url.Action("Step2", new { adtype = bookingCart.ViewName });

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
            bookingCart.CompleteStep(3);
            bookingCart.UserId = _userManager.GetCurrentUser(this.User).Username;

            if (bookingCart.NoPaymentRequired())
            {
                _cartRepository.Save(bookingCart);
                return RedirectToAction("Success");
            }

            // We only support paypal just for now
            var response = _paymentService.SubmitPayment(new PaymentRequest
            {
                PayReference = bookingCart.BookingReference,
                BookingOrderResult = _rateCalculator.Calculate(bookingCart),
                ReturnUrl = Url.ActionAbsolute("AuthorisePayment", "Booking"),
                CancelUrl = Url.ActionAbsolute("Step3", "Booking").Append("?cancel=true")
            });

            bookingCart.PaymentReference = response.PaymentId;
            _cartRepository.Save(bookingCart);
            return Redirect(response.ApprovalUrl);
        }

        [BookingRequired]
        public ActionResult AuthorisePayment(string payerId)
        {
            var bookingCart = _bookingContext.Current();
            _paymentService.CompletePayment(new PaymentRequest { PayerId = payerId, PayReference = bookingCart.PaymentReference });
            return RedirectToAction("Success");
        }

        // 
        // GET /Booking/Success
        [HttpGet, Authorize, AuthorizeCartIdentity]
        public ActionResult Success()
        {
            var bookingCart = _bookingContext.Current();

            var bookingOrder = _rateCalculator.Calculate(bookingCart);
            bookingOrder.SetRecipientDetails(_userManager.GetCurrentUser(this.User));

            var id = _bookingManager.CreateBooking(bookingCart, bookingOrder);

            // Complete the booking
            bookingCart.Complete();
            _cartRepository.Save(bookingCart);

            var currentUser = _userManager.GetCurrentUser(User);

            // Send email to the user and support staff
            var newBookingEmail = this.Map<BookingCart, NewBooking>(bookingCart);
            newBookingEmail.AdId = id.ToString(); // We can only get the Id 
            _broadcastManager.SendEmail(newBookingEmail, _clientConfig.SupportEmailList);
            _broadcastManager.SendEmail(newBookingEmail, currentUser.Email);

            // Build the view model
            var successView = new SuccessView
            {
                AdId = id.ToString(),
                IsBookingActive = bookingCart.StartDate <= DateTime.Today,
                ExistingUsers = _userManager.GetUserNetworksForUserId(currentUser.Username).Select(usr => new UserNetworkEmailView
                {
                    Email = usr.UserNetworkEmail,
                    FullName = usr.FullName,
                    Selected = true,
                }).ToArray()
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
        public ActionResult AssignOnlineImage(string documentId)
        {
            var bookingCart = _bookingContext.Current();

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
                    .Select(e => e.Date.ToString(DATE_FORMAT))
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

        [HttpPost, Authorize]
        public ActionResult NotifyContactsAboutMyAd(string id, List<UserNetworkEmailView> users)
        {
            int adId;
            // This should be a valid ad because AuthorizeBookingIdentity would have filtered it out
            if (!int.TryParse(id, out adId))
            {
                throw new ArgumentException("Invalid ad ID.");
            }

            var adSearchResult = _searchService.GetAdById(adId);

            foreach (var friendEmail in users.Where(u => u.Selected).Select(u => u.Email))
            {
                _broadcastManager.SendEmail(new AdShare
                {
                    AdvertiserName = adSearchResult.ContactName,
                    AdDescription = adSearchResult.HtmlText.TruncateOnWordBoundary(100),
                    AdTitle = adSearchResult.Heading,
                    ClientName = friendEmail
                }, friendEmail);
            }
            return Json("completed");
        }

        [HttpPost, Authorize]
        public ActionResult AddUserNetwork(UserNetworkEmailView userNetwork)
        {
            if (!ModelState.IsValid)
            {
                string[] errorList = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage).ToArray();
                return Json(new { valid = false, errors = errorList });
            }

            // Adds a contact for the existing (logged in user)
            _userManager.CreateUserNetwork(this.User, userNetwork.Email, userNetwork.FullName);

            return Json(new { valid = true });
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
            {
                return Json("Invalid model");
            }
            this.Map(eventViewModel, bookingCart);

            bookingCart.CompleteStep(2);
            _cartRepository.Save(bookingCart);

            return Json(new { nextUrl = Url.Action("Step3") });
        }

        #region Mappings
        public void OnRegisterMaps(IConfiguration configuration)
        {
            // Step 2 view is very flat with OnlineAd Prefix on properties
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<OnlineAdModel, Step2View>()
                .ForMember(member => member.OnlineAdDescription, options => options.MapFrom(src => src.HtmlText));
            configuration.CreateMap<OnlineAdModel, Step3View>();
            configuration.CreateMap<LineAdModel, Step2View>();
            configuration.CreateMap<BookingOrderResult, PriceSummaryView>()
                .ConvertUsing<PriceSummaryViewConverter>();
            configuration.CreateMap<BookingCart, Step3View>()
                .ForMember(m => m.PublicationCount, options => options.MapFrom(src => src.Publications.Length));
            configuration.CreateMap<IBookingCart, EventViewModel>().ConvertUsing(new BookingCartToEventViewConverter(_dateService));

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


            // To Email Template
            configuration.CreateMap<BookingCart, NewBooking>()
                .ForMember(member => member.AdDescription, options => options.MapFrom(source => source.OnlineAdModel.HtmlText))
                .ForMember(member => member.AdHeading, options => options.MapFrom(source => source.OnlineAdModel.Heading))
                .ForMember(member => member.StartDate, options => options.MapFrom(source => source.StartDate.Value.ToString("dd-MMM-yyyy")))
                .ForMember(member => member.EndDate, options => options.MapFrom(source => source.EndDate.Value.ToString("dd-MMM-yyyy")))
                .ForMember(member => member.TotalPrice, options => options.MapFrom(source => source.TotalPrice.ToString("N")))
                ;
        }

        #endregion

    }


}
