namespace Paramount.Services.Proxy
{
    using System.ServiceModel;
    using Paramount.Common.ServiceContracts;
    using System.ServiceModel.Channels;
    using Paramount.Common.DataTransferObjects.Broadcast.Messages;

    public partial class BroadcastServiceClient : ClientBase<IBroadcastService>
    {
        public BroadcastServiceClient() { }

        public BroadcastServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public BroadcastServiceClient(Binding binding, EndpointAddress endpointAddress)
            : base(binding, endpointAddress) { }

        public SendEmailResponse SendMail(SendEmailRequest request)
        {
            return Channel.SendEmail(request);
        }

        public SendEmailResponse SendNewsletter(SendNewsLetterRequest request)
        {
            return Channel.SendNewsletter(request);
        }

        public string GetServiceInfo()
        {
            return Channel.GetServiceInfo();
        }

        public void Process(ProcessEmailsRequest request)
        {
            Channel.ProcessEmails(request);
        }

        public void CreateEmailBroadcastTrack(CreateEmailTrackRequest request)
        {
            Channel.CreateEmailBroadcastTrack(request);
        }

        public GetEmailTemplateListResponse GetEmailTemplateListByClient(GetEmailTemplateListRequest request)
        {
            return Channel.GetEmailTemplateByEntity(request);
        }

        public void InsertUpdateTemplate(InsertUpdateTemplateRequest request)
        {
            Channel.InsertUpdateTemplate(request);
        }

        public GetEmailTemplateResponse GetEmailTemplate(GetEmailTemplateRequest request)
        {
            return Channel.GetEmailTemplate(request);
        }

        public GetBroadcastActivityResponse GetBroadcastActivity(GetBroadcastActivityRequest request)
        {
            return Channel.GetBroadcastActivity(request);
        }
    }
}
