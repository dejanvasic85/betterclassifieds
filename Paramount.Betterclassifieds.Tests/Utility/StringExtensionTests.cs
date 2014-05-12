using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ToCamelCaseWithSpaces_EmptyString_ReturnsEmptyString()
        {
            var result = "".ToCamelCaseWithSpaces();
            result.IsEqualTo("");
        }

        [Test]
        public void ToCamelCaseWithSpaces_NoCapitals_ReturnsSameResult()
        {
            var result = "helloworld".ToCamelCaseWithSpaces();
            result.IsEqualTo("helloworld");
        }

        [Test]
        public void ToCamelCaseWithSpaces_CamelCasedWord_ReturnsSameValueWithSpaces()
        {
            var result = "fooBarWithHelloWorld".ToCamelCaseWithSpaces();
            result.IsEqualTo("foo Bar With Hello World");
        }

        [Test]
        public void ToCamelCaseWithSpaces_ContainsAcronymAndPreservesAcronym_ReturnsSameValueWithSpaces()
        {
            var result = "fooBarWithHelloWorldAndABC".ToCamelCaseWithSpaces();
            result.IsEqualTo("foo Bar With Hello World And ABC");
        }
    }
}