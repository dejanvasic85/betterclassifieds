using System.Collections.Generic;
using PayPal;

namespace Paramount.Betterclassifieds.Payments.PayPal
{
    public static class Configuration
    {
        public static readonly string ClientId = "AdzNQRANeIFxBsrLTYVDSKNrl8uuqftshLAZemgqqN5i2qHaWi3SYqEIY2DL";
        public static readonly string ClientSecret = "EJidmBCn4yFeCfZbVdx9scjjaquXAs_D4P6K3dHbCT5Qg2d0T2Hy6In3zVYq";

        // Create the configuration map that contains mode and other optional configuration details.
        public static Dictionary<string, string> GetConfig()
        {
            var config = new Dictionary<string, string>();

            // Endpoints are varied depending on whether sandbox OR live is chosen for mode
            config["mode"] = "sandbox";
            config["endpoint"] = "https://api.sandbox.paypal.com";

            // These values are defaulted in SDK. If you want to override default values, uncomment it and add your value
            // config["connectionTimeout"] = "360000";
            // config["requestRetries"] = "1";
            return config;
        }

        // Create accessToken
        private static string GetAccessToken()
        {
            // ###AccessToken
            // Retrieve the access token from
            // OAuthTokenCredential by passing in
            // ClientID and ClientSecret
            // It is not mandatory to generate Access Token on a per call basis.
            // Typically the access token can be generated once and
            // reused within the expiry window                
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // Returns APIContext object
        public static APIContext GetAPIContext()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();

            // Use this variant if you want to pass in a request id  
            // that is meaningful in your application, ideally 
            // a order id.
            // String requestId = Long.toString(System.nanoTime();
            // APIContext apiContext = new APIContext(GetAccessToken(), requestId ));

            return apiContext;
        }

    }
}