using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Repository;

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [TestFixture]
    public class BookingRepositoryTests
    {
        private IBookingRepository _bookingRepository;

        public BookingRepositoryTests()
        {
            _bookingRepository = new BookingRepository(
                new ServerDateService(),
                new DbContextFactory());
        }

        [Test]
        public void GetBookingsForOnlineAds()
        {
            var results = _bookingRepository
                .GetBookingsForOnlineAds(new[] { 1, 2, 3 });
        }
    }
}
