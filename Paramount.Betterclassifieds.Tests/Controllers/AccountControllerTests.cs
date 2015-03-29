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
        public void Login_Get_UserAlreadyLoggedIn_RedirectsToHome()
        {
            // arrange            
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), true);
            var mockUser = CreateMockOf<IPrincipal>();

            // act
            var controller = CreateController(mockUser: mockUser);
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
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            var mockUser = CreateMockOf<IPrincipal>();

            // act
            var ctrl = CreateController(mockUser: mockUser);
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
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), true);
            var mockUser = CreateMockOf<IPrincipal>();
            var loginViewModel = CreateMockOf<LoginViewModel>();

            // act
            var ctrl = CreateController(mockUser: mockUser);
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
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), null);
            var mockUser = CreateMockOf<IPrincipal>();
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: mockUser);
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
            mockApplicationUser
                .SetupWithVerification(call => call.AuthenticateUser(mockAuthMgr.Object, It.IsAny<string>(), It.IsAny<bool>()), false);
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), mockApplicationUser.Object);
            var mockUser = CreateMockOf<IPrincipal>();
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: mockUser);
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
            mockApplicationUser
                .SetupWithVerification(call => call.AuthenticateUser(mockAuthMgr.Object, It.IsAny<string>(), It.IsAny<bool>()), true);
            mockAuthMgr.SetupWithVerification(call => call.IsUserIdentityLoggedIn(It.IsAny<IPrincipal>()), false);
            mockUserMgr.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.Is<string>(s => s == "fakeUser")), mockApplicationUser.Object);
            var mockUser = CreateMockOf<IPrincipal>();
            var mockLoginViewModel = new LoginViewModel { Username = "fakeUser" };

            // act
            var ctrl = CreateController(mockUser: mockUser, mockTempData: mockTempData);
            var result = ctrl.Login(mockLoginViewModel);

            // assert - that we have an error
            result.IsTypeOf<RedirectResult>();
            var redirectResult = ((RedirectResult)result);
            redirectResult.Url.IsEqualTo("/fakeReturnUrl");
        }

        //[Test]
        //public void Details_Get_RetrievesUserProfileDetails_ReturnsViewModel_ForEdit()
        //{

        //    mockUserMgr.Setup(call => call.GetCurrentUser(It.IsAny<IPrincipal>()))
        //        .Returns(new ApplicationUser
        //        {
        //            FirstName = "Aaron",
        //            LastName = "Ramsey",
        //            City = "Melbourne",
        //            Username = "aramsay",
        //            AddressLine1 = "1 Flinders Street",
        //            AddressLine2 = "Vic",
        //            State = "VIC",
        //            Email = "ramsey@arsenal.com",
        //            Phone = "04333333333",
        //            Postcode = "3000"
        //        });

        //    var controller = this.CreateController();

        //    var result = controller.Details();


        //}
    }
}