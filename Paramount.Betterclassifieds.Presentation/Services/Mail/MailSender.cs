using System;
using Paramount.Betterclassifieds.Business;
using RestSharp;
using RestSharp.Authenticators;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IMailSender
    {
        void Send(string to, string body, string subject, params MailAttachment[] attachments);
    }

    public class MailgunSender : IMailSender
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

        public void Send(string to, string body, string subject, params MailAttachment[] attachments)
        {
            var request = new RestRequest();
            request.AddParameter("domain", _clientConfig.EmailDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", _clientConfig.EmailFromAddress);
            request.AddParameter("to", to);
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    request.AddFileBytes("attachment",
                        attachment.FileContents,
                        attachment.Filename,
                        attachment.ContentType);
                }
            }

            _restClient.Execute(request);
        }
    }
}