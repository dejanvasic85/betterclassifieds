using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Paramount.Utility.Security
{
    public class SecureQueryString : NameValueCollection
    {

        public SecureQueryString()  { }
  
        public SecureQueryString(string encryptedString)
        {
            Deserialize(CryptoHelper.Decrypt(encryptedString));
        }

        /// <summary>
        /// Returns the encrypted query string.
        /// </summary>
        public string EncryptedString
        {
            get
            {
                return HttpUtility.UrlEncode(CryptoHelper.Encrypt(Serialize()));
            }
        }

        /// <summary>
        /// Returns the EncryptedString property.
        /// </summary>
        public override string ToString()
        {
            return EncryptedString;
        }
        
     
        /// <summary>
        /// Deserializes a decrypted query string and stores it
        /// as name/value pairs.
        /// </summary>
        private void Deserialize(string decryptedQueryString)
        {
            var nameValuePairs = decryptedQueryString.Split('&');
            foreach (var nameValue in nameValuePairs.Select(t => t.Split('=')).Where(nameValue => nameValue.Length == 2))
            {
                base.Add(nameValue[0], nameValue[1]);
            }
        }


        /// <summary>
        /// Serializes the underlying NameValueCollection as a QueryString
        /// </summary>
        public string Serialize()
        {
            var sb = new StringBuilder();
            foreach (var key in base.AllKeys)
            {
                sb.Append(key);
                sb.Append('=');
                sb.Append(base[key]);
                sb.Append('&');
            }
            
            return sb.ToString();
        }

    }
}