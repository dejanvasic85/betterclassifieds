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
        private readonly IBookingId _bookingId;
        private readonly BookingCartRepository _bookingCartRepository;

        public BookingController(ISearchService searchService, IBookingId bookingId)
        {
            _searchService = searchService;
            _bookingId = bookingId;
            _bookingCartRepository = new BookingCartRepository();
        }

        //
        // GET: /Booking/
        [HttpGet]
        public ActionResult Step1()
        {
            var stepOneView = new Step1View
            {
                ParentCategories = _searchService.GetTopLevelCategories().Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() }),
                Publications = this.MapList<PublicationModel, PublicationView>(_searchService.GetPublications()),
                BookingCart = _bookingCartRepository.GetBookingCart(_bookingId.Id)
            };

            return View(stepOneView);
        }

        [HttpPost]
        public ActionResult Step1(int categoryId, int[] publications)
        {
            // Create new booking here if doesn't exist
            var bookingCart = _bookingCartRepository.GetBookingCart(_bookingId.ToString());
            if (bookingCart == null)
                bookingCart = BookingCartFactory.CreateBookingCart(Session.SessionID, User.Identity.Name, _bookingId);

            bookingCart.CategoryId = categoryId;
            bookingCart.Publications = publications == null ? new List<int>() : publications.ToList();

            // Persist and move on
            _bookingCartRepository.SaveBookingCart(bookingCart);

            return Json("success");
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
