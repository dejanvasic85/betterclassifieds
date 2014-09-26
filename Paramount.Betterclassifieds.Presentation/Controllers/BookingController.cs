using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business.Models;
    using Business.Search;
    using Business.Managers;
    using ViewModels.Booking;

    public class BookingController : Controller, IMappingBehaviour
    {
        private readonly IUnityContainer _container;
        private readonly ISearchService _searchService;
        private readonly IBookingId _bookingId;
        private readonly IBookingCartRepository _bookingCartRepository;
        private readonly IClientConfig _clientConfig;

        public BookingController(IUnityContainer container, ISearchService searchService, IBookingId bookingId, IBookingCartRepository bookingCartRepository, IClientConfig clientConfig)
        {
            _searchService = searchService;
            _bookingId = bookingId;
            _bookingCartRepository = bookingCartRepository;
            _clientConfig = clientConfig;
            _container = container;
        }

        //
        // GET: /Booking/Step/1 - Category and publications
        [HttpGet, BookingStep(1)]
        public ActionResult Step1()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            var categories = _searchService.GetCategories();

            var viewModel = new Step1View
            {
                ParentCategoryOptions = categories.Where(c => c.ParentId == null).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationSelectionView>(_searchService.GetPublications()),
            };

            // There is an existing booking cart already
            if (bookingCart != null)
            {
                viewModel.CategoryId = bookingCart.CategoryId;
                viewModel.SubCategoryId = bookingCart.SubCategoryId;

                // Set selected publications
                if (bookingCart.Publications != null)
                {
                    viewModel.SetSelectedPublications(bookingCart.Publications);
                }

                // Load subcategories (if parent is selected)
                if (bookingCart.CategoryId.HasValue)
                {
                    viewModel.SubCategoryOptions = categories.Where(c => c.ParentId == bookingCart.CategoryId.Value).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() });
                }
            }

            return View(viewModel);
        }

        [HttpPost, BookingStep(1)]
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
            bookingCart.CompletedSteps.Add(1);

            //// Persist and move on
            _bookingCartRepository.SaveBookingCart(bookingCart);

            // Our view can't "submit" the form
            return Json(Url.Action("Step2"));
        }

        //
        // GET: /Booking/Step/2 - ad details
        [HttpGet, BookingStep(2)]
        public ActionResult Step2()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            var stepTwoModel = this.Map<OnlineAdCart, Step2View>(bookingCart.OnlineAdCart);

            return View(stepTwoModel);
        }

        [HttpPost, ValidateAntiForgeryToken, BookingStep(2)]
        public ActionResult Step2(Step2View viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Todo - convert markdown to html
            // var markdown = new MarkdownDeep.Markdown();
            // markdown.Transform(viewModel.OnlineAdDescription);
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            bookingCart.OnlineAdCart = this.Map<Step2View, OnlineAdCart>(viewModel);
            bookingCart.CompletedSteps.Add(2);

            // Save and continue
            _bookingCartRepository.SaveBookingCart(bookingCart);

            return RedirectToAction("Step3");
        }

        // 
        // GET /Booking/Step/3 - Scheduling
        [HttpGet, BookingStep(3)]
        public ActionResult Step3()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
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

            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            bookingCart.StartDate = viewModel.StartDate;
            bookingCart.CompletedSteps.Add(3);

            _bookingCartRepository.SaveBookingCart(bookingCart);

            return RedirectToAction("Step4");
        }

        // 
        // GET /Booking/Step/4 - Confirmation
        [HttpGet, BookingStep(4)]
        public ActionResult Step4()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            var viewModel = new Step4View();

            return View(viewModel);
        }

        [HttpPost, BookingStep(4)]
        public ActionResult Step4(Step4View viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            if (bookingCart.NoPaymentRequired())
            {
                // Todo - submit the booking and redirect to success
            }
            else
            {
                // Todo - hook up the payments
            }

            bookingCart.CompletedSteps.Add(4);
            bookingCart.Completed = true;

            _bookingCartRepository.SaveBookingCart(bookingCart);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            return Json(new { completed = true });
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            // Step 2 view is very flat with OnlineAd Prefix on properties
            configuration.RecognizeDestinationPrefixes("OnlineAd");
            configuration.RecognizePrefixes("OnlineAd");

            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<OnlineAdCart, Step2View>();

            // From ViewModel
            configuration.CreateMap<Step2View, OnlineAdCart>();
        }
    }

}
