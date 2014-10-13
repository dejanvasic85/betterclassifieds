﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business;
    using Business.Broadcast;
    using Business.Models;
    using Business.Search;
    using Business.Managers;
    using Business.Repository;
    using ViewModels.Booking;

    public class BookingController : Controller, IMappingBehaviour
    {
        private readonly IUnityContainer _container;
        private readonly ISearchService _searchService;
        private readonly IBookingManager _bookingManager;
        private readonly IClientConfig _clientConfig;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserManager _userManager;
        private readonly IRateCalculator _rateCalculator;
        private readonly IBroadcastManager _broadcastManager;

        public BookingController(IUnityContainer container,
            ISearchService searchService,
            IClientConfig clientConfig,
            IDocumentRepository documentRepository,
            IBookingManager bookingManager,
            IUserManager userManager,
            IRateCalculator rateCalculator,
            IBroadcastManager broadcastManager)
        {
            _searchService = searchService;
            _clientConfig = clientConfig;
            _documentRepository = documentRepository;
            _bookingManager = bookingManager;
            _userManager = userManager;
            _rateCalculator = rateCalculator;
            _broadcastManager = broadcastManager;
            _container = container;
        }

        #region Steps
        //
        // GET: /Booking/Step/1 - Category and publications
        [HttpGet, BookingStep(1)]
        public ActionResult Step1()
        {
            // Fetch the cart but provider a creating method because it's the first step...
            var bookingCart = _bookingManager.GetCart(() => _container.Resolve<BookingCart>());
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
                return Json(new { errorMsg = "Please ensure you select a category before next step." });

            var bookingCart = _bookingManager.GetCart();
            bookingCart.CategoryId = viewModel.CategoryId;
            bookingCart.SubCategoryId = viewModel.SubCategoryId;
            bookingCart.Publications = viewModel.Publications.Where(p => p.IsSelected).Select(p => p.PublicationId).ToArray();
            bookingCart.CompleteStep(1);
            _bookingManager.SaveBookingCart(bookingCart);

            // Our view can't "submit" the form
            return Json(Url.Action("Step2"));
        }

        //
        // GET: /Booking/Step/2 - ad details
        [HttpGet, BookingStep(2)]
        public ActionResult Step2()
        {
            var bookingCart = _bookingManager.GetCart();
            var stepTwoModel = this.Map<OnlineAdCart, Step2View>(bookingCart.OnlineAdCart);

            // Load the location options
            stepTwoModel.LocationOptions = _searchService.GetLocations().Select(l => new SelectListItem { Text = l.Title, Value = l.LocationId.ToString() }).OrderBy(l => l.Text).ToList();
            int locationAreaId = stepTwoModel.OnlineAdLocationId.HasValue
                ? stepTwoModel.OnlineAdLocationId.Value
                : int.Parse(stepTwoModel.LocationOptions.First(l => l.Text.Trim().Equals("Any Location")).Value);

            stepTwoModel.LocationAreaOptions = _searchService.GetLocationAreas(locationAreaId)
                    .Select(l => new SelectListItem { Text = l.Title, Value = l.LocationAreaId.ToString() })
                    .OrderBy(l => l.Text).ToList();

            // Set max number of images available for upload ( available on global client configuration object )
            stepTwoModel.MaxOnlineImages = _clientConfig.MaxOnlineImages;

            // Set convenient contact details for the user
            var applicationUser = _userManager.GetCurrentUser(this.User);
            if (applicationUser != null)
            {
                stepTwoModel.OnlineAdContactName = applicationUser.FirstName;
                stepTwoModel.OnlineAdPhone = applicationUser.Phone;
                stepTwoModel.OnlineAdEmail = applicationUser.Email;
            }

            return View(stepTwoModel);
        }

        [HttpPost, ValidateAntiForgeryToken, BookingStep(2)]
        public ActionResult Step2(Step2View viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var bookingCart = _bookingManager.GetCart();
            this.Map(viewModel, bookingCart.OnlineAdCart);
            bookingCart.OnlineAdCart.SetDescriptionHtml(new MarkdownDeep.Markdown().Transform(viewModel.OnlineAdDescription));
            bookingCart.CompleteStep(2);

            // Save and continue
            _bookingManager.SaveBookingCart(bookingCart);

            return RedirectToAction("Step3");
        }

        // 
        // GET /Booking/Step/3 - Scheduling
        [HttpGet, BookingStep(3)]
        public ActionResult Step3()
        {
            var bookingCart = _bookingManager.GetCart();
            var viewModel = new Step3View
            {
                StartDate = bookingCart.StartDate,
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

            var bookingCart = _bookingManager.GetCart();
            bookingCart.SetSchedule(_clientConfig, viewModel.StartDate.GetValueOrDefault());
            bookingCart.CompleteStep(3);
            _bookingManager.SaveBookingCart(bookingCart);

            return RedirectToAction("Step4");
        }

        // 
        // GET /Booking/Step/4 - Confirmation
        [HttpGet, BookingStep(4), Authorize]
        public ActionResult Step4()
        {
            var bookingCart = _bookingManager.GetCart();
            bookingCart.TotalPrice = _rateCalculator.GetPriceBreakDown(bookingCart).Total;
            var viewModel = this.Map<BookingCart, Step4View>(bookingCart);
            this.Map(bookingCart.OnlineAdCart, viewModel);
            return View(viewModel);
        }

        [HttpPost, BookingStep(4), Authorize]
        public ActionResult Step4(Step4View viewModel)
        {
            var bookingCart = _bookingManager.GetCart();

            if (!ModelState.IsValid)
            {
                // Return the mapped object from the booking cart
                this.Map(bookingCart, viewModel);
                this.Map(bookingCart.OnlineAdCart, viewModel);
                return View(viewModel);
            }
            // Complete the booking cart (needs to move on now)
            bookingCart.CompleteStep(4);
            bookingCart.UserId = _userManager.GetCurrentUser(this.User).Username;

            _bookingManager.SaveBookingCart(bookingCart);

            if (bookingCart.NoPaymentRequired())
            {
                _bookingManager.CompleteCurrentBooking(bookingCart);
                return RedirectToAction("Success");
            }

            // Todo - payment providers

            return View(viewModel);
        }

        // 
        // GET /Booking/Success/{adId}
        [HttpGet, Authorize, AuthorizeBookingIdentity]
        public ActionResult Success(string id)
        {
            var currentUser = _userManager.GetCurrentUser(User).Username;

            var successView = new SuccessView
            {
                AdId = id,
                ExistingUserNetwork = _userManager.GetUserNetworksForUserId(currentUser).Select(usr => new UserNetworkEmailView
                {
                    Email = usr.UserNetworkEmail,
                    IsSelected = true
                }).ToArray()
            };
            return View(successView);
        }

        #endregion

        #region Json Requests

        [HttpPost, BookingRequired]
        public ActionResult UploadOnlineImage()
        {
            var bookingCart = _bookingManager.GetCart();

            Guid? documentId = null;

            var files = Request.Files.Cast<string>()
                .Select(file => Request.Files[file].CastTo<HttpPostedFileBase>())
                .Where(postedFile => postedFile != null && postedFile.ContentLength != 0)
                .ToList();

            if (bookingCart.OnlineAdCart.Images.Count + files.Count > _clientConfig.MaxOnlineImages)
            {
                return Json(new { errorMsg = string.Format("File limit reached. You can upload up to {0} images.", _clientConfig.MaxOnlineImages) });
            }

            // Todo validation on file types and file size - just another filter (where statement)

            foreach (var postedFile in files)
            {
                documentId = Guid.NewGuid();

                var imageDocument = new Document(documentId.Value, postedFile.InputStream.FromStream(), postedFile.ContentType,
                    postedFile.FileName, postedFile.ContentLength, this.User.Identity.Name);

                _documentRepository.Save(imageDocument);

                // Persist to the booking cart also
                bookingCart.OnlineAdCart.Images.Add(documentId.ToString());
                _bookingManager.SaveBookingCart(bookingCart);
            }

            return Json(new { documentId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, BookingRequired]
        public ActionResult RemoveOnlineImage(Guid documentId)
        {
            // Remove the image from the document repository
            _documentRepository.DeleteDocument(documentId);

            var bookingCart = _bookingManager.GetCart();
            bookingCart.OnlineAdCart.Images.Remove(documentId.ToString());
            _bookingManager.SaveBookingCart(bookingCart);

            return Json(new { removed = true });
        }

        [HttpGet, BookingRequired]
        public ActionResult GetRate()
        {
            var bookingCart = _bookingManager.GetCart();
            return Json(_rateCalculator.GetPriceBreakDown(bookingCart), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NotifyContactsAboutMyAd(int adId, IEnumerable<string> contactEmails)
        {
            var adSearchRTesult = _searchService.GetAdById(adId);

            foreach (var contactEmail in contactEmails)
            {
                _broadcastManager.SendEmail(new AdShare
                {
                    AdvertiserName = adSearchRTesult.ContactName,
                    AdDescription = adSearchRTesult.Description,
                    AdTitle = adSearchRTesult.Heading,
                    ClientName = contactEmail//change this to client name
                }, contactEmail);
            }

            return Json("completed");
        }

        #endregion

        #region Mappings
        public void OnRegisterMaps(IConfiguration configuration)
        {
            // Step 2 view is very flat with OnlineAd Prefix on properties
            configuration.RecognizeDestinationPrefixes("OnlineAd");
            configuration.RecognizePrefixes("OnlineAd");

            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<OnlineAdCart, Step2View>();
            configuration.CreateMap<BookingCart, Step4View>();
            configuration.CreateMap<OnlineAdCart, Step4View>();

            // From ViewModel
            configuration.CreateMap<Step2View, OnlineAdCart>()
                .ForMember(member => member.Images, options => options.Ignore());
        }

        #endregion

    }

}
