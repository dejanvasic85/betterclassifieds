using System.Collections.Generic;

namespace Paramount.ApplicationBlock.Mvc
{
    internal static class HtmlAttributeExtensions
    {
        internal static IDictionary<string, object> WithFormControl(this IDictionary<string, object> attributes)
        {
            AddClass(attributes, "form-control");
            return attributes;
        }

        internal static IDictionary<string, object> WithLargeInput(this IDictionary<string, object> attributes)
        {
            AddClass(attributes, "form-control input-lg");
            return attributes;
        } 
        
        internal static IDictionary<string, object> WithRows(this IDictionary<string, object> attributes, int rows)
        {
            attributes.Add("rows", rows.ToString());
            return attributes;
        }

        private static void AddClass(IDictionary<string, object> attributes, string classValue)
        {
            if (attributes.ContainsKey("class"))
            {
                attributes["class"] = string.Format("{0} {1}", attributes["class"], classValue);
            }
            else
            {
                attributes["class"] = classValue;
            }
        }
    }
}