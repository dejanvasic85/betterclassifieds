using System.Diagnostics;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Paramount.Betterclassifieds.Business;
using RestSharp;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IGoogleCaptchaVerifier
    {
        bool IsValid(string siteSectionSecret, HttpRequestBase httpRequest);
    }

    public class GoogleCaptchaVerifier : IGoogleCaptchaVerifier
    {
        private readonly ILogService _logService;
        private readonly IApplicationConfig _appConfig;

        public GoogleCaptchaVerifier(ILogService logService, IApplicationConfig appConfig)
        {
            _logService = logService;
            _appConfig = appConfig;
        }

        public bool IsValid(string siteSectionSecret, HttpRequestBase httpRequest)
        {
            if (!_appConfig.GoogleCaptchaEnabled)
            {
                _logService.Info("Google captcha disabled. Returning true");
                return true;
            }

            var watch = new Stopwatch();
            watch.Start();
            var restclient = new RestClient("https://www.google.com/recaptcha/api");

            _logService.Info("Verifying the google reCaptcha");

            var request = new RestRequest("/siteverify");
            request.AddQueryParameter("secret", siteSectionSecret);
            request.AddQueryParameter("response", httpRequest.Form["g-recaptcha-response"]);
            
            var response = restclient.Post(request);
            watch.Stop();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                var parsed = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(content);

                if (parsed.Success)
                {
                    _logService.Info("Google Recaptcha verified ok", watch.Elapsed);
                    return true;
                }
            }

            _logService.Warn($"Google ReCaptcha unsuccessful. Status {response.StatusCode}. Content: {response.Content}. Error {response.ErrorMessage}", watch.Elapsed);
            return false;
        }
    }
    
    public class GoogleCaptchaResponse
    {
        public bool Success { get; set; }   

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }

        public string Hostname { get; set; }
    }
}
