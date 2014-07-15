using Moq;
using NUnit.Framework;
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
            var viewResult = (ViewResult) result;
            var homeModel = viewResult.Model;
            homeModel.IsTypeOf<HomeModel>();
            ((HomeModel) homeModel).AdSummaryList.Count.IsEqualTo(2);
        }

        [Test]
        public void ContactUs_Get_ReturnsContactModelAndAddress()
        {
            // Arrange
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
            var viewResult = (ViewResult) result;
            var model = viewResult.Model;
            model.IsTypeOf<ContactUsModel>();
            var address = viewResult.ViewBag.Address as AddressViewModel;
            Assert.IsNotNull(address);
            address.AddressLine1.IsEqualTo("Company co...");
            address.Country.IsEqualTo("Australia");
        }
    }
}
