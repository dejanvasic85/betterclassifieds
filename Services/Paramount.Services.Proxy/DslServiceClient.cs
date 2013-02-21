namespace Paramount.Services.Proxy
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using Paramount.Common.DataTransferObjects.DSL.Messages;
    using Paramount.Common.ServiceContracts;

    public partial class DslServiceClient : ClientBase<IDslService>
    {
        public DslServiceClient() { }

        public DslServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public DslServiceClient(Binding binding, EndpointAddress endpointAddress)
            : base(binding, endpointAddress) { }

        public CreateDslDocumentResponse CreateDslDocument(CreateDslDocumentRequest request)
        {
            return Channel.CreateDslDocument(request);
        }

        public GetDslDocumentRequest GetDslDocument(GetDslDocumentRequest request)
        {
            return Channel.GetDslDocument(request);
        }

        public GetDslCategoryResponse GetDslCategory(GetDslCategoryRequest request)
        {
            return Channel.GetDslCategory(request);
        }

        public void DeleteDslDocument(DeleteDslDocumentRequest request)
        {
            Channel.DeleteDslDocument(request);
        }

        public void AppendFileChunk(AppendFileChunkRequest request)
        {
            Channel.AppendFileChunk(request);
        }

        public CompleteFileUploadResponse CompleteFileUpload(UploadFileRequest request)
        {
            return Channel.CompleteFileUpload(request);
        }

        public DownloadChunkResponse DownloadChunk(DownloadChunkRequest request)
        {
            return Channel.DownloadChunk(request);
        }

        public GetFileSizeResponse GetFileSize(GetFileSizeRequest request)
        {
            return Channel.GetFileSize(request);
        }

        public GetMaxChunkSizeResponse GetMaximumChunkSize()
        {
            return Channel.GetMaximumChunkSize();
        }
    }
}
