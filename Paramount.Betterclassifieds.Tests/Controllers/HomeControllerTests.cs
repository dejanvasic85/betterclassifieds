using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
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
            CreateMockOf<IBroadcastManager>();
            CreateMockOf<IEnquiryManager>();
            CreateMockOf<ISearchService>();
            var clientConfigMock = CreateMockOf<IClientConfig>();
            clientConfigMock.SetupGet(prop => prop.ClientAddress)
            .Returns(new Address
            {
                AddressLine1 = "Company co...",
                AddressLine2 = "123 Smith Street",
                Country = "Australia",
                Postcode = "1111",
                State = "VIC",
                Suburb = "Melbourne"
            });
            clientConfigMock.SetupGet(prop => prop.ClientPhoneNumber).Returns("03999999");
            clientConfigMock.SetupGet(prop => prop.ClientAddressLatLong).Returns(new Tuple<string, string>("1","2"));


            // Act
            var result = BuildController().ContactUs();

            // Assert
            result.IsTypeOf<ViewResult>();
            
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.TypeOf<ContactUsView>());
            Assert.That(viewResult.ViewBag.Address, Is.Not.Null);
            Assert.That(viewResult.ViewBag.PhoneNumber, Is.Not.Null);
            Assert.That(viewResult.ViewBag.AddressLatitude, Is.Not.Null);
            Assert.That(viewResult.ViewBag.AddressLongitude, Is.Not.Null);
        }

        [Test]
        public void ContactUs_Post_SendsEmailAndCallsManager_ReturnsJsonResult()
        {
            // Arrange
            var supportEmails = new[] { "fake@email.com" };
            var mockModel = new ContactUsView
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
            var controller = BuildController();

            // Assert
            var result = controller.ContactUs(mockModel);
            result.IsTypeOf<JsonResult>();
        }
    }
}
