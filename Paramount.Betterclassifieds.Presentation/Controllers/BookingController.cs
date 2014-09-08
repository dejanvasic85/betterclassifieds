using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    using Business.Models;
    using Business.Search;
    using ViewModels.Booking;

    public class BookingController : Controller, IMappingBehaviour
    {
        private readonly ISearchService _searchService;
        private readonly BookingCartSessionManager _bookingCartSessionManager;
        private readonly BookingCartRepository _bookingCartRepository;

        public BookingController(ISearchService searchService, BookingCartSessionManager bookingCartSessionManager)
        {
            _searchService = searchService;
            _bookingCartSessionManager = bookingCartSessionManager;
            _bookingCartRepository = new BookingCartRepository();
        }

        //
        // GET: /Booking/
        [HttpGet]
        public ActionResult Step1()
        {
            var bookingId = _bookingCartSessionManager.GetId();

            var stepOneView = new Step1View
            {
                ParentCategories = _searchService.GetTopLevelCategories().Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationView>(_searchService.GetPublications()),
                BookingCart = _bookingCartRepository.GetBookingCart(bookingId)
            };

            return View(stepOneView);
        }

        [HttpPost]
        public ActionResult Step1(int categoryId, int[] publications)
        {
            // Create new booking here if doesn't exist
            var id = _bookingCartSessionManager.GetId();
            BookingCart bookingCart = id.HasValue() 
                ? _bookingCartRepository.GetBookingCart(id) 
                : BookingCartFactory.CreateBookingCart(Session.SessionID, User.Identity.Name);

            bookingCart.CategoryId = categoryId;
            bookingCart.Publications = publications == null ? new List<int>() : publications.ToList();

            // Persist and move on
            _bookingCartRepository.SaveBookingCart(bookingCart);


            return Json("success");
        }

        public ActionResult Step2()
        {
            var bookingCart = _bookingCartRepository.GetBookingCart(  _bookingCartSessionManager.GetId() );

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
