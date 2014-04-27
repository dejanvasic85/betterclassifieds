using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void ToCamelCaseWithSpaces_EmptyString_ReturnsEmptyString()
        {
            var result = "".ToCamelCaseWithSpaces();
            result.IsEqualTo("");
        }

        [TestMethod]
        public void ToCamelCaseWithSpaces_NoCapitals_ReturnsSameResult()
        {
            var result = "helloworld".ToCamelCaseWithSpaces();
            result.IsEqualTo("helloworld");
        }

        [TestMethod]
        public void ToCamelCaseWithSpaces_CamelCasedWord_ReturnsSameValueWithSpaces()
        {
            var result = "fooBarWithHelloWorld".ToCamelCaseWithSpaces();
            result.IsEqualTo("foo Bar With Hello World");
        }

        [TestMethod]
        public void ToCamelCaseWithSpaces_ContainsAcronymAndPreservesAcronym_ReturnsSameValueWithSpaces()
        {
            var result = "fooBarWithHelloWorldAndABC".ToCamelCaseWithSpaces();
            result.IsEqualTo("foo Bar With Hello World And ABC");
        }
    }
}