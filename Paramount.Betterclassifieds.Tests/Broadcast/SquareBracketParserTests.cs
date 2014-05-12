﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.Broadcast
{
    [TestFixture]
    public class SquareBracketParserTests
    {
        [Test]
        public void ParseToString_ValidTemplateWithOneToken_ReturnsTransformedString()
        {
            // Arrange 
            const string template = "foo [/something/]";
            var tokenValues = new Dictionary<string, string> { { "something", "bar" } };

            // Act 
            SquareBracketParser parser = new SquareBracketParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo("foo bar");
        }

        [Test]
        public void ParseToString_NoTokensProvided_ReturnsOriginalTemplate()
        {
            // Arrange 
            const string template = "foo [/something/]";
            var tokenValues = new Dictionary<string, string>();

            // Act 
            SquareBracketParser parser = new SquareBracketParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo(template);
        }

        [Test]
        public void ParseToString_TemplateHasNoPlaceholders_ReturnsOriginalTemplate()
        {
            // Arrange 
            const string template = "foo bar";
            var tokenValues = new Dictionary<string, string> { { "something", "bar" } };

            // Act 
            SquareBracketParser parser = new SquareBracketParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo(template);
        }

        [Test]
        public void ParseToString_TemplateIsNullOrEmpty_ReturnsEmptyString()
        {
            // Arrange 
            
            var tokenValues = new Dictionary<string, string> { { "something", "bar" } };

            // Act 
            SquareBracketParser parser = new SquareBracketParser();
            string result = parser.ParseToString(string.Empty, tokenValues);
            string result2 = parser.ParseToString(null, tokenValues);

            // Assert
            result.IsEqualTo(string.Empty);
            result2.IsEqualTo(string.Empty);
        }

        [Test]
        public void ParseToString_TokensAreNull_ReturnsEmptyString()
        {
            // Arrange 
            // Act 
            SquareBracketParser parser = new SquareBracketParser();
            string result = parser.ParseToString("Not Relevant now", null);

            // Assert
            result.IsEqualTo(string.Empty);
        }
    }
}
