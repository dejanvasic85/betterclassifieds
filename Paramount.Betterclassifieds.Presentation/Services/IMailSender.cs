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
        private readonly IClientConfig _clientConfig;
        private readonly IRestClient _restClient;

        public MailgunSender(IClientConfig clientConfig, IApplicationConfig applicationConfig)
        {
            _clientConfig = clientConfig;
            _restClient = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", applicationConfig.MailgunApiKey)
            };
        }

        public void Send(TMail mail)
        {
            var request = new RestRequest();
            request.AddParameter("domain", _clientConfig.EmailDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", _clientConfig.EmailFromAddress);
            request.AddParameter("to", mail.To);
            request.AddParameter("subject", mail.Subject);
            request.AddParameter("html", mail.Body);
            request.Method = Method.POST;
            _restClient.Execute(request);
        }
    }
}