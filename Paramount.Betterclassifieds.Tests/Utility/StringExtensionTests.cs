using System;
using System.Web.UI;
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

        [Test]
        public void TruncateOnWordBoundary_StringLengthSameAsMax_ReturnsTruncatedValue()
        {
            var result = "This is it".TruncateOnWordBoundary(10);

            Assert.That(result, Is.EqualTo("This is..."));
        }

        [Test]
        public void TruncateOnWordBoundary_EntireWord_ReturnsTheSameWord()
        {
            var result = "MyValueIsOneWordOnly".TruncateOnWordBoundary(20);

            Assert.That(result, Is.EqualTo("MyValueIsOneWordOnly"));
        }

        [Test]
        public void TruncateOnWordBoundary_RegularStringWithNormalWords_ReturnsTruncatedValue()
        {
            var result = "Two Words Should be Really coold man".TruncateOnWordBoundary(20);

            Assert.That(result, Is.EqualTo("Two Words Should be..."));
        }

        [Test]
        public void TruncateOnWordBoundary_RegularStringWithNormalWords_CustomSuffix_ReturnsTruncatedValueWithCustomSuffix()
        {
            var result = "Two Words Should be Really coold man".TruncateOnWordBoundary(20, " ooOOOoo");

            Assert.That(result, Is.EqualTo("Two Words Should be ooOOOoo"));
        }

        [Test]
        public void TruncateOnWordBoundary_ShorterWord_ReturnsSameWordWithNoSuffix()
        {
            var result = "Two Words".TruncateOnWordBoundary(20);

            Assert.That(result, Is.EqualTo("Two Words"));
        }

        [Test]
        public void TruncateOnWordBoundary_FailingString()
        {
            var stringInProd =
                "Experienced 54 yo keyboard player (some lead/backing vocals) interested in joining Duo, Trio or Band - 60's, 70's, 80's etc covers. Tony 0407-414-883".TruncateOnWordBoundary(150);


        }

        [Test]
        public void StripLineBreaks_NoLineBreaks_ReturnsSameValue()
        {
            Assert.That("ladida".StripLineBreaks(), Is.EqualTo("ladida"));
        }

        [Test]
        public void StripLineBreaks_NewLine_SwapsNewLineWithSpaces()
        {
            Assert.That("\nladi\nda\n".StripLineBreaks(), Is.EqualTo(" ladi da "));
        }

        [Test]
        public void StripLineBreaks_NewLineAndBreak_SwapsNewLineWithSpaces()
        {
            Assert.That("\n\rladi\n\rda\n".StripLineBreaks(), Is.EqualTo(" ladi da "));
        }

        [Test]
        public void FileExtension_ValidFileName_ReturnsExtension()
        {
            var result = "myfilename.jpg".FileExtension();

            Assert.That(result, Is.EqualTo("jpg"));
        }

        [Test]
        public void FileExtension_NoExtension_ReturnsEmpty()
        {
            var result = "myfilename".FileExtension();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void FileExtension_EmptyString_ReturnsEmpty()
        {
            var result = "".FileExtension();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void WithoutFileExtension_ValidFileName_ReturnsFileNameWithoutExtension()
        {
            var result = "myfilename.jpg".WithoutFileExtension();

            Assert.That(result, Is.EqualTo("myfilename"));
        }

        [Test]
        public void WithoutFileExtension_EmptyString_ReturnsEmptyString()
        {
            var result = "".WithoutFileExtension();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void WithoutFileExtension_IndexAtOne_ReturnsEmptyString()
        {
            var result = ".something".WithoutFileExtension();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void WithoutFileExtension_FilenameWithoutExtenion_ReturnsFilename()
        {
            var result = "something".WithoutFileExtension();

            Assert.That(result, Is.EqualTo("something"));
        }

        [Test]
        public void ReplaceLineBreaks_EnvironmentNewLine_DefaultParameter_ReplacesNewLineWithSpace()
        {
            var sample = string.Format("hello" + Environment.NewLine + "there").ReplaceLineBreaks();
            sample.IsEqualTo("hello there");

            var sampleWithReturn = string.Format("hello" + Environment.NewLine + " there\r\n").ReplaceLineBreaks("<br />");
            sampleWithReturn.IsEqualTo("hello<br /> there<br />");
        }

        [Test]
        public void ReplaceLineBreaks_TwoNewLines_ReplacesBothWithSpace()
        {
            var sampleWithTwoNewLines = string.Format("hello\n\nthere").ReplaceLineBreaks();
            sampleWithTwoNewLines.IsEqualTo("hello  there");
        }

        [Test]
        public void ReplaceLineBreaks_EnvironmentNewLine_AndCarriageReturn_WithCustomParameter_ReplacesNewLineWithParameter()
        {
            var sampleWithReturn = string.Format("hello" + Environment.NewLine + " there\r\n").ReplaceLineBreaks("<br />");
            sampleWithReturn.IsEqualTo("hello<br /> there<br />");
        }

    }
}