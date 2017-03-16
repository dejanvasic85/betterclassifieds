using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;
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
    }
}
