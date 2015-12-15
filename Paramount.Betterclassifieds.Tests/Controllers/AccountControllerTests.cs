using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests : ControllerTest<AccountController>
    {
        [Test]
        public void Login_Get_UserAlreadyLoggedIn_RedirectsToHome()
        {
            // arrange            
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), true);

            // act
            var controller = CreateController(mockUser: _mockLoggedInUser);
            var result = controller.Login(string.Empty);

            // assert
            result.IsTypeOf<RedirectToRouteResult>();
            var redirectResult = (RedirectToRouteResult)result;
            redirectResult.RouteValues.ElementAt(0).Value.IsEqualTo("Index");
            redirectResult.RouteValues.ElementAt(1).Value.IsEqualTo("Home");
        }

        [Test]
        public void Login_Get_UserNeedsToLoginOrRegister_ReturnsLoginOrRegisterModel()
        {
            // arrange
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
           
            // act
            var ctrl = CreateController(mockUser: _mockLoggedInUser);
            var result = ctrl.Login("/fakeReturnUrl");

            // assert
            ctrl.TempData.ContainsKey(AccountController.ReturnUrlKey);
            ctrl.TempData.ContainsValue("/fakeReturnUrl");
            result.IsTypeOf<ViewResult>();
            var viewResult = ((ViewResult)result);
            viewResult.Model.IsTypeOf<LoginOrRegisterModel>();
        }

        [Test]
        public void Login_Post_UserAlreadyLoggedIn_ReturnsModelWithError()
        {
            // arrange
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), true);
            var loginViewModel = CreateMockOf<LoginViewModel>();

            // act
            var ctrl = CreateController(mockUser: _mockLoggedInUser);
            var result = ctrl.Login(loginViewModel.Object);

            // assert - that we have an error
            result.IsTypeOf<ViewResult>();
            ctrl.ModelState.Count.IsEqualTo(1);
            ctrl.ModelState.ElementAt(0).Key.IsEqualTo("AlreadyLoggedIn");
        }

        [Test]
        public void Login_Post_UserDoesNotExist_ReturnsModelWithError()
        {
            // arrange
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            _mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), null);
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: _mockLoggedInUser);
            var result = ctrl.Login(mockLoginViewModel);

            // assert - that we have an error
            result.IsTypeOf<ViewResult>();
            ctrl.ModelState.Count.IsEqualTo(1);
            ctrl.ModelState.ElementAt(0).Key.IsEqualTo("EmailNotValid");
        }

        [Test]
        public void Login_Post_UnableToAuthenticate_ReturnsModelWithError()
        {
            // arrange
            var mockApplicationUser = CreateMockOf<ApplicationUser>();
            mockApplicationUser.SetupWithVerification(call => call.AuthenticateUser(_mockAuthMgr.Object, It.IsAny<string>(), It.IsAny<bool>()), false);
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            _mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), mockApplicationUser.Object);
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: _mockLoggedInUser);
            var result = ctrl.Login(mockLoginViewModel);

            // assert - that we have an error
            result.IsTypeOf<ViewResult>();
            ctrl.ModelState.Count.IsEqualTo(1);
            ctrl.ModelState.ElementAt(0).Key.IsEqualTo("BadPassword");
        }

        [Test]
        public void Login_Post_CorrectPassword_RedirectToReturnUrlInTempData()
        {
            // arrange
            var mockTempData = new TempDataDictionary {
                {
                    AccountController.ReturnUrlKey, "/fakeReturnUrl"
                }};
            var mockApplicationUser = CreateMockOf<ApplicationUser>();
            mockApplicationUser.SetupWithVerification(call => call.AuthenticateUser(_mockAuthMgr.Object, It.IsAny<string>(), It.IsAny<bool>()), true);
            _mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            _mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), mockApplicationUser.Object);
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: _mockLoggedInUser, mockTempData: mockTempData);
            var result = ctrl.Login(mockLoginViewModel);

            // assert - that we have an error
            result.IsTypeOf<RedirectResult>();
            var redirectResult = ((RedirectResult)result);
            redirectResult.Url.IsEqualTo("/fakeReturnUrl");
        }

        [Test]
        public void Details_Get_RetrievesUserProfileDetails_ReturnsViewModel_ForEdit()
        {
            // Arrange
            var mockUserProfile = new ApplicationUserMockBuilder().Default().Build();
            _mockUserMgr.SetupWithVerification(call => call.GetCurrentUser(It.IsAny<IPrincipal>()), result: mockUserProfile);

            // Act
            var controller = this.CreateController(mockUser: _mockLoggedInUser);
            var result = controller.Details();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewBag.Updated, Is.Not.Null);
            Assert.That(viewResult.ViewBag.Updated, Is.False);
        }

        private Mock<IUserManager> _mockUserMgr;
        private Mock<IAuthManager> _mockAuthMgr;
        private Mock<IBroadcastManager> _mockBroadcastMgr;
        private Mock<ISearchService> _searchServiceMgr;
        private Mock<IPrincipal> _mockLoggedInUser;

        [SetUp]
        public void SetupCotroller()
        {
            _mockLoggedInUser = CreateMockOf<IPrincipal>();
            _mockUserMgr = CreateMockOf<IUserManager>();
            _mockAuthMgr = CreateMockOf<IAuthManager>();
            _mockBroadcastMgr = CreateMockOf<IBroadcastManager>();
            _searchServiceMgr = CreateMockOf<ISearchService>();
        }
    }
}