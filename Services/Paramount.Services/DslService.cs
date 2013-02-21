namespace Paramount.Services
{
    using System;
    using System.ServiceModel;
    using ApplicationBlock.Configuration;
    using Common.DataTransferObjects;
    using Common.DataTransferObjects.DSL;
    using Common.DataTransferObjects.DSL.Messages;
    using Common.ServiceContracts;
    using DataService;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class DslService : IDslService
    {
        public CreateDslDocumentResponse CreateDslDocument(CreateDslDocumentRequest request)
        {
            var response = new CreateDslDocumentResponse();

            // get the category details from the database
            DslDocumentCategory documentCategory = DslDataService.GetDocumentCategoryByCode(request.DocumentCategory);

            // perform validation on request file and the category for storage
            int index = request.FileName.LastIndexOf('.');
            string fileType = request.FileName.Substring(index, request.FileName.Length - index).ToLower();

            if (!documentCategory.AcceptedFileTypes.Contains(fileType))
            {
                throw new NotSupportedException(string.Format("The requested file does not match one of the accepted file types for Category '{0}'.", documentCategory.Title));
            }

            // check the file size
            if (request.FileLength > documentCategory.MaximumFileSize)
            {
                throw new NotSupportedException(string.Format("The requested file exceeded the maximum file size of {0} for Document Category {1}", documentCategory.MaximumFileSize, documentCategory.Title));
            }

            DslDocument document = DslDataService.CreateDocument(request.DocumentId
                , request.AccountId
                , request.DocumentCategory
                , request.ApplicationCode
                , request.StartDate
                , request.EndDate
                , request.EntityCode
                , request.FileData
                , request.FileName
                , request.FileType
                , request.Username
                , request.IsPrivate
                , request.ReferenceData
                , request.FileLength);

            response.DocumentId = document.DocumentId;

            return response;
        }

        public GetDslDocumentRequest GetDslDocument(GetDslDocumentRequest request)
        {
            DslDocument document = DslDataService.GetDocument(request.DocumentId, request.EntityCode);

            request.FileData = document.FileData;
            request.FileLength = document.FileLength;

            return request;

            //var response = new GetDslDocumentRequest
            //{
            //    DocumentId = document.DocumentId,
            //    EntityCode = document.EntityCode,
            //    FileData = document.FileData,
            //    FileLength = document.FileLength
            //};

            //return response;
        }

        public GetDslCategoryResponse GetDslCategory(GetDslCategoryRequest request)
        {
            var response = new GetDslCategoryResponse();
            response.DocumentCategory = DslDataService.GetDocumentCategoryByCode(request.DslCategoryType);
            return response;
        }

        public void DeleteDslDocument(DeleteDslDocumentRequest request)
        {
            var documentIdGuid = new Guid(request.DocumentId);
            DslDataService.DeleteDocument(documentIdGuid);
        }

        public void AppendFileChunk(AppendFileChunkRequest request)
        {
            DslDataService.AppendChunk(request.FileId, request.Buffer, request.Offset);
        }

        public CompleteFileUploadResponse CompleteFileUpload(UploadFileRequest request)
        {
            DslDataService.InsertFile(
                request.DocumentId,
                request.AccountId,(int)
                Enum.Parse(typeof(DslDocumentCategoryType), request.DocumentCategoryId),
                request.ApplicationCode,
                request.StartDate,
                request.EndDate,
                request.ClientCode,
                request.FileName,
                request.FileType,
                request.Username,
                request.IsPrivate,
                request.ReferenceData,
                request.FileLength
                );
            return new CompleteFileUploadResponse {UploadStatus = FileUploadStatus.Sucecess};
        }

        public DownloadChunkResponse DownloadChunk(DownloadChunkRequest request)
        {
            var fileChunk = DslDataService.DownloadFileChunk(request.DocumentId, request.ClientCode, request.Offset, ConfigSettingReader.DslChunkLength);
            return new DownloadChunkResponse {FileChunk = fileChunk};
        }

        public GetFileSizeResponse GetFileSize(GetFileSizeRequest request)
        {
            return new GetFileSizeResponse
                       {FileSize = DslDataService.GetFileSize(request.DocumentId, request.ClientCode)};
        }

        public GetMaxChunkSizeResponse GetMaximumChunkSize()
        {
            return new GetMaxChunkSizeResponse {ChunkSize = ConfigSettingReader.DslChunkLength};
        }
    }
}
