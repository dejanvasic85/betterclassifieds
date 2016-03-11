using System.Security.Principal;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public abstract class ControllerTest<T>
        where T : Controller
    {
        private MockRepository _mockRepository;
        private List<Action> _verifyList;
        private IUnityContainer _containerBuilder;

        [SetUp]
        public void Initialise()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _containerBuilder = new UnityContainer();
        }

        [TearDown]
        public void Cleanup()
        {
            _verifyList.ForEach(action => action());
            _verifyList.Clear();
        }

        /// <summary>
        /// Creates a controller with a mock of the HttpContextBase with optional base url and route information (for testing routes)
        /// </summary>
        protected T BuildController(string mockUrl = "~/MockUrl/", RouteData routeData = null,
            RouteCollection routes = null, TempDataDictionary mockTempData = null, Mock<IPrincipal> mockUser = null)
        {
            _containerBuilder.RegisterType(typeof(T));

            var controller = _containerBuilder.Resolve<T>();

            // Mock all the required http context stuff ( really helpful for resolving Http crap like Routes )
            var mockHttpContext = CreateHttpContextBase(mockUrl, mockUser);
            if (routeData == null)
                routeData = new RouteData();

            if (routes == null)
            {
                routes = new RouteCollection();
                Paramount.Betterclassifieds.Presentation.RouteConfig.RegisterRoutes(routes);
            }
            
            var mockRequestContext = new RequestContext(mockHttpContext.Object, routeData);
            var mockControllerContext = new ControllerContext(mockRequestContext, controller);

            controller.Url = new UrlHelper(mockRequestContext, routes);
            controller.ControllerContext = mockControllerContext;
            controller.TempData = mockTempData;

            return controller;
        }

        protected MockRepository MockRepository
        {
            get { return this._mockRepository; }
        }

        protected List<Action> VerifyList
        {
            get { return this._verifyList; }
        }

        protected IUnityContainer ContainerBuilder
        {
            get { return this._containerBuilder; }
        }

        protected Mock<TMock> CreateMockOf<TMock>() where TMock : class
        {
            return _mockRepository.CreateMockOf<TMock>(_containerBuilder, _verifyList);
        }

        private Mock<HttpContextBase> CreateHttpContextBase(string url, Mock<IPrincipal> mockUser = null)
        {
            var context = CreateMockOf<HttpContextBase>();
            var request = CreateMockOf<HttpRequestBase>();
            var response = CreateMockOf<HttpResponseBase>();
            var session = CreateMockOf<HttpSessionStateBase>();
            var server = CreateMockOf<HttpServerUtilityBase>();

            request.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns("/");
            request.Setup(r => r.ApplicationPath).Returns("/");
            request.Setup(r => r.QueryString).Returns(GetQueryStringParameters(url));
            request.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns(GetUrlFileName(url));
            request.Setup(r => r.PathInfo).Returns(string.Empty);
            request.Setup(r => r.Url).Returns(new Uri("http://dummy-localhost"));
            request.Setup(r => r.ServerVariables).Returns(new NameValueCollection());

            response.Setup(s => s.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            response.SetupProperty(res => res.StatusCode, (int)System.Net.HttpStatusCode.OK);

            context.Setup(h => h.Request).Returns(request.Object);
            context.Setup(h => h.Response).Returns(response.Object);
            context.Setup(call => call.GetService(typeof(HttpWorkerRequest)))
                .Returns(null);

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            if (mockUser != null)
                context.Setup(call => call.User).Returns(mockUser.Object);

            return context;
        }

        private string GetUrlFileName(string url)
        {
            return (url.Contains("?"))
               ? url.Substring(0, url.IndexOf("?"))
               : url;
        }

        private NameValueCollection GetQueryStringParameters(string url)
        {
            if (!url.Contains("?"))
                return null;

            var parameters = new NameValueCollection();

            var parts = url.Split("?".ToCharArray());
            var keys = parts[1].Split("&".ToCharArray());

            foreach (var key in keys)
            {
                var part = key.Split("=".ToCharArray());
                parameters.Add(part[0], part[1]);
            }

            return parameters;
        }
    }
}
