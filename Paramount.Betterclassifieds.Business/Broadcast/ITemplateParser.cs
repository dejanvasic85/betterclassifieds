using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface ITemplateParser
    {
        string Name { get; }
        string ParseToString(string template, IDictionary<string, string> tokenValues);
    }

    /// <summary>
    /// Performs a simple find and replace for key value pairs with the key in format of [/{0}/] within the template
    /// </summary>
    public class SquareBracketParser : ITemplateParser
    {
        private const string Key = "[/{0}/]";

        public string Name { get { return GetType().Name; } }

        public string ParseToString(string template, IDictionary<string, string> tokenValues)
        {
            return tokenValues.Aggregate(template, (current, token) =>
                current.Replace(string.Format(Key, token.Key), token.Value));
        }
    }

    public static class ParserFactory
    {
        // Hardcoded registrations of template parsers available for use ( we'll only use one really ! )
        private static Dictionary<string, Type> Container = new Dictionary<string, Type>
        {
            { typeof(SquareBracketParser).Name, typeof(SquareBracketParser)}
        };

        public static ITemplateParser ResolveParser(string name)
        {
            if (!Container.ContainsKey(name)) 
                throw new ArgumentException(string.Format("'{0}' is not a registered template parser", name), name);

            return (ITemplateParser)Activator.CreateInstance(Container[name]);
        }
    }
}