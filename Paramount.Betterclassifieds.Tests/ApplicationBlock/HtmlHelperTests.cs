using System;
using NUnit.Framework;
using Moq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Mvc.HtmlHelpers;

namespace Paramount.Betterclassifieds.Tests.ApplicationBlock
{
    [TestFixture]
    public class HtmlHelperTests
    {
        [Test]
        public void BootstrapTextBoxFor_WithFakeModel_ReturnsMvcString()
        {
            // Arrange
            var fakeModel = new FakeViewModelForHtmlHelperTest { Name = "John Snow" };
            var fakeHelper = CreateFakeModelHelper(new ViewDataDictionary(fakeModel));

            // Action
            var result = fakeHelper.BootstrapTextBoxFor(m => m.Name);

            // Assert
            Assert.IsInstanceOf<MvcHtmlString>(result);
            Assert.IsTrue(result.ToHtmlString().Contains("form-control"));
        }

        [Test]
        public void DropDownListForEnum_WithFakeModel_ReturnsMvcString()
        {
            // Arrange
            var fakeModel = new FakeViewModelForHtmlHelperTest { MyType = MyType.HelloWorld };
            var fakeHelper = CreateFakeModelHelper(new ViewDataDictionary(fakeModel));

            // Action
            var result = fakeHelper.DropDownListForEnum(m => m.MyType, MyType.FooBar);

            // Assert
            Assert.IsInstanceOf<MvcHtmlString>(result);
            Assert.IsTrue(result.ToHtmlString().StartsWith("<select id=\"MyType\" name=\"MyType\""));
        }

        [Test]
        public void DropDownListForEnum_WithFakeModelAndName_ReturnsMvcString()
        {
            // Arrange
            var fakeModel = new FakeViewModelForHtmlHelperTest { MyType = MyType.HelloWorld };
            var fakeHelper = CreateFakeModelHelper(new ViewDataDictionary(fakeModel));

            // Action
            var result = fakeHelper.DropDownListForEnum(m => m.MyType, MyType.FooBar, name: "randomId");

            // Assert
            Assert.IsInstanceOf<MvcHtmlString>(result);
            Assert.IsTrue(result.ToHtmlString().StartsWith("<select Name=\"randomId\" id=\"randomId\""));
        }

        [Test]
        public void DropDownListForEnum_ModelNotAnEnum_ReturnsMvcString()
        {
            // Arrange
            var fakeModel = new FakeViewModelForHtmlHelperTest { MyType = MyType.HelloWorld };
            var fakeHelper = CreateFakeModelHelper(new ViewDataDictionary(fakeModel));

            // Action
            // Assert
            Assert.Throws<ArgumentException>(() => fakeHelper.DropDownListForEnum(m => m.MyType, ""));
        }

        /// <summary>
        /// Helper method for generating a mock HtmlHelper :) SWeet!
        /// </summary>
        private static HtmlHelper<FakeViewModelForHtmlHelperTest> CreateFakeModelHelper(ViewDataDictionary viewData)
        {
            var stubControllerContext = new Mock<ControllerContext>();
            stubControllerContext.Setup(x => x.HttpContext).Returns(new Mock<HttpContextBase>().Object);
            stubControllerContext.Setup(x => x.RouteData).Returns(new RouteData());
            stubControllerContext.Setup(x => x.Controller).Returns(new Mock<ControllerBase>().Object); ;

            var stubViewContext = new Mock<ViewContext>();
            stubViewContext.Setup(x => x.View).Returns(new Mock<IView>().Object);
            stubViewContext.Setup(x => x.ViewData).Returns(viewData);
            stubViewContext.Setup(x => x.TempData).Returns(new TempDataDictionary());

            var mockViewDataContainer = new Mock<IViewDataContainer>();

            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            return new HtmlHelper<FakeViewModelForHtmlHelperTest>(stubViewContext.Object, mockViewDataContainer.Object);
        }

    }

    internal class FakeViewModelForHtmlHelperTest
    {
        public string Name { get; set; }
        public MyType MyType { get; set; }
    }

    internal enum MyType
    {
        FooBar = 0,
        HelloWorld = 1
    }
}
