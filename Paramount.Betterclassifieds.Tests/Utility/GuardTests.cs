using System;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void NotNull_ValueIsNotNull_DoesNotThrowException()
        {
            var str = "hello world";
            Paramount.Guard.NotNull(str);
        }

        [Test]
        public void NotNull_ValueIsNull_ThrowsArgumentException()
        {
            string str = null;
            Assert.Throws<ArgumentNullException>(() => Paramount.Guard.NotNull(str));
        }

        [Test]
        public void NotNullIn_NoValueIsNull_DoesNotThrowException()
        {
            var collection = new string[] {"one", "two"};
            Paramount.Guard.NotNullIn(collection);
        }

        [Test]
        public void NotNullIn_OneValueIsNull_ThrowsArgumentException()
        {
            var collection = new[] {"hey", null, "you"};
            Assert.Throws<ArgumentNullException>(()=> Paramount.Guard.NotNullIn(collection));
        }
    }
}