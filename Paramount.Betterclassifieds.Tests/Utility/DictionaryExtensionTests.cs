using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestClass]
    public class DictionaryExtensionTests
    {
        [TestMethod]
        public void AddOrUpdate_KeyExistsValueExists_ReturnsSelf()
        {
            IDictionary<string, int> pairs = new Dictionary<string, int>{{ "one", 1}, {"two", 2}}.AddOrUpdate("one", 1);

            pairs.Count.IsEqualTo(2);
            pairs.ContainsKey("one").IsTrue();
            pairs["one"].IsEqualTo(1);
        }

        [TestMethod]
        public void AddOrUpdate_KeyExistsValueToUpdate_ReturnsSelfWithNewKey()
        {
            IDictionary<string, int> pairs = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } }.AddOrUpdate("two", 0);

            pairs.Count.IsEqualTo(2);
            pairs.ContainsKey("two").IsTrue();
            pairs["two"].IsEqualTo(0);
        }

        [TestMethod]
        public void AddOrUpdate_KeyDoesNotExists_ReturnsSelfWithNewKey()
        {
            IDictionary<string, int> pairs = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } }.AddOrUpdate("three", 3);

            pairs.Count.IsEqualTo(3);
            pairs.ContainsKey("three").IsTrue();
            pairs["three"].IsEqualTo(3);
        }
    }
}
