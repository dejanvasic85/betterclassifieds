using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    [TestFixture]
    public class DapperTest
    {
        [Test]
        public void AddOrUpdateCategories()
        {
            // Arrange and Act
            using (ITestDataManager dataManager = new DapperDataManager())
            {
                dataManager.AddOrUpdateCategory(name: "Selenium Sub", parent: "Selenium Parent");
            }
        }

    }
}