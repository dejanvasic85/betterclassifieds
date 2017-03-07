using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.PresentationServices
{
    [TestFixture]
    public class MailServiceTests : TestContext<MailService>
    {
        private Mock<ITemplatingService> _templatingService;
        private Mock<IUrl> _urlMock;
        private Mock<IUserManager> _userManager;
        private Mock<IMailSender<EmailDetails>> _mailSender;
        private ApplicationUser _mockUser;

        [SetUp]
        public void SetupDependencies()
        {
            _urlMock = CreateMockOf<IUrl>();
            _urlMock.SetupWithVerification(call => call.WithAbsoluteUrl(), _urlMock.Object); // All outgoing links need to be absolute

            _templatingService = CreateMockOf<ITemplatingService>();
            _userManager = CreateMockOf<IUserManager>();
            _mailSender = CreateMockOf<IMailSender<EmailDetails>>();

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
                call => call.Send(It.IsAny<EmailDetails>()));


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

    }
}
