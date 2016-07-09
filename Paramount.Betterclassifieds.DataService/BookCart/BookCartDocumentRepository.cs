using System;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;

namespace Paramount.Betterclassifieds.DataService
{
    public class BookCartDocumentRepository : IBookCartRepository
    {
        private readonly IDocumentRepository _documentRepository;

        public BookCartDocumentRepository(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public BookingCart GetBookingCart(string id)
        {
            return _documentRepository.GetJsonDocument<BookingCart>(new Guid(id));
        }

        public IBookingCart Save(IBookingCart bookingCart)
        {
            Guard.NotNull(bookingCart);
            Guard.NotNullOrEmpty(bookingCart.Id);

            var id = new Guid(bookingCart.Id);
            _documentRepository.CreateOrUpdateJsonDocument(id, bookingCart);
            return bookingCart;
        }
    }
}