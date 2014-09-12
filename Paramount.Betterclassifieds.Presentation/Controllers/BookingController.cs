using System.Linq;
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
        // GET: /Booking/
        [HttpGet]
        public ActionResult Step1()
        {
            BookingCart bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id);

            var stepOneView = new Step1View
            {
                ParentCategories = _searchService.GetTopLevelCategories().Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationView>(_searchService.GetPublications()),
            };

            if (bookingCart != null)
            {
                stepOneView.SelectedCategoryId = bookingCart.CategoryId;
                stepOneView.SelectedSubCategoryId = bookingCart.SubCategoryId;
                stepOneView.SelectedPublications = bookingCart.Publications;
            }

            return View(stepOneView);
        }

        [HttpPost]
        public ActionResult Step1(int categoryId, int[] publications)
        {
            // Fetch the booking cart from repository
            // If null, then use the the container to resolve it ( using a factory - see PresentationInitialiser )
            BookingCart bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id) ?? _container.Resolve<BookingCart>();

            bookingCart.CategoryId = categoryId;
            bookingCart.Publications = publications;

            // Persist and move on
            _bookingCartRepository.SaveBookingCart(bookingCart);

            return Json(Url.Action("Step2"));
        }

        public ActionResult Step2()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.ToString());

            if (bookingCart == null || bookingCart.IsStep1NotValid())
                throw new BookingNotValidException();

            return View();
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<PublicationModel, PublicationView>();
        }
    }

}
