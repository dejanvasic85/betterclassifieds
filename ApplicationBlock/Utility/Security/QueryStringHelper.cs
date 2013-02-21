namespace Paramount.Utility.Security
{
    public static class QueryStringHelper
    {
        public static string GenerateSecureUrl(string path, SecureQueryString queryString )
        {
            return string.Format("{0}?key={1}", path, queryString.EncryptedString);
        }
    }
}