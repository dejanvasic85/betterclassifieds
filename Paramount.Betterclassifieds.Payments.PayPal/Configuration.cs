using System.Net;
using PayPal.Api;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public static class ApiContextFactory
    {
        // Returns APIContext object
        public static APIContext CreateApiContext()
        {
            var config = ConfigManager.Instance.GetProperties();
            
            var credentials = new OAuthTokenCredential(
                config[BaseConstants.ClientId], 
                config[BaseConstants.ClientSecret], 
                config);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 9999;

            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = new APIContext(credentials.GetAccessToken())
            {
                Config = ConfigManager.Instance.GetProperties()
            };

            // Use this variant if you want to pass in a request id  
            // that is meaningful in your application, ideally 
            // a order id.
            // String requestId = Long.toString(System.nanoTime();
            // APIContext apiContext = new APIContext(GetAccessToken(), requestId ));

            return apiContext;
        }

    }
}