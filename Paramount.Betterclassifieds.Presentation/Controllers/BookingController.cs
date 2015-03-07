﻿namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;

    using Business;
    using Business.Booking;
    using Business.Print;
    using Business.Broadcast;
    using Business.Search;
    using Business.DocumentStorage;
    using Business.Payment;
    using ViewModels.Booking;
    using ViewModels;

    public class BookingController : Controller, IMappingBehaviour
    {
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
            IPaymentService paymentService)
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
        }

        #region Steps

        //
        // GET: /Booking/Step/1 - Category and publications
        [HttpGet, BookingStep(1)]
        public ActionResult Step1()
        {
            var bookingCart = _bookingContext.Current();
            var categories = _searchService.GetCategories();

            var viewModel = new Step1View
            {
                ParentCategoryOptions = categories.Where(c => c.ParentId == null).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationSelectionView>(_searchService.GetPublications()),
                CategoryId = bookingCart.CategoryId,
                SubCategoryId = bookingCart.SubCategoryId,
            };

            // Set selected publications
            viewModel.SetSelectedPublications(bookingCart.Publications);

            // Load subcategories (if parent is selected)
            if (bookingCart.CategoryId.HasValue)
            {
                viewModel.SubCategoryOptions = categories.Where(c => c.ParentId == bookingCart.CategoryId.Value).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() });
            }

            return View(viewModel);
        }

        [HttpPost, BookingStep(1)]
        public ActionResult Step1(Step1View viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { errorMsg = "Please ensure you select a category before next step." });
            }

            var bookingCart = _bookingContext.Current();
            bookingCart.CategoryId = viewModel.CategoryId;
            bookingCart.SubCategoryId = viewModel.SubCategoryId;

            if (viewModel.Publications != null)
                bookingCart.Publications = viewModel.Publications.Where(p => p.IsSelected).Select(p => p.PublicationId).ToArray();

            bookingCart.CompleteStep(1);
            _cartRepository.Save(bookingCart);

            // Our view can't "submit" the form so just return json with the redirection url
            return Json(Url.Action("Step2"));
        }

        //
        // GET: /Booking/Step/2 - ad details
        [HttpGet, BookingStep(2)]
        public ActionResult Step2()
        {
            var bookingCart = _bookingContext.Current();
            var stepTwoModel = this.Map<OnlineAdModel, Step2View>(bookingCart.OnlineAdModel);
            this.Map(bookingCart.LineAdModel, stepTwoModel);

            // Set max number of images available for upload ( available on global client configuration object )
            stepTwoModel.MaxOnlineImages = _clientConfig.MaxOnlineImages;

            // Set the configured max image size
            stepTwoModel.MaxImageUploadBytes = _applicationConfig.MaxImageUploadBytes;

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


            this.Map(viewModel, bookingCart.OnlineAdModel);
            this.Map(viewModel, bookingCart.LineAdModel);
            bookingCart.OnlineAdModel.SetDescription(viewModel.OnlineAdDescription);
            bookingCart.CompleteStep(2);

            // Save and continue
            _cartRepository.Save(bookingCart);

            return RedirectToAction("Step3");
        }

        // 
        // GET /Booking/Step/3 - Scheduling
        [HttpGet, BookingStep(3)]
        public ActionResult Step3()
        {
            var bookingCart = _bookingContext.Current();
            var viewModel = new Step3View
            {
                StartDate = bookingCart.GetStartDateOrMinimum(),
                DurationDays = _clientConfig.RestrictedOnlineDaysCount
            };

            // todo - Line Ads - Fetch the up-coming available editions

            return View(viewModel);
        }

        [HttpPost, BookingStep(3)]
        public ActionResult Step3(Step3View viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var bookingCart = _bookingContext.Current();
            bookingCart.SetSchedule(_clientConfig, viewModel.StartDate.GetValueOrDefault());
            bookingCart.CompleteStep(3);
            _cartRepository.Save(bookingCart);

            return RedirectToAction("Step4");
        }

        // 
        // GET /Booking/Step/4 - Confirmation
        [HttpGet, BookingStep(4), Authorize]
        public ActionResult Step4(string cancel)
        {
            bool isPaymentCancelled;
            bool.TryParse(cancel, out isPaymentCancelled);

            var bookingCart = _bookingContext.Current();
            bookingCart.TotalPrice = _rateCalculator.Calculate(bookingCart).Total;
            _cartRepository.Save(bookingCart);

            var viewModel = this.Map<BookingCart, Step4View>(bookingCart);
            viewModel.IsPaymentCancelled = isPaymentCancelled;

            return View(viewModel);
        }

        [HttpPost, BookingStep(4), Authorize]
        public ActionResult Step4(Step4View viewModel)
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
            bookingCart.CompleteStep(4);
            bookingCart.UserId = _userManager.GetCurrentUser(this.User).Username;

            if (bookingCart.NoPaymentRequired())
            {
                _cartRepository.Save(bookingCart);
                return RedirectToAction("Success");
            }

            // We only support paypal just for now
            var response = _paymentService.SubmitPayment(new PaymentRequest
            {
                PayReference = bookingCart.Reference,
                BookingRateResult = _rateCalculator.Calculate(bookingCart),
                ReturnUrl = Url.ActionAbsolute("AuthorisePayment", "Booking"),
                CancelUrl = Url.ActionAbsolute("Step4", "Booking").Append("?cancel=true")
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

            var id = _bookingManager.CreateBooking(bookingCart);

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

            return View(successView);
        }

        #endregion

        #region Json Requests

        [HttpPost, BookingRequired]
        public ActionResult UploadOnlineImage()
        {
            var bookingCart = _bookingContext.Current();

            Guid? documentId = null;

            var files = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].As<HttpPostedFileBase>())
                .Where(postedFile => postedFile != null && postedFile.ContentLength != 0)
                .ToList();

            if (bookingCart.OnlineAdModel.Images.Count + files.Count > _clientConfig.MaxOnlineImages)
            {
                return Json(new { errorMsg = string.Format("File limit reached. You can upload up to {0} images.", _clientConfig.MaxOnlineImages) });
            }

            // There should only be 1 uploaded file so just check the size ...
            var uploadedFile = files.First();
            if (uploadedFile.ContentLength > _applicationConfig.MaxImageUploadBytes)
            {
                return Json(new { errorMsg = "The file exceeds the maximum file size." });
            }

            if (!_applicationConfig.AcceptedImageFileTypes.Any(type => type.Equals(uploadedFile.ContentType)))
            {
                return Json(new { errorMsg = "Not an accepted file type." });
            }

            documentId = Guid.NewGuid();

            var imageDocument = new Document(documentId.Value, uploadedFile.InputStream.FromStream(), uploadedFile.ContentType,
                uploadedFile.FileName, uploadedFile.ContentLength, this.User.Identity.Name);

            _documentRepository.Save(imageDocument);

            // Persist to the booking cart also
            bookingCart.OnlineAdModel.AddImage(documentId.ToString());
            _cartRepository.Save(bookingCart);


            return Json(new { documentId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, BookingRequired]
        public ActionResult RemoveOnlineImage(Guid documentId)
        {
            // Remove the image from the document repository
            _documentRepository.DeleteDocument(documentId);

            var bookingCart = _bookingContext.Current();
            bookingCart.OnlineAdModel.RemoveImage(documentId.ToString());
            _cartRepository.Save(bookingCart);

            return Json(new { removed = true });
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
            var viewModel = this.Map<BookingRateResult, PriceSummaryView>(bookingRateResult);

            return Json(viewModel);
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

        #endregion

        #region Mappings
        public void OnRegisterMaps(IConfiguration configuration)
        {
            // Step 2 view is very flat with OnlineAd Prefix on properties
            configuration.RecognizeDestinationPrefixes("OnlineAd", "Line");
            configuration.RecognizePrefixes("OnlineAd", "Line");

            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<OnlineAdModel, Step2View>();
            configuration.CreateMap<LineAdModel, Step2View>()
                .ForMember(m => m.LineAdText, options => options.MapFrom(src => src.AdText.Replace("'", "''")));
            configuration.CreateMap<OnlineAdModel, Step4View>();
            configuration.CreateMap<BookingCart, Step4View>();
            configuration.CreateMap<BookingRateResult, PriceSummaryView>()
                .ConvertUsing<PriceSummaryViewConverter>();

            // From ViewModel
            configuration.CreateMap<Step2View, OnlineAdModel>()
                .ForMember(member => member.Images, options => options.Ignore());
            configuration.CreateMap<Step2View, LineAdModel>();
            configuration.CreateMap<UserNetworkEmailView, UserNetworkModel>();
            configuration.CreateMap<PricingFactorsView, PricingFactors>();

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
