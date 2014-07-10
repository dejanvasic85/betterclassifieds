using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Tests.Mocks;

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
        [Ignore]
        public void ContactUs_ReadsContactAndMapFromService_ReturnsContactModel()
        {
            // Arrange
            CreateMockOf<ISearchService>();
            CreateMockOf<IClientConfig>();

            // Act
            var result = CreateController().ContactUs();

            
            // Assert

        }
    }
}
