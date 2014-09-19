using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business.Managers;

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
        [HttpGet]
        public ActionResult Step1()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            var categories = _searchService.GetCategories();

            var viewModel = new Step1View
            {
                ParentCategoryOptions = categories.Where(c => c.ParentId == null).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationSelectionView>(_searchService.GetPublications()),
            };

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

            // Todo - move this to a filter? People shouldn't reach this method if they haven't done previous steps
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

            // Todo - convert markdown to html
            // var markdown = new MarkdownDeep.Markdown();
            // markdown.Transform(viewModel.OnlineAdDescription);

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
            // Todo - move this to a filter? People shouldn't reach this method if they haven't done previous steps
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);
            if (bookingCart == null || bookingCart.IsStep2NotComplete())
                throw new BookingNotValidException();

            var viewModel = this.Map<BookingCart, Step3View>(bookingCart);
            
            // Fetch the up-coming available editions
            viewModel.DurationDays = _clientConfig.RestrictedOnlineDaysCount;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Step3(Step3View viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            return View(viewModel);
        }

        
        public void OnRegisterMaps(IConfiguration configuration)
        {
            // To view model
            configuration.CreateMap<PublicationModel, PublicationSelectionView>();
            configuration.CreateMap<BookingCart, Step2View>();
            configuration.CreateMap<BookingCart, Step3View>();
            
            // From ViewModel
            configuration.CreateMap<Step2View, BookingCart>();
            configuration.CreateMap<Step3View, BookingCart>();
        }
    }

}
