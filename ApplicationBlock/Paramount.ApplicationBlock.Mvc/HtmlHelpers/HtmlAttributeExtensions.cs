using System.Collections.Generic;

namespace Paramount.ApplicationBlock.Mvc
{
    internal static class HtmlAttributeExtensions
    {
        internal static IDictionary<string, object> WithFormControl(this IDictionary<string, object> attributes)
        {
            attributes.Add("class", "form-control");
            return attributes;
        }   

        internal static IDictionary<string, object> WithLargeInput(this IDictionary<string, object> attributes)
        {
            attributes.Add("class", "form-control input-lg");
            return attributes;
        } 
        
        internal static IDictionary<string, object> WithRows(this IDictionary<string, object> attributes, int rows)
        {
            attributes.Add("rows", rows.ToString());
            return attributes;
        }
    }
}