using System;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    internal class EditAdControllerTests : ControllerTest<EditAdController>
    {

        [Test]
        public void EventDashboard_Get_Returns_ViewResult()
        {
            // Arrange 
            const int adId = 23;
            const int onlineAdId = 90;
            const int eventId = 10;

            var mockSearchResult = new AdSearchResultMockBuilder().WithOnlineAdId(onlineAdId).WithNumOfViews(7).Build();
            var mockGuestListBuilder = new EventGuestDetailsMockBuilder().WithGuestEmail("foo@bar.com").WithGuestFullName("Foo Bar").WithTicketNumber(123);
            var mockGuestList = new[] { mockGuestListBuilder.Build(), mockGuestListBuilder.Build() };
            var mockTicketBuilder = new EventTicketMockBuilder().WithRemainingQuantity(5).WithEventId(eventId);
            var mockEvent = new EventModelMockBuilder().WithEventId(eventId).WithTickets(new[] { mockTicketBuilder.Build() }).Build();
            var mockPaymentSummary = new EventPaymentSummaryMockBuilder().WithEventOrganiserOwedAmount(90).WithSystemTicketFee(10).WithTotalTicketSalesAmount(100).Build();
            

            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == adId)), mockSearchResult);
            _eventManagerMock.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(It.Is<int>(p => p == onlineAdId), It.Is<bool>(p => true)), mockEvent);
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.Is<int>(p => p == eventId)), mockGuestList);
            _eventManagerMock.SetupWithVerification(call => call.BuildPaymentSummary(It.Is<int>(p => p == eventId)), mockPaymentSummary);
            _eventManagerMock.SetupWithVerification(call => call.GetEventPaymentRequestStatus(It.Is<int>(p => p == eventId)), EventPaymentRequestStatus.Complete);

            // Act 
            var result = BuildController().EventDashboard(adId);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as EventDashboardViewModel;
            Assert.That(viewModel, Is.Not.Null);

            Assert.That(viewModel.PageViews, Is.EqualTo(7));
            Assert.That(viewModel.Tickets.Count, Is.EqualTo(1));
            Assert.That(viewModel.AdId, Is.EqualTo(adId));
            Assert.That(viewModel.EventId, Is.EqualTo(eventId));
            Assert.That(viewModel.Guests.Count, Is.EqualTo(2));
            Assert.That(viewModel.TotalRemainingQty, Is.EqualTo(5));
            Assert.That(viewModel.SystemTicketFeeLabel, Is.EqualTo("10%"));
            Assert.That(viewModel.EventOrganiserOwedAmount, Is.EqualTo(90));
            Assert.That(viewModel.TotalSoldAmount, Is.EqualTo(100));
        }

        [Test]
        public void EventGuestListDownloadPdf_Get_Returns_FileResult()
        {
            // arrange
            const int adId = 1;
            const int eventId = 2;
            const string mockPdfOutput = "<html><body>Sample Data</body></html>";


            var builder = new EventGuestDetailsMockBuilder();
            var mockGuests = new[]
            {
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build(),
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build()
            };

            var mockSearchResult = new AdSearchResultMockBuilder().WithHeading("Testing").Build();

            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == 1)), mockSearchResult);
            _templatingServiceMock.SetupWithVerification(call => call.Generate(It.IsAny<object>(), It.Is<string>(param => param == "EventGuestList")), mockPdfOutput);
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.Is<int?>(val => val == eventId)), mockGuests);


            // act
            var result = BuildController().EventGuestListDownloadPdf(adId, eventId);

            // assert
            Assert.That(result, Is.TypeOf<FileContentResult>());
            var fileContentResult = (FileContentResult)result;
            Assert.That(fileContentResult.FileDownloadName, Is.EqualTo("Guest List.pdf"));
            Assert.That(fileContentResult.ContentType, Is.EqualTo("application/pdf"));

        }

        [Test]
        public void EventPaymentRequest_Get_Returns_View()
        {
            // arrange
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(It.IsAny<IPrincipal>()), mockApplicationUser);
            var mockPrincipal = CreateMockOf<IPrincipal>();
            var mockPaymentSummary = new EventPaymentSummaryMockBuilder()
                .WithEventOrganiserOwedAmount(90)
                .WithSystemTicketFee(10)
                .WithTotalTicketSalesAmount(100)
                .Build();

            _eventManagerMock.SetupWithVerification(call => call.BuildPaymentSummary(It.IsAny<int>()), mockPaymentSummary);


            // act
            var result = this.BuildController(mockUser: mockPrincipal).EventPaymentRequest(It.IsAny<int>(), It.IsAny<int>());

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as EventPaymentSummaryViewModel;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.AmountOwed, Is.EqualTo(90));
            Assert.That(viewModel.OurFeesPercentage, Is.EqualTo(10));
            Assert.That(viewModel.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(viewModel.PreferredPaymentType, Is.EqualTo(mockApplicationUser.PreferredPaymentMethod.ToString()));
            Assert.That(viewModel.PayPalEmail, Is.EqualTo(mockApplicationUser.PayPalEmail));
            Assert.That(viewModel.DirectDebitDetails, Is.Not.Null);
            Assert.That(viewModel.DirectDebitDetails.BankName, Is.EqualTo(mockApplicationUser.BankName));
            Assert.That(viewModel.DirectDebitDetails.AccountName, Is.EqualTo(mockApplicationUser.BankAccountName));
            Assert.That(viewModel.DirectDebitDetails.AccountNumber, Is.EqualTo(mockApplicationUser.BankAccountNumber));
            Assert.That(viewModel.DirectDebitDetails.BSB, Is.EqualTo(mockApplicationUser.BankBsbNumber));
        }

        [Test]
        public void EventPaymentRequest_Post_CallsManagerAndBroadcast_ReturnsJsonUrl()
        {
            // arrange
            var username = "fooBarr";
            var mockUser = CreateMockOf<IPrincipal>().SetupIdentityCall(username);
            var adId = 10;
            var eventId = 1;
            var requestedAmount = 100;
            var paymentMethod = "PayPal";
            var supportEmails = new[] {"support@email.com"};

            _clientConfigMock.SetupWithVerification(call => call.SupportEmailList, supportEmails);
            _eventManagerMock.SetupWithVerification(call => call.CreateEventPaymentRequest(
                It.Is<int>(p => p == eventId),
                It.Is<PaymentType>(p => p == PaymentType.PayPal),
                It.Is<decimal>(p => p == requestedAmount),
                It.Is<string>(p => p == username)));

            _broadcastManagerMock.SetupWithVerification(call => call.SendEmail(
                It.IsAny<Business.Broadcast.EventPaymentRequest>(),
                It.Is<string[]>(p => p == supportEmails)), Guid.NewGuid());

            // act
            var controller = BuildController(mockUser: mockUser);

            var result = controller.EventPaymentRequest(adId, new EventPaymentRequestViewModel
            {
                EventId = eventId,
                PaymentMethod = paymentMethod,
                RequestedAmount = requestedAmount
            });

            // assert
            Assert.That(result, Is.TypeOf<JsonResult>());
            var jsonResult = (JsonResult)result;
            Assert.That(jsonResult.Data.ToString(), Is.EqualTo("{ NextUrl = /EditAd/EventDashboard?id=" + adId + " }"));
        }

        private Mock<ISearchService> _searchServiceMock;
        private Mock<IApplicationConfig> _applicationConfigMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IBookingManager> _bookingManagerMock;
        private Mock<IEventManager> _eventManagerMock;
        private Mock<ITemplatingService> _templatingServiceMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IBroadcastManager> _broadcastManagerMock;

        [SetUp]
        public void SetupDependencies()
        {
            _searchServiceMock = CreateMockOf<ISearchService>();
            _applicationConfigMock = CreateMockOf<IApplicationConfig>();
            _clientConfigMock = CreateMockOf<IClientConfig>();
            _bookingManagerMock = CreateMockOf<IBookingManager>();
            _eventManagerMock = CreateMockOf<IEventManager>();
            _templatingServiceMock = CreateMockOf<ITemplatingService>();
            _templatingServiceMock.Setup(call => call.Init(It.IsAny<Controller>())).Returns(_templatingServiceMock.Object);
            _userManagerMock = CreateMockOf<IUserManager>();
            _broadcastManagerMock = CreateMockOf<IBroadcastManager>();
        }
    }
}