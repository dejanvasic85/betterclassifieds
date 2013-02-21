namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using DataTransferObjects;
    using DataTransferObjects.DSL.Messages;
    using DataTransferObjects.DSL;
    
    [ServiceContract]
    [ServiceKnownType(typeof(DslDocumentCategoryType))]
    public interface IDslService
    {
        [OperationContract]
        CreateDslDocumentResponse CreateDslDocument(CreateDslDocumentRequest request);

        [OperationContract]
        GetDslDocumentRequest GetDslDocument(GetDslDocumentRequest request);

        [OperationContract]
        GetDslCategoryResponse GetDslCategory(GetDslCategoryRequest request);

        [OperationContract]
        void DeleteDslDocument(DeleteDslDocumentRequest request);

        [OperationContract]
        void AppendFileChunk(AppendFileChunkRequest request);

        [OperationContract]
        CompleteFileUploadResponse CompleteFileUpload(UploadFileRequest request);

        [OperationContract]
        DownloadChunkResponse DownloadChunk(DownloadChunkRequest request);

        [OperationContract]
        GetFileSizeResponse GetFileSize(GetFileSizeRequest request);

        [OperationContract]
        GetMaxChunkSizeResponse GetMaximumChunkSize();

    }
}
