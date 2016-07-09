using System;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Repository;

namespace Paramount.Betterclassifieds.Payments.Stripe.Tests
{
    [TestFixture]
    public class DocumentRepositoryTests
    {
        [Test]
        public void CrudOperations_Are_Successful()
        {
            var repo = new DocumentRepository(new DbContextFactory());

            var bookingCart = BookingCart.Create("session123", "tempuser");
            var documentId = new Guid(bookingCart.Id);
            var jsonData = JsonConvert.SerializeObject(bookingCart);
            var bytes = Encoding.ASCII.GetBytes(jsonData);            
            var document = new Document(documentId, 
                bytes,
                ContentType.Json,
                $"booking-{documentId}.json",
                bytes.Length);

            repo.Create(document);

            // Get
            var retrievedDocument = repo.GetDocument(documentId);
            var retrievedData = Encoding.ASCII.GetString(retrievedDocument.Data);
            var retrievedCart = JsonConvert.DeserializeObject<BookingCart>(retrievedData);
            Assert.That(retrievedCart.SessionId, Is.EqualTo("session123"));

            // Update
            retrievedCart.Event = new EventModel
            {
                Location = "9 Sophia Street, Sunshine West"
            };
            retrievedDocument.Data = ToJson(retrievedCart);
            repo.UpdateDocument(retrievedDocument);

            // Drop
            repo.DeleteDocument(documentId);
        }

        private byte[] ToJson(IBookingCart cart)
        {
            var str = JsonConvert.SerializeObject(cart);
            var bytes = Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        private IBookingCart FromJson(byte[] data)
        {
            var str = Encoding.ASCII.GetString(data);
            var obj = JsonConvert.DeserializeObject<IBookingCart>(str);
            return obj;
        }
    }
}