namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using System.Web.Services;
    using DataTransferObjects;
    using DataTransferObjects.Broadcast.Messages;
    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1),ServiceContract]
    public interface IBroadcastService
    {
        [OperationContract]
        SendEmailResponse SendEmail(SendEmailRequest request);

        [OperationContract]
        SendEmailResponse SendNewsletter(SendNewsLetterRequest request);

        [OperationContract]
        string GetServiceInfo();

        [OperationContract]
        void ProcessEmails(ProcessEmailsRequest request);

        [OperationContract]
        void CreateEmailBroadcastTrack(CreateEmailTrackRequest request);

        [OperationContract]
        GetEmailTemplateListResponse GetEmailTemplateByEntity(GetEmailTemplateListRequest request);

        [OperationContract]
        void InsertUpdateTemplate(InsertUpdateTemplateRequest insertUpdateTemplateRequest);

        [OperationContract]
        GetEmailTemplateResponse GetEmailTemplate(GetEmailTemplateRequest emailTemplateRequest);
    }
}