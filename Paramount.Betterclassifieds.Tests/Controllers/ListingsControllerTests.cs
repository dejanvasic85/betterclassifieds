using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class ListingsControllerTests : ControllerTest<ListingsController>
    {
        private Mock<IMailService> _mailService;
        private Mock<IClientConfig> _mockClientConfig;
        private Mock<ISearchService> _mockSearchService;
        private Mock<IBookingManager> _mockBookingManager;
        private Mock<IApplicationConfig> _mockAppConfig;
        private Mock<IRobotVerifier> _mockRobotVerifier;
        private Mock<IAuthManager> _mockAuthManager;
        private Mock<IUserManager> _mockUserManager;

        [Test]
        public void AdEnquiry_Post_NoLoggedInUser_NameEmpty_ReturnsModelErrors()
        {
            var enquiryMock = new AdEnquiryViewModel
            {
                AdId = 1,
                FullName = string.Empty,
                Email = "foo@bar.com",
                Question = "hello sir",
                Phone = "0493493939"
            };

            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), null);

            var controller = BuildController();

            var result = controller.AdEnquiry(enquiryMock, string.Empty);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        [Test]
        public void AdEnquiry_Post_NoLoggedInUser_EmailEmpty_ReturnsModelErrors()
        {
            var enquiryMock = new AdEnquiryViewModel
            {
                AdId = 1,
                FullName = "Foo bar",
                Email = string.Empty,
                Question = "hello sir",
                Phone = "0493493939"
            };

            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), null);

            var controller = BuildController();

            var result = controller.AdEnquiry(enquiryMock, string.Empty);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        [Test]
        public void AdEnquiry_Post_CaptchaFailsValidation_ReturnsModelErrors()
        {
            var enquiryMock = new AdEnquiryViewModel
            {
                AdId = 1,
                FullName = "Foo bar",
                Email = "foo@bar.com",
                Question = "hello sir",
                Phone = "0493493939"
            };
            var captchaResponse = "123";

            _mockAppConfig.SetupWithVerification(call => call.GoogleAdEnquiryCatpcha, new RecaptchaConfig("key", "secret"));
            _mockRobotVerifier.SetupWithVerification(call => call.IsValid(It.Is<string>(s => s == "secret"), It.Is<string>(s => s == captchaResponse)), false);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), null);

            var controller = BuildController();

            var result = controller.AdEnquiry(enquiryMock, captchaResponse);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        [Test]
        public void AdEnquiry_Post_UserNotLoggedIn_SendsEnquiry()
        {
            var enquiryMock = new AdEnquiryViewModel
            {
                AdId = 1,
                FullName = "Foo bar",
                Email = "foo@bar.com",
                Question = "hello sir",
                Phone = "0493493939"
            };
            var captchaResponse = "123";

            _mockAppConfig.SetupWithVerification(call => call.GoogleAdEnquiryCatpcha, new RecaptchaConfig("key", "secret"));
            _mockRobotVerifier.SetupWithVerification(call => call.IsValid(It.Is<string>(s => s == "secret"), It.Is<string>(s => s == captchaResponse)), true);
            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), null);
            _mockBookingManager.SetupWithVerification(call => call.SubmitAdEnquiry(It.IsAny<AdEnquiry>()));
            _mailService.SetupWithVerification(call => call.SendListingEnquiryEmail(It.Is<AdEnquiryViewModel>(a => a == enquiryMock)));

            var controller = BuildController();

            var result = controller.AdEnquiry(enquiryMock, captchaResponse);
            result.IsTypeOf<JsonResult>();
        }

        [Test]
        public void AdEnquiry_Post_LoggedInUser_CopiesUserDetails_SendsEnquiry()
        {
            var mockEnquiry = new AdEnquiryViewModel
            {
                AdId = 1,
                FullName = string.Empty,
                Email = string.Empty,
                Question = "hello sir",
                Phone = "0493493939"
            };
            var captchaResponse = "123";
            var mockUser = new ApplicationUserMockBuilder().Default().Build();

            _mockUserManager.SetupWithVerification(call => call.GetCurrentUser(), mockUser);
            _mockBookingManager.SetupWithVerification(call => call.SubmitAdEnquiry(It.IsAny<AdEnquiry>()));
            _mailService.SetupWithVerification(call => call.SendListingEnquiryEmail(It.Is<AdEnquiryViewModel>(a => a == mockEnquiry)));

            var controller = BuildController();

            var result = controller.AdEnquiry(mockEnquiry, captchaResponse);
            result.IsTypeOf<JsonResult>();
            mockEnquiry.FullName = mockUser.FullName;
            mockEnquiry.Email = mockUser.Email;
        }


        [SetUp]
        public void SetupDependencies()
        {
            _mailService = CreateMockOf<IMailService>();
            _mailService.Setup(call => call.Initialise(It.IsAny<Controller>())).Returns(_mailService.Object);
            _mockClientConfig = CreateMockOf<IClientConfig>();
            _mockSearchService = CreateMockOf<ISearchService>();
            _mockBookingManager = CreateMockOf<IBookingManager>();
            _mockAppConfig = CreateMockOf<IApplicationConfig>();
            _mockRobotVerifier = CreateMockOf<IRobotVerifier>();
            _mockAuthManager = CreateMockOf<IAuthManager>();
            _mockUserManager = CreateMockOf<IUserManager>();
        }
    }
}