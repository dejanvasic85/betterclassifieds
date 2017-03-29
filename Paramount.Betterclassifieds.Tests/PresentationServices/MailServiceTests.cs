using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.PresentationServices
{
    [TestFixture]
    public class MailServiceTests : TestContext<MailService>
    {
        private Mock<ITemplatingService> _templatingService;
        private Mock<IUrl> _urlMock;
        private Mock<IUserManager> _userManager;
        private Mock<IMailSender> _mailSender;
        private Mock<IPdfGenerator> _pdfGenerator;
        private Mock<IClientConfig> _clientConfig;
        private ApplicationUser _mockUser;
        private Mock<ILogService> _logService;
        private Mock<ISearchService> _searchService;

        [SetUp] 
        public void SetupDependencies()
        {
            _urlMock = CreateMockOf<IUrl>();
            _urlMock.SetupWithVerification(call => call.WithAbsoluteUrl(), _urlMock.Object); // All outgoing links need to be absolute

            _templatingService = CreateMockOf<ITemplatingService>();
            _userManager = CreateMockOf<IUserManager>();
            _mailSender = CreateMockOf<IMailSender>();
            _pdfGenerator = CreateMockOf<IPdfGenerator>();
            _clientConfig = CreateMockOf<IClientConfig>();
            _logService = CreateMockOf<ILogService>();
            _searchService = CreateMockOf<ISearchService>();

            _mockUser = new ApplicationUserMockBuilder().Default().Build();
            _userManager.Setup(call => call.GetCurrentUser()).Returns(_mockUser);
        }

        [Test]
        public void SendEventOrganiserInvite_CreatesViewModel_CallsMailSender()
        {
            var mockAd = new AdSearchResultMockBuilder().Default().Build();
            var inviteToken = Guid.NewGuid().ToString();

            // Setup and verify
            _templatingService.SetupWithVerification(
                call => call.Generate(
                    It.IsAny<object>(),
                    It.Is<string>(view => view == "~/Views/Email/EventOrganiserInvite.cshtml"))
                    , "email-body");

            _urlMock.SetupWithVerification(
                call => call.Home(), "/home");

            _urlMock.SetupWithVerification(
                call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "/cool-event");

            _urlMock.SetupWithVerification(
                call => call.EventOrganiserInviteUrl(It.Is<int>(v => v == 1), It.Is<string>(t => t == inviteToken), It.Is<string>(r => r == "foo@bar.com")), "/invitation");

            _mailSender.SetupWithVerification(
                call => call.Send(
                    It.Is<string>(t => t == "foo@bar.com"),
                    It.Is<string>(b => b == "email-body"),
                    It.Is<string>(s => s == "Invite to manage " + mockAd.Heading)));


            var mailService = BuildTargetObject();

            mailService.SendEventOrganiserInvite("foo@bar.com",
                mockAd, 1, inviteToken);
        }

        [Test]
        public void SendEventOrganiserInvite_NoTo_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
            .SendEventOrganiserInvite(string.Empty, null, 0, string.Empty));
        }

        [Test]
        public void SendEventOrganiserInvite_NoAd_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => BuildTargetObject()
            .SendEventOrganiserInvite("foo@bar.com", null, 0, string.Empty));
        }

        [Test]
        public void SendEventOrganiserInvite_EventIsZero_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => BuildTargetObject()
            .SendEventOrganiserInvite("foo@bar.com", new AdSearchResult(), 0, string.Empty));
        }

        [Test]
        public void SendEventOrganiserInvite_TokenIsEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
            .SendEventOrganiserInvite("foo@bar.com", new AdSearchResult(), 1, string.Empty));
        }

        [Test]
        public void SendTicketBuyerNotification_NoInvoiceAttachment_CallsMailSender()
        {
            var mockAd = new AdSearchResultMockBuilder().Default().Build();
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventBooking = new EventBookingMockBuilder().Default()
                .WithEvent(mockEvent)
                .WithEventId(mockEvent.EventId.GetValueOrDefault())
                .WithTotalCost(0)
                .Build();

            // Setup service calls
            _urlMock.SetupWithVerification(
                call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "http://event-url.com");

            var htmlMockbodyHtml = "<html>MockBody</html>";
            _templatingService.SetupWithVerification(
                call => call.Generate(
                    It.IsAny<object>(),
                    It.Is<string>(str => str == "~/Views/Email/EventTicketBuyer.cshtml")),
                htmlMockbodyHtml);

            _mailSender.SetupWithVerification(
                call => call.Send(It.Is<string>(str => str == "foo@bar.com"),
                    It.Is<string>(body => body == htmlMockbodyHtml),
                    It.Is<string>(subject => subject == "Event booking for " + mockAd.Heading),
                    It.IsAny<MailAttachment[]>()));

            var target = BuildTargetObject();

            target.SendTicketBuyerEmail("foo@bar.com", mockAd, mockEventBooking);
        }

        [Test]
        public void SendWelcomeEmail_CallsMailSender()
        {
            _urlMock.SetupWithVerification(call => call.Home(), "/home");

            _clientConfig.SetupWithVerification(call => call.ClientName, "client123");

            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<WelcomeEmail>(),
                "~/Views/Email/Welcome.cshtml"), "<body>hello</body>");

            _mailSender.SetupWithVerification(call => call.Send(
                It.Is<string>(str => str == "foo@bar.com"),
                It.IsAny<string>(),
                It.IsAny<string>()));

            BuildTargetObject().SendWelcomeEmail("foo@bar.com", "user123");
        }

        [Test]
        public void SendForgotPasswordEmail_CallsMailSender()
        {
            _urlMock.SetupWithVerification(call => call.Login(), "/home");

            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<ForgotPasswordEmail>(),
                "~/Views/Email/ForgotPassword.cshtml"), "<body>hello</body>");

            _mailSender.SetupWithVerification(call => call.Send(
                It.Is<string>(str => str == "foo@bar.com"),
                It.IsAny<string>(),
                It.IsAny<string>()));

            BuildTargetObject().SendForgotPasswordEmail("foo@bar.com", "password123");
        }

        [Test]
        public void SendTicketTransfer_CallsMailSender()
        {
            var mockAd = new AdSearchResultMockBuilder().Default().Build();

            var mockBody = "<body>hello</body>";

            var expectedSubject = $"Tickets transfer for event {mockAd.Heading}";

            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<TicketTransferEmail>(),
                "~/Views/Email/EventTicketTransfer.cshtml"), mockBody);

            _urlMock.SetupWithVerification(call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "http://eventurl");

            _mailSender.SetupWithVerification(call => call.Send(
                It.Is<string>(str => str == "foo@bar.com"),
                It.Is<string>(str => str == mockBody),
                It.Is<string>(str => str == expectedSubject)
                ));

            BuildTargetObject().SendTicketTransfer(mockAd, "foo@bar.com", "bar@foo.com");
        }

        [Test]
        public void SendSendGuestRemoval_CallsMailSender()
        {
            var mockAd = new AdSearchResultMockBuilder().Default().Build();
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventBookingTicket = new EventBookingTicketMockBuilder().Default().Build();

            var mockBody = "<body>guest removed</body>";

            var expectedSubject = $"Ticket Cancelled for {mockAd.Heading}";

            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<EventGuestRemovedEmail>(),
                "~/Views/Email/EventGuestRemoved.cshtml"), mockBody);

            _urlMock.SetupWithVerification(call => call.EventUrl(It.IsAny<string>(), It.IsAny<int>()), "http://eventurl");

            _mailSender.SetupWithVerification(call => call.Send(
                It.Is<string>(str => str == "foo@bar.com"),
                It.Is<string>(str => str == mockBody),
                It.Is<string>(str => str == expectedSubject)
                ));

            BuildTargetObject().SendGuestRemoval(mockAd, mockEvent, mockEventBookingTicket);
        }

        [Test]
        public void SendEventOrganiserIdentityApproval_CallsMailSender()
        {
            var expectedSubject = "Event organiser identity";
            var mockUser = new ApplicationUserMockBuilder().Default().Build();


            var mockBody = "<body></body>";
            _templatingService.SetupWithVerification(call =>
                call.Generate(It.IsAny<EventOrganiserIdentityConfirmationEmail>(),
                    It.Is<string>(t => t == "~/Views/Email/EventOrganiserIdentityConfirmation.cshtml")),
                    mockBody);


            _clientConfig.SetupWithVerification(call => call.SupportEmailList, new[] { "foo@bar.com" });
            _userManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);

            _mailSender.SetupWithVerification(call => call.Send(
               It.Is<string>(str => str == "foo@bar.com"),
               It.Is<string>(str => str == mockBody),
               It.Is<string>(str => str == expectedSubject),
               It.IsAny<MailAttachment[]>()
               ));


            var attachments = new List<MailAttachment>
            {
                new MailAttachment()
            };

            BuildTargetObject().SendEventOrganiserIdentityConfirmation(attachments);
        }

        [Test]
        public void SendRegistrationConfirmationEmail_CallsMailSender()
        {
            _mailSender.SetupWithVerification(call =>
                call.Send(It.Is<string>(str => str == "foo@bar.com"),
                    It.Is<string>(str => str == "<fake-body></fake-body>"),
                    It.Is<string>(str => str == "Confirmation Code")));


            _templatingService.SetupWithVerification(call => call.Generate(
                It.IsAny<RegisrationConfirmationEmail>(),
                It.Is<string>(t => t == "~/Views/Email/RegistrationConfirmation.cshtml")),
                result: "<fake-body></fake-body>"
                );


            BuildTargetObject()
                .SendRegistrationConfirmationEmail("foo@bar.com", "token123");
        }

        [Test]
        public void SendListingCompleteEmail_CallsMailSender()
        {
            var mockBookingCart = new Mock<IBookingCart>();
            mockBookingCart.Setup(call => call.UserId).Returns("user123");
            mockBookingCart.Setup(call => call.CategoryAdType).Returns("BoomBam");
            var mockSupportEmail = "fake@support.com";

            var mockOnlineAd = new OnlineAdModel
            {
                Heading = "Fake Listing"
            };

            mockBookingCart.Setup(call => call.OnlineAdModel).Returns(mockOnlineAd);

            var applicationUser = new ApplicationUserMockBuilder().Default().Build();

            _mailSender.Setup(call =>
               call.Send(It.Is<string>(str => str == applicationUser.Email),
                   It.Is<string>(str => str == "<fake-body></fake-body>"),
                   It.Is<string>(str => str == "Listing placed")));
            
            
            _mailSender.Setup(call =>
               call.Send(It.Is<string>(str => str == mockSupportEmail),
                   It.Is<string>(str => str == "<fake-body></fake-body>"),
                   It.Is<string>(str => str == "Listing placed")));


            _templatingService.SetupWithVerification(call => call.Generate(
                It.IsAny<ListingCompleteEmail>(),
                It.Is<string>(t => t == "~/Views/Email/ListingCompleteView.cshtml")),
                result: "<fake-body></fake-body>");

            _urlMock.SetupWithVerification(call => call.AdUrl(
                It.Is<string>(heading => heading == mockOnlineAd.Heading),
                It.Is<int>(id => id == 123),
                It.Is<string>(adType => adType == mockBookingCart.Object.CategoryAdType)),
                result: "http://fake-url");

            _clientConfig.SetupWithVerification(call => call.SupportEmailList, new[] { mockSupportEmail });

            BuildTargetObject()
                .SendListingCompleteEmail(applicationUser, 123, mockBookingCart.Object);
        }

        [Test]
        public void SendListingEnquiryEmail_CallsMailSender()
        {
            var mockAdEnquiry = new AdEnquiryViewModel
            {
                AdId = 1,
                Email = "hello@email.com",
                Question = "whoa dude!"
            };

            var mockAd = new AdSearchResultMockBuilder().Default().Build();
            var applicationUser = new ApplicationUserMockBuilder().Default().Build();

            _userManager.SetupWithVerification(call => call.GetUserByUsername(
                It.Is<string>(username => username == mockAd.Username)),
                applicationUser);

            _mailSender.Setup(call =>
               call.Send(It.Is<string>(str => str == applicationUser.Email),
                   It.Is<string>(str => str == "<fake-body></fake-body>"),
                   It.Is<string>(str => str == "Support required for " + mockAd.Heading)));


            _urlMock.SetupWithVerification(call =>
                call.AdUrl(It.Is<string>(heading => heading == mockAd.Heading),
                    It.Is<int>(id => id == mockAd.AdId),
                    It.Is<string>(adType => adType == mockAd.CategoryAdType)),
                    result: "http://adUrl.com.au");

            _searchService.SetupWithVerification(call =>
                call.GetByAdId(It.Is<int>(adId => adId == mockAdEnquiry.AdId)),
                result: mockAd);

            _templatingService.SetupWithVerification(call => call.Generate(
                It.Is<AdEnquiryViewModel>(vm => vm == mockAdEnquiry),
                It.Is<string>(t => t == "~/Views/Email/ContactAdvertiser.cshtml")),
                result: "<fake-body></fake-body>");
            

            BuildTargetObject()
                .SendListingEnquiryEmail(mockAdEnquiry);
        }
    }
}
