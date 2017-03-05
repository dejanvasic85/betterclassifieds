using System;
using Paramount.Betterclassifieds.Business;
using RestSharp;
using RestSharp.Authenticators;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IMailSender<in TMail> where TMail : EmailDetails
    {
        void Send(TMail mail);
    }

    public class MailgunSender<TMail> : IMailSender<TMail> where TMail : EmailDetails
    {
        private readonly IApplicationConfig _applicationConfig;
        private readonly IClientConfig _clientConfig;

        public MailgunSender(IApplicationConfig applicationConfig, IClientConfig clientConfig)
        {
            _applicationConfig = applicationConfig;
            _clientConfig = clientConfig;
        }

        public void Send(TMail mail)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api",
                    "key-518e9736ee87c705773eaa1a967778da")
            };
            
            var request = new RestRequest();
            request.AddParameter("domain", "kandobay.com.au", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@kandobay.com.au>");
            request.AddParameter("to", mail.To);
            request.AddParameter("subject", mail.Subject);
            request.AddParameter("html", mail.Body);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}