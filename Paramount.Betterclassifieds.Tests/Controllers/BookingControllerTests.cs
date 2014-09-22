using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class BookingControllerTests : ControllerTest<BookingController>
    {
        private Mock<ISearchService> mockSearchService;
        private Mock<IBookingManager> mockBookingManager;

        [SetUp]
        public void SetupDependencies()
        {
            mockSearchService = CreateMockOf<ISearchService>();
            mockBookingManager = CreateMockOf<IBookingManager>();
        }

    }
}