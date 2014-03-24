using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.BusinessModel.Broadcast
{
    [TestClass]
    public class BroadcastTemplateParserTests
    {
        [TestMethod]
        public void ParseToString_ValidTemplateWithOneToken_ReturnsTransformedString()
        {
            // Arrange 
            const string template = "foo [/something/]";
            var tokenValues = new Dictionary<string, string> { { "something", "bar" } };

            // Act 
            BroadcastTemplateParser parser = new BroadcastTemplateParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo("foo bar");
        }

        [TestMethod]
        public void ParseToString_NoTokensProvided_ReturnsOriginalTemplate()
        {
            // Arrange 
            const string template = "foo [/something/]";
            var tokenValues = new Dictionary<string, string>();

            // Act 
            BroadcastTemplateParser parser = new BroadcastTemplateParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo(template);
        }

        [TestMethod]
        public void ParseToString_TemplateHasNotPlaceholders_ReturnsOriginalTemplate()
        {
            // Arrange 
            const string template = "foo bar";
            var tokenValues = new Dictionary<string, string> { { "something", "bar" } };

            // Act 
            BroadcastTemplateParser parser = new BroadcastTemplateParser();
            string result = parser.ParseToString(template, tokenValues);

            // Assert
            result.IsEqualTo(template);
        }
    }
}
