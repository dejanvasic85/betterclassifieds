using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests
{
    public static class JsonResultAssertExtensions
    {
        public static void JsonResultContains(this ActionResult actionResult, string expected)
        {
            actionResult.IsTypeOf<JsonResult>().Data.ToString().IsEqualTo(expected, ignoreCase: true);
        }

        public static void JsonResultDoesNotContain(this ActionResult actionResult, string expected)
        {
            var jsonResult = (JsonResult)actionResult;
            Assert.That(jsonResult.Data.ToString(), Is.Not.StringContaining(expected));
        }

        public static void JsonResultPropertyEquals<TProp>(this JsonResult jsonResult, string propertyName, TProp expectedValue)
        {
            var property = jsonResult.Data.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName));

            if (null == property)
            {
                Assert.Fail("Unable to find property " + propertyName + " in jsonResult");
            }

            var value = (TProp)property.GetValue(jsonResult.Data, null);
            Assert.AreEqual(expectedValue, value);
        }

        public static void JsonResultPropertyExists(this JsonResult jsonResult, string propertyName)
        {
            Assert.That(jsonResult.Data.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName)), Is.Not.Null);
        }

        public static void JsonResultContainsErrors(this JsonResult jsonResult)
        {
            JsonResultPropertyExists(jsonResult, "Errors");
        }

        public static void JsonResultNextUrlIs(this JsonResult jsonResult, string expectedNextUrl)
        {
            JsonResultPropertyEquals(jsonResult, "NextUrl", expectedNextUrl);
        }

    }
}