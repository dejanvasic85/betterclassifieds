using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Tests.Mocks;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.Services.Seo;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    public class HomeControllerTests : ControllerTest<HomeController>
    {
        [Test]
        public void Index_CallsSearchServiceForLatestSix_ReturnsSummary()
        {
            // Arrange
            _mockSearchService
                .SetupWithVerification(call => call.GetLatestAds(It.Is<int>(a => a == 9)),
                new List<AdSearchResult>
                {
                    new AdSearchResult{AdId = 1, CategoryId = 1, CategoryName = "MockCategory", Heading = "Ad 1", Description = "Description 1"},
                    new AdSearchResult{AdId = 2, CategoryId = 1, CategoryName = "MockCategory", Heading = "Ad 2", Description = "Description 2"}
                });

            // Act
            var result = BuildController().Index();

            // Assert
            result.IsTypeOf<ViewResult>();
            var viewResult = (ViewResult)result;
            var homeModel = viewResult.Model;
            homeModel.IsTypeOf<HomeModel>();
            ((HomeModel)homeModel).AdSummaryList.Count.IsEqualTo(2);
        }

        [Test]
        public void ContactUs_Get_ReturnsContactModelAndAddress()
        {
            // Arrange
            _mockClientConfig.SetupGet(prop => prop.ClientAddress)
             .Returns(new Address
             {
                 StreetNumber = "Company co...",
                 StreetName = "123 Smith Street",
                 Country = "Australia",
                 Postcode = "1111",
                 State = "VIC",
                 Suburb = "Melbourne"
             });
            _mockClientConfig.SetupGet(prop => prop.ClientPhoneNumber).Returns("03999999");
            _mockClientConfig.SetupGet(prop => prop.ClientAddressLatLong).Returns(new Tuple<string, string>("1", "2"));
            _mockAppConfig.SetupWithVerification(call => call.GoogleCaptchaEnabled, result: true);
            _mockAppConfig.SetupWithVerification(call => call.GoogleGeneralEnquiryCatpcha, new RecaptchaConfig("key", "secret"));


            // Act
            var result = BuildController().ContactUs();

            // Assert
            result.IsTypeOf<ViewResult>();

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewBag.Address, Is.Not.Null);
            Assert.That(viewResult.ViewBag.PhoneNumber, Is.Not.Null);
            Assert.That(viewResult.ViewBag.AddressLatitude, Is.Not.Null);
            Assert.That(viewResult.ViewBag.AddressLongitude, Is.Not.Null);

            var vm = viewResult.ViewResultModelIsTypeOf<ContactUsView>();
            vm.GoogleCaptchaEnabled.IsTrue();
            vm.GoogleCaptchaKey.IsEqualTo("key");
        }

        [Test]
        public void ContactUs_Post_SendsEmailAndCallsManager_ReturnsJsonResult()
        {
            var mockModel = new ContactUsView
            {
                FullName = "George C",
                Comment = "I love masterchef",
                Email = "george@masterchef.com",
                Phone = "555 498"
            };
            
            _mockAppConfig.SetupWithVerification(call => call.GoogleGeneralEnquiryCatpcha, new RecaptchaConfig("key", "secret"));
            
            _mailService.SetupWithVerification(call => call.SendSupportEmail(It.Is<ContactUsView>(view => view == mockModel)));
            _mockRobotVerifier.SetupWithVerification(
                call => call.IsValid(It.Is<string>(s => s == "secret"), It.IsAny<HttpRequestBase>()), true);

            // Act
            var controller = BuildController();

            // Assert
            var result = controller.ContactUs(mockModel);
            result.IsTypeOf<JsonResult>();
        }

        [Test]
        public void ContactUs_Post_RobotCheckFails_ReturnsJsonModelErrors()
        {
            var mockModel = new ContactUsView
            {
                FullName = "George C",
                Comment = "I love masterchef",
                Email = "george@masterchef.com",
                Phone = "555 498"
            };

            _mockAppConfig.SetupWithVerification(call => call.GoogleGeneralEnquiryCatpcha, new RecaptchaConfig("key", "secret"));
            _mockRobotVerifier.SetupWithVerification(
                call => call.IsValid(It.Is<string>(s => s == "secret"), It.IsAny<HttpRequestBase>()), false);

            // Act
            var controller = BuildController();

            // Assert
            var result = controller.ContactUs(mockModel);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        private Mock<IMailService> _mailService;
        private Mock<IClientConfig> _mockClientConfig;
        private Mock<ISearchService> _mockSearchService;
        private Mock<ISitemapFactory> _mockSitemapFactory;
        private Mock<IApplicationConfig> _mockAppConfig;
        private Mock<IRobotVerifier> _mockRobotVerifier;

        [SetUp]
        public void SetupDependencies()
        {
            _mailService = CreateMockOf<IMailService>();
            _mailService.Setup(call => call.Initialise(It.IsAny<Controller>())).Returns(_mailService.Object);
            _mockClientConfig = CreateMockOf<IClientConfig>();
            _mockSearchService = CreateMockOf<ISearchService>();
            _mockSitemapFactory = CreateMockOf<ISitemapFactory>();
            _mockAppConfig = CreateMockOf<IApplicationConfig>();
            _mockRobotVerifier = CreateMockOf<IRobotVerifier>();
        }
    }
}
