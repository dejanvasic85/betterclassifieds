using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Tests.Search
{
    /// <summary>
    /// Summary description for AdSearchFilterTests
    /// </summary>
    [TestClass]
    public class SearchFilterTests
    {
        [TestMethod]
        public void GetAdId_KeywordIsText_ReturnsNull()
        {
            // Arrange
            var filter = new SearchFilters {Keyword = "random"};

            // Act
            // Assert
            filter.AdId.IsNull();
        }

        [TestMethod]
        public void GetAdId_KeywordIsNumber_HasValue()
        {
            // Arrange
            var filter = new SearchFilters {Keyword = "848772"};

            // Act
            // Assert
            filter.AdId.IsNotNull();
            filter.AdId.IsEqualTo(848772);
        }

        [TestMethod]
        public void GetAdId_KeywordContainsPublicationPrefix_HasValue()
        {
            // Arrange
            var filter = new SearchFilters { Keyword = "8-3748" };

            // Act
            // Assert
            filter.AdId.IsNotNull();
            filter.AdId.IsEqualTo(3748);
        }

        [TestMethod]
        public void GetAdId_KeywordContainsPublicationPrefixWithMultiDigits_HasValue()
        {
            // Arrange
            var filter = new SearchFilters { Keyword = "1008-3748" };

            // Act
            // Assert
            filter.AdId.IsNotNull();
            filter.AdId.IsEqualTo(3748);
        }

        [TestMethod]
        public void GetAdId_BadAdId_ReturnsNull()
        {
            // Arrange
            var filter = new SearchFilters { Keyword = "8-blsdfj" };

            // Act
            // Assert
            filter.AdId.IsNull();
        }

        [TestMethod]
        public void GetAdId_BadPublicationId_ReturnsNull()
        {
            // Arrange
            var filter = new SearchFilters { Keyword = "blsdfj-9" };

            // Act
            // Assert
            filter.AdId.IsNull();
        }
    }
}
