using System;
using System.Collections.Generic;
using System.Reflection;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    /// <summary>
    /// Used for properties on Broadcast types e.g. AccountConfirmation class for FirstName property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class PlaceholderAttribute : Attribute
    {
        public string TokenName { get; private set; }
        
        public PlaceholderAttribute(string tokenName)
        {
            this.TokenName = tokenName;
        }
    }

    public static class PlaceholderExtentensions
    {
        /// <summary>
        /// Creates dictionary with key being attribute TokenName and value as property of the object
        /// </summary>
        public static IDictionary<string, string> ToPlaceholderDictionary<T>(this T broadcast) where T : class
        {
            var placeholders = new Dictionary<string, string>();

            // Build up the dictionary of values to be replaced in the templates ( Subject and Body for emails )
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                var attr = propertyInfo.GetCustomAttribute<PlaceholderAttribute>();
                if (attr == null)
                    continue;

                placeholders.Add(attr.TokenName, propertyInfo.GetValue(broadcast).ToString());
            }

            return placeholders;
        }
    }
}