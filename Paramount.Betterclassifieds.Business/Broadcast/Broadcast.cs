﻿using System.Collections.Generic;
using System.Reflection;

namespace Paramount.Betterclassifieds.Business
{
    public abstract class Broadcast
    {
        public string Recipient { get; set; }
        public abstract string TemplateName { get; }
     
        /// <summary>
        /// Returns a dictionary of all the template placeholder names and their values
        /// </summary>
        public IDictionary<string, string> GetPlaceholders()
        {
            var placeholders = new Dictionary<string, string>();

            // Build up the dictionary of values to be replaced in the templates ( Subject and Body for emails )
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                var attr = propertyInfo.GetCustomAttribute<PlaceholderAttribute>();
                if (attr == null)
                    continue;

                placeholders.Add(attr.TokenName, propertyInfo.GetValue(this).ToString());
            }

            return placeholders;
        }
    }
}