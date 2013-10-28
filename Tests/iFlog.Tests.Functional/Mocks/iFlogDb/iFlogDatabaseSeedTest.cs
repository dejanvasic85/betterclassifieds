using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    [TestClass]
    public class iFlogDatabaseSeedTest
    {
        [TestMethod]
        public void InitialiseDatabase_Successful()
        {
            IDataManager manager = new EntityDataManager();
            manager.Initialise();
            manager.AddOrUpdateOnlineAd("Ignored ad");
        }
    }
}
