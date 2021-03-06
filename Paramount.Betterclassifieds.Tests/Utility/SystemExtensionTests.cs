using System;
using System.Globalization;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class SystemExtensionTests
    {
        [Test]
        public void ToIsoDateString_ReturnsIsoDateFormat()
        {
            var someDateTime = DateTime.ParseExact("2017-06-12 05:55", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var result = someDateTime.ToIsoDateString();

            result.IsNotNull();
            result.IsEqualTo("2017-06-12T05:55:00");
        }

        [Test]
        public void ToIsoDateString_NullValue_ReturnsEmptyString()
        {
            DateTime? nullDate = null;
            var result = nullDate.ToIsoDateString();
            result.IsNull();
        }

        [Test]
        public void ToUtcIsoDateString_ReturnsIsoDateFormat()
        {
            var someDateTime = DateTime.ParseExact("2017-06-12 05:55", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var result = someDateTime.ToUtcIsoDateString();

            result.IsNotNull();
            result.IsEqualTo("2017-06-12T05:55:00Z");
        }

        [Test]
        public void ToUtcIsoDateString_NullValue_ReturnsEmptyString()
        {
            DateTime? nullDate = null;
            var result = nullDate.ToUtcIsoDateString();
            result.IsNull();
        }

        [Test]
        public void ToDisplayDateTimeFormat_ReturnsString()
        {
            var datetime = new DateTime(2000, 8, 13, 9, 30, 30);
            var format = datetime.ToDisplayDateTimeFormat();

            format.IsEqualTo("Sunday, 13 August 2000 9:30 AM");
        }

        [Test]
        public void ToDisplayDateTimeFormat_Null_ReturnsEmptyString()
        {
            DateTime? nullDate = null;
            var format = nullDate.ToDisplayDateTimeFormat();

            format.IsEqualTo(string.Empty);
        }

        [Test]
        public void ToHourFormat_ReturnsString()
        {
            var datetime = new DateTime(2000, 8, 13, 9, 30, 30);
            var format = datetime.ToHourFormat();

            format.IsEqualTo("9:30 AM");
        }
    }
}