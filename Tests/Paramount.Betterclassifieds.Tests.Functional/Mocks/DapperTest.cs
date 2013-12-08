using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    [TestFixture]
    public class DapperTest
    {
        [Test]
        public void AddOrUpdateBooking_Successful()
        {
            // Arrange and Act
            using (ITestDataManager dataManager = new DapperDataManager())
            {
                dataManager.AddOrUpdateOnlineAd("this is a cool ad", "Selenium Sub");
            }
        }

        [Test]
        public void AddOrUpdateCategory_Successful()
        {
            using (ITestDataManager manager = new DapperDataManager())
            {
                manager.AddOrUpdateCategory("test", "parent test");
            }
        }
    }
}