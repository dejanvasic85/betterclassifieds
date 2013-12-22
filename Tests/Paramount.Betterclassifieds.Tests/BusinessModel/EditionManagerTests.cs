using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
    using Business;

    [TestClass]
    public class EditionManagerTests
    {
        private static MockRepository MockRepository;
        private readonly List<Action> verifyList = new List<Action>();

        #region Test Setup

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            MockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestInitialize]
        public void InitTest()
        {
            verifyList.Clear();

        }

        [TestCleanup]
        public void CleanTest()
        {
            verifyList.ForEach(action => action());
        }

        #endregion
        
        //[TestMethod]
        //public void RemoveEdition_BookingsExist_RemovesEditionAndExtends()
        //{
        //    // Arrange 
        //    var editionDate = new DateTime(2013, 12, 25);
        //    MockRepository.Create<>()

        //    // Act 
        //    var editionManager = new EditionManager();
        //    editionManager.RemoveEdition(editionDate);

        //    // Assert

        //}
    }
}
