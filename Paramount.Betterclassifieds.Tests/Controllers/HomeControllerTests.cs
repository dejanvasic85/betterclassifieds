using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Tests.Mocks;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    public class HomeControllerTests : ControllerTest<HomeController>
    {
        [Test]
        public void Index_CallsSearchServiceForLatestTen_ReturnsSummary()
        {
            // Arrange
            CreateMockOf<IBroadcastManager>();
            CreateMockOf<IEnquiryManager>();
            CreateMockOf<IClientConfig>();
            CreateMockOf<ISearchService>()
                .SetupWithVerification(call => call.GetLatestAds(It.Is<int>(a => a == 10)),
                new List<AdSearchResult>
                {
                    new AdSearchResult{AdId = 1, CategoryId = 1, CategoryName = "MockCategory", Heading = "Ad 1", Description = "Description 1"},
                    new AdSearchResult{AdId = 2, CategoryId = 1, CategoryName = "MockCategory", Heading = "Ad 2", Description = "Description 2"}
                });

            // Act
            var result = CreateController().Index();

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
            CreateMockOf<IBroadcastManager>();
            CreateMockOf<IEnquiryManager>();
            CreateMockOf<ISearchService>();
            CreateMockOf<IClientConfig>()
                .SetupGet(prop => prop.ClientAddress)
                .Returns(new Address
                {
                    AddressLine1 = "Company co...",
                    AddressLine2 = "123 Smith Street",
                    Country = "Australia",
                    Postcode = "1111",
                    State = "VIC",
                    Suburb = "Melbourne"
                });

            // Act
            var result = CreateController().ContactUs();

            // Assert
            result.IsTypeOf<ViewResult>();
            var viewResult = (ViewResult)result;
            var model = viewResult.Model;
            model.IsTypeOf<ContactUsModel>();
            var address = viewResult.ViewBag.Address as AddressViewModel;
            Assert.IsNotNull(address);
            address.AddressLine1.IsEqualTo("Company co...");
            address.Country.IsEqualTo("Australia");
        }

        [Test]
        public void ContactUs_Post_SendsEmailAndCallsManager_ReturnsJsonResult()
        {
            // Arrange
            var supportEmails = new[] { "fake@email.com" };
            var mockModel = new ContactUsModel
            {
                FullName = "George C",
                Comment = "I love masterchef",
                Email = "george@masterchef.com",
                Phone = "555 498"
            };

            CreateMockOf<ISearchService>();
            CreateMockOf<IBroadcastManager>()
                .SetupWithVerification(call => call.SendEmail(
                    It.IsAny<SupportRequest>(),
                    It.Is<string[]>(s => s == supportEmails)),
                    result: It.IsAny<Guid>());

            CreateMockOf<IEnquiryManager>()
                .SetupWithVerification(call => call.CreateSupportEnquiry(
                    It.Is<string>(s => s == mockModel.FullName),
                    It.Is<string>(s => s == mockModel.Email),
                    It.Is<string>(s => s == mockModel.Phone),
                    It.Is<string>(s => s == mockModel.Comment),
                    It.IsAny<string>()));

            CreateMockOf<IClientConfig>()
                .SetupGet(prop => prop.SupportEmailList).Returns(supportEmails).Verifiable();

            // Act
            var controller = CreateController();

            // Assert
            var result = controller.ContactUs(mockModel);
            result.IsTypeOf<JsonResult>();
        }
    }

    [TestFixture]
    public class AccountControllerTests : ControllerTest<AccountController>
    {
        private Mock<IUserManager> mockUserMgr;
        private Mock<IAuthManager> mockAuthMgr;
        private Mock<IBroadcastManager> mockBroadcastMgr;
        private Mock<ISearchService> searchServiceMgr;

        [SetUp]
        public void SetupCotroller()
        {
            mockUserMgr = CreateMockOf<IUserManager>();
            mockAuthMgr = CreateMockOf<IAuthManager>();
            mockBroadcastMgr = CreateMockOf<IBroadcastManager>();
            searchServiceMgr = CreateMockOf<ISearchService>();
        }

        [Test]
        public void Login_UserAlreadyLoggedIn_RedirectsToHome()
        {
            // arrange            
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), true);
            var mockUser = CreateMockOf<IPrincipal>();

            // act
            var controller = CreateController(mockUser: mockUser);
            var result = controller.Login(string.Empty);

            // assert
            result.IsTypeOf<RedirectToRouteResult>();
            var redirectResult = (RedirectToRouteResult) result;
            redirectResult.RouteValues.ElementAt(0).Value.IsEqualTo("Index");
            redirectResult.RouteValues.ElementAt(1).Value.IsEqualTo("Home");
        }

        [Test]
        public void Login_UserNeedsToLoginOrRegister_ReturnsLoginOrRegisterModel()
        {
            // arrange
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            var mockUser = CreateMockOf<IPrincipal>();
            
            // act
            var ctrl = CreateController(mockUser: mockUser);
            var result = ctrl.Login("/fakeReturnUrl");

            // assert
            ctrl.TempData.ContainsKey(AccountController.ReturnUrlKey);
            ctrl.TempData.ContainsValue("/fakeReturnUrl");
            result.IsTypeOf<ViewResult>();
            var viewResult = ((ViewResult) result);
            viewResult.Model.IsTypeOf<LoginOrRegisterModel>();
        }
    }
}
