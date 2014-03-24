using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Betterclassifieds.Business
{
    public interface IBroadcastTemplateParser
    {
        string ParseToString(string template, IDictionary<string, string> tokenValues);
    }

    /// <summary>
    /// Performs a simple find and replace for key value pairs with the key in format of [/{0}/] within the template
    /// </summary>
    public class BroadcastTemplateParser : IBroadcastTemplateParser
    {
        private const string Key = "[/{0}/]";

        public string ParseToString(string template, IDictionary<string, string> tokenValues)
        {
            return tokenValues.Aggregate(template, (current, token) => 
                current.Replace(string.Format(Key, token.Key), token.Value));
        }
    }
}