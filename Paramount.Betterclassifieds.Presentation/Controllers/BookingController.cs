﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business.Models;
    using Business.Search;
    using ViewModels.Booking;

    public class BookingController : Controller, IMappingBehaviour
    {
        private readonly IUnityContainer _container;
        private readonly ISearchService _searchService;
        private readonly IBookingId _bookingId;
        private readonly IBookingCartRepository _bookingCartRepository;

        public BookingController(IUnityContainer container, ISearchService searchService, IBookingId bookingId, IBookingCartRepository bookingCartRepository)
        {
            _searchService = searchService;
            _bookingId = bookingId;
            _bookingCartRepository = bookingCartRepository;
            _container = container;
        }

        //
        // GET: /Booking/Step/1 - Category and publications
        [HttpGet]
        public ActionResult Step1()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            var categories = _searchService.GetCategories();

            var stepOneView = new Step1View
            {
                ParentCategoryOptions = categories.Where(c => c.ParentId == null).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationSelectionView>(_searchService.GetPublications()),
            };

            if (bookingCart != null)
            {
                stepOneView.CategoryId = bookingCart.CategoryId;
                stepOneView.SubCategoryId = bookingCart.SubCategoryId;

                // Set selected publications
                if (bookingCart.Publications != null)
                {
                    stepOneView.SetSelectedPublications(bookingCart.Publications);
                }

                // Load subcategories (if parent is selected)
                if (bookingCart.CategoryId.HasValue)
                {
                    stepOneView.SubCategoryOptions = categories.Where(c => c.ParentId == bookingCart.CategoryId.Value).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() });
                }
            }

            return View(stepOneView);
        }

        [HttpPost]
        public ActionResult Step1(Step1View viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Fetch the booking cart from repository
            // If null, then use the the container to resolve it ( using a factory - see PresentationInitialiser )
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id) ?? _container.Resolve<BookingCart>();

            // Map step 1 model to the view cart
            bookingCart.CategoryId = viewModel.CategoryId;
            bookingCart.SubCategoryId = viewModel.SubCategoryId;
            bookingCart.Publications = viewModel.Publications.Where(p => p.IsSelected).Select(p => p.PublicationId).ToArray();

            //// Persist and move on
            _bookingCartRepository.SaveBookingCart(bookingCart);

            // Our view can't "submit" the form
            return Json(Url.Action("Step2"));
        }

        //
        // GET: /Booking/Step/2 - ad details
        public ActionResult Step2()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.ToString());

            if (bookingCart == null || bookingCart.IsStep1NotComplete())
                throw new BookingNotValidException();

            var stepTwoModel = this.Map<BookingCart, Step2View>(bookingCart);

            return View(stepTwoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Step2(Step2View viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Map from view
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            this.Map(viewModel, bookingCart);

            // Save and continue
            _bookingCartRepository.SaveBookingCart(bookingCart);

            return RedirectToAction("Step3");
        }

        // 
        // GET /Booking/Step/3 - Scheduling
        public ActionResult Step3()
        {
            // Validate current state of the booking
            var booking = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            if (booking == null || booking.IsStep2NotComplete())
                throw new BookingNotValidException();

            var viewModel = this.Map<BookingCart, Step3View>(booking);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Step3(Step3View viewModel)
        {
            return View();
        }

        
        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();

            configuration.CreateMap<BookingCart, Step2View>()
                // IsLineAdIncluded should be set only if there are publications selected in step 1
                .ForMember(member => member.IsLineAdIncluded, options => options.MapFrom(source => source.Publications.Any()))
                ;

            configuration.CreateMap<BookingCart, Step3View>()
                // IsLineAdIncluded should be set only if there are publications selected in step 1
               .ForMember(member => member.IsLineAdIncluded, options => options.MapFrom(source => source.Publications.Any()))
               ;


            configuration.CreateMap<Step2View, BookingCart>();
            configuration.CreateMap<Step3View, BookingCart>();

        }
    }

}
