namespace Paramount.Betterclassifieds.Payments.pp
{
    public static class ApiContextFactory
    {
        // Returns APIContext object
        public static PayPal.APIContext CreateApiContext()
        {
            var config = PayPal.Manager.ConfigManager.Instance.GetProperties();
            
            var credentials = new PayPal.OAuthTokenCredential(
                config[PayPal.BaseConstants.ClientId], 
                config[PayPal.BaseConstants.ClientSecret], 
                config);

            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            PayPal.APIContext apiContext = new PayPal.APIContext(credentials.GetAccessToken())
            {
                Config = PayPal.Manager.ConfigManager.Instance.GetProperties()
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