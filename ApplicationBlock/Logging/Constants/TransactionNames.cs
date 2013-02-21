namespace Paramount.ApplicationBlock.Logging.Constants
{
    public class TransactionNames
    {
        private const string Request = "Request.";
        private const string Response = "Response.";
        //private const string Broadcast = "Broadcast.";

        public const string CreateEventRequest = Request + "CreateEvent";
        public const string CreateEventResponse = Response + "CreateEvent";

        public const string GetEventGenreRequest = Request + "GetGenreList";
        public const string GetEventGenreResponse = Response + "GetGenreList";

        //broadcast
        public  const string ProcessEmailResponse = Response + "EmailProcess";
        public const string ProcessEmailRequest = Request + "Email.Process";

        public const string SendEmailRequest = Request + "Broadcast.SendEmail";
        public const string SendEmailResponse = Response + "Broadcast.SendEmail";

        public const string GetEmailTemplatesRequest = Request + "GetEmailTemplates";
        public const string GetEmailTemplatesResponse = Response + "GetEmailTemplates";

        //dsl
        public const string UploadFileChunkRequest = Request + "UploadFileChunk";
        public const string UploadFileChunkResponse = Response + "UploadFileChunk";
        public const string DownloadFileChunkRequest = Request + "DownloadFileChunk";
        public const string DownloadFileChunkResponse = Response + "DownloadFileChunk";
        public const string UploadFileRequest = Request + "UploadFile";
        public const string UploadFileResponse = Response + "UploadFile";
        public const string DownloadFileRequest = Request + "DownloadFile";
        public const string DownloadFileResponse = Response + "DownloadFile";
        public const string GetDocumentCategoryRequest = Request + "GetDocumentCategory";
        public const string GetDocumentCategoryResponse = Response + "GetDocumentCategory";
        public const string DeleteDocumentCategoryRequest = Request + "DeleteDocumentCategory";
        public const string DeleteDocumentCategoryResponse = Response + "DeleteDocumentCategory";
        public const string GetFileSizeRequest = Request + "GetFileSize";
        public const string GetFileSizeResponse = Response + "GetFileSize";
        public const string GetAllowedChunkSizeRequest = Request + "GetAllowedChunkSize";
        public const string GetAllowedChunkSizeResponse = Response + "GetAllowedChunkSize";

        //
        public const string GetNewsletterUsersResponse = Response + "GetNewsletterUsers";
        public const string GetNewsletterUsersRequest = Request + "GetNewsletterUsers";

        public const string UnSubscribeRequest = Request + "UnSubscribeUser";
    }
}
