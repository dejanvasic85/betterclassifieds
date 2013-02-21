namespace Paramount.Utility
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlUtilities
    {
        /// <summary>
        /// Serializes object with no namespace and no <?xml ... > tag. Returns pure xml of the object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerialiseObjectPureXml(object obj)
        {
            string serialXml;
            //Empty namespace    
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            //Serialize the passed object    
            var xs = new XmlSerializer(obj.GetType());
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                // set XML writer settings        
                var xwSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, CloseOutput = true };
                // create xml using the settings        
                using (XmlWriter xw = XmlWriter.Create(sw, xwSettings))
                {
                    if (xw != null) xs.Serialize(xw, obj, ns);
                    // get the xml and clean up            
                    serialXml = sb.ToString();
                    if (xw != null) xw.Flush();
                }
                sw.Flush();
            }
            //    
            return serialXml;
        }

        public static string SerializeObject(Object obj)
        {
            string serialXml;
            using (var sw = new StringWriter())
            {
                var xs = new XmlSerializer(obj.GetType());
                xs.Serialize(sw, obj);
                serialXml = sw.ToString();
                sw.Flush();
            }
            return serialXml;
        }
        public static T DeserializeObject<T>(string objString)
        {
            Object obj;
            var xs = new XmlSerializer(typeof(T));
            var encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(objString);
            using (var memoryStream = new MemoryStream(byteArray))
            {
                using (new XmlTextWriter(memoryStream, Encoding.UTF8))
                {
                    obj = xs.Deserialize(memoryStream);
                }
            }
            return (T)obj;
        }

        public static T CopyObject<T>(object obj)
        {
            return DeserializeObject<T>(SerializeObject(obj));
        }

        public static T CopyObjectFromProxy<T>(object obj, string removeNameSpace)
        {
            var serializedObject = SerializeObject(obj);
            serializedObject = serializedObject.Replace(removeNameSpace, "");
            return DeserializeObject<T>(serializedObject);
        }

        public static T CopyObjectToProxy<T>(object obj, string addNameSpace)
        {
            return DeserializeObject<T>(AddNameSpace(SerializeObject(obj), addNameSpace));
        }

        private static string AddNameSpace(string serializedObject, string addNameSpace)
        {
            var workingXml = serializedObject;
            while (workingXml.Length > 0)
            {
                var eachNode = workingXml.Substring(workingXml.IndexOf("<"), workingXml.IndexOf(">") + 1);
                if ((eachNode.ToLower().Contains("?xml")) || (eachNode.ToLower().Contains("xmlns:")) || (eachNode.ToLower().Contains("/")))
                    workingXml = workingXml.Replace(eachNode, "");
                else
                {
                    serializedObject = serializedObject.Replace(eachNode, eachNode.Replace(">", " " + addNameSpace + ">")); eachNode = eachNode.Replace("<", "</"); workingXml = workingXml.Substring(workingXml.IndexOf(eachNode) + eachNode.Length);
                }
            }
            return serializedObject;
        }
    }
}
