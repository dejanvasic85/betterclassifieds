using System;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
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
        private readonly IApplicationConfig _applicationConfig;
        private readonly IRestClient _restClient;
        private readonly ILogService _logService;

        public MailgunSender(IClientConfig clientConfig, IApplicationConfig applicationConfig, ILogService logService)
        {
            _clientConfig = clientConfig;
            _applicationConfig = applicationConfig;
            _logService = logService;
            _restClient = new RestClient
            {
                BaseUrl = new Uri(applicationConfig.MailgunBaseUrl),
                Authenticator = new HttpBasicAuthenticator("api", applicationConfig.MailgunApiKey)
            };
        }

        public void Send(string to, string body, string subject, params MailAttachment[] attachments)
        {
            _logService.Info($"MailService: Preparing message for {to} subject: {subject}");

            var request = new RestRequest();
            request.AddParameter("domain", _applicationConfig.MailgunDomain, ParameterType.UrlSegment);
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

            _logService.Info("Mailgun completed successfully");
        }
    }
}