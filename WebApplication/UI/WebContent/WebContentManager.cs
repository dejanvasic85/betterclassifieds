namespace Paramount.Common.UI.WebContent
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;

    public class WebContentManager
    {
        public static string GetResource(EntityGroup entityGroup, ContentItem contentClass, string fieldId)
        {
            // try to get resources from web content
            //var output =
            // (string)HttpContext.GetGlobalResourceObject(entityGroup.ToString(), contentClass);

            var output = GetResourceFromXml(entityGroup.ToString(), string.Format("{0}/{1}", contentClass, fieldId));


            return string.IsNullOrEmpty(output) ? string.Format(CultureInfo.InvariantCulture, "missing.{0}/{1}/{2}", entityGroup, contentClass, fieldId) : output;
        }
        /// <summary>
        /// this function will try to find a resource item from the entityPreference xml file
        /// if no key's found then return empty string
        /// if more than one items found for the same key then an exception will be raised
        /// if a item is found but the qualifier doesn't match then an exception will be raised
        /// </summary>
        /// <param name="qualifier"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetResourceFromXml(string qualifier, string key)
        {
            var xmlContent =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Paramount.Common.UI.WebContent.ContentXml.xml");

            if (xmlContent == null)
            {
                throw new ArgumentNullException(
                    string.Format(CultureInfo.InvariantCulture, "Content Xml Stream object is null"));
            }

            var resourceXml = new XmlDocument();

            resourceXml.Load(xmlContent);

            var keyNodes = resourceXml.SelectNodes(@"//*[@key='" + key + "']");

            if (keyNodes == null)
            {
                return string.Empty;
            }

            if (keyNodes.Count == 0)
            {
                return string.Empty;
            }

            if (keyNodes.Count > 1)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "duplicate content - qualifier:{0} key:{1}",
                        qualifier,
                        key));
            }

            if (string.Compare(qualifier, keyNodes[0].Attributes["qualifier"].Value, StringComparison.OrdinalIgnoreCase)
                != 0)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "invlid qualifier - qualifier:{0} key:{1}",
                        qualifier,
                        key));
            }

            return keyNodes[0].InnerText;
        }
    }
}