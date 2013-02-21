namespace Paramount.DSL.UIController
{
    using System;
    using System.Linq;
    using ApplicationBlock.Configuration;
    //using ApplicationBlock.Logging.AuditLogging;
    //using ApplicationBlock.Logging.Constants;
    //using ApplicationBlock.Logging.EventLogging;
    using Common.DataTransferObjects.DSL.Messages;
    using Services.Proxy;
    using Utility;
    using System.IO;
    using Paramount.DSL.UIController.ViewObjects;

    public static class DslController
    {
        public static long GetFileSize(Guid documentId, string entityCode)
        {
            var groupingId = Guid.NewGuid().ToString();
            try
            {
                var request = new GetFileSizeRequest
                {
                    ClientCode = entityCode,
                    DocumentId = documentId
                };

                //var auditLog = new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = XmlUtilities.SerializeObject(request),
                //    TransactionName = TransactionNames.GetFileSizeRequest
                //};
                //AuditLogManager.Log(auditLog);
                var response = WebServiceHostManager.DslServiceHost.GetFileSize(request);

                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = XmlUtilities.SerializeObject(response),
                //    TransactionName = TransactionNames.GetFileSizeResponse
                //});
                return response.FileSize;
            }
            catch (Exception ex)
            {
                //EventLogManager.Log(ex);
                throw;
            }
        }

        private static byte[] DownloadChunk(Guid documentId, int offSet, string entityCode, string groupingId)
        {
            var request = new DownloadChunkRequest
            {
                ClientCode = entityCode,
                DocumentId = documentId,
                Offset = offSet
            };

            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = groupingId,
            //    Data = XmlUtilities.SerializeObject(request),
            //    TransactionName = TransactionNames.DownloadFileChunkRequest
            //});
            var response = WebServiceHostManager.DslServiceHost.DownloadChunk(request);

            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = groupingId,
            //    Data = "Success",
            //    TransactionName = TransactionNames.DownloadFileChunkResponse
            //});
            return response.FileChunk;
        }

        public static void DownloadFile(Guid documentId, string entityCode, string downloadPath)
        {
            var groupingId = Guid.NewGuid().ToString();

            var request = new DownloadChunkRequest
            {
                ClientCode = entityCode,
                DocumentId = documentId
            };

            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = groupingId,
            //    Data = XmlUtilities.SerializeObject(request),
            //    TransactionName = TransactionNames.DownloadFileRequest
            //});
            try
            {
                var offSet = 0;
                var fileSize = GetFileSize(documentId, entityCode);
                int numRetries = 0;
                using (var fs = new FileStream(downloadPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var proxy = WebServiceHostManager.DslServiceHost;

                    fs.Seek(offSet, SeekOrigin.Begin);

                    // download the chunks from the web service one by one, until all the bytes have been read, meaning the entire file has been downloaded.
                    while (offSet < fileSize)
                    {
                        try
                        {
                            var buffer = DownloadChunk(documentId, offSet, entityCode, groupingId);

                            fs.Write(buffer, 0, buffer.Length);
                            offSet += buffer.Length; // save the offset position for resume
                        }
                        catch (Exception ex)
                        {
                            // swallow the exception and try again
                            //EventLogManager.Log(ex);

                            if (numRetries++ >= ConfigManager.MaxDslRetries) // too many retries, bail out
                            {
                                fs.Close();
                                throw new Exception("Error occurred during upload, too many retries.\r\n" + ex.Message);
                            }
                        }
                    }

                }

                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = "Success",
                //    TransactionName = TransactionNames.DownloadFileResponse
                //});
            }
            catch (Exception ex)
            {
                ////EventLogManager.Log(ex);
                throw;
            }
        }

        public static string UploadDslDocument(DslDocumentCategoryTypeView documentCategoryType,
            Stream fileStream, int fileLength, string fileName, string fileType, string username,
            string reference, string applicationName, string entityCode)
        {
            var docId = Guid.NewGuid();
            var request = new UploadFileRequest
            {
                ClientCode = entityCode,
                FileLength = fileLength,
                FileName = fileName,
                Username = username,
                FileType = fileType,
                DocumentCategoryId = documentCategoryType.ToString(),
                ApplicationName = applicationName,
                ApplicationCode = applicationName,
                ReferenceData = reference,
                DocumentId = docId
            };



            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = docId.ToString(),
            //    Data = XmlUtilities.SerialiseObjectPureXml(request),
            //    TransactionName = TransactionNames.UploadFileRequest
            //});
            ////
            try
            {
                var chunkSize = GetMaximumAllowedChunk();
                var offSet = 0;
                var numRetries = 0;
                var buffer = new byte[chunkSize];
                // Upload the document using the DSL Streaming Service
                WebServiceHostManager.DslServiceHost.CompleteFileUpload(request);
                using (fileStream)
                {
                    fileStream.Position = offSet;
                    int bytesRead;

                    // send the chunks to wcf, until FileStream.Read() returns 0, meaning the entire file has been read.
                    do
                    {
                        bytesRead = fileStream.Read(buffer, 0, chunkSize);

                        // check if this is the last chunk and resize the buffer
                        if (bytesRead != buffer.Length)
                        {
                            chunkSize = bytesRead;
                            var trimmedBuffer = new byte[bytesRead];
                            Array.Copy(buffer, trimmedBuffer, bytesRead);
                            buffer = trimmedBuffer;	// the trimmed buffer should become the new 'buffer'
                        }
                        if (buffer.Length == 0)
                            break;	// nothing more to send
                        try
                        {
                            // Append chunk to store 
                            AppendFileChunk(docId.ToString(), buffer, offSet);
                            offSet += bytesRead;	// save the offset position for resume
                        }
                        catch (Exception ex)
                        {
                            //EventLogManager.Log(new EventLog(ex) { SecondaryData = docId.ToString(), TransactionName = TransactionNames.UploadFileRequest });

                            // rewind the filestream and keep trying
                            fileStream.Position -= bytesRead;

                            if (numRetries++ >= ConfigSettingReader.FileAppendMaxRetry) // too many retries, bail out
                            {
                                fileStream.Close();
                                throw new Exception(String.Format("Error occurred during upload, too many retries. \n{0}", ex));
                            }
                        }
                    } while (bytesRead > 0);
                }


                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = docId.ToString(),
                //    Data = "Success",
                //    TransactionName = TransactionNames.UploadFileResponse
                //});

            }
            catch (Exception ex)
            {
                //EventLogManager.Log(ex);
                throw;
            }
            return docId.ToString();
        }

        public static string UploadDslDocument(string documentId, DslDocumentCategoryTypeView documentCategoryType,
            Stream fileStream, int fileLength, string fileName, string fileType, string username,
            string reference, string applicationName, string entityCode)
        {
            var docId = new Guid(documentId);
            var request = new UploadFileRequest
            {
                ClientCode = entityCode,
                FileLength = fileLength,
                FileName = fileName,
                Username = username,
                FileType = fileType,
                DocumentCategoryId = documentCategoryType.ToString(),
                ApplicationName = applicationName,
                ApplicationCode = applicationName,
                ReferenceData = reference,
                DocumentId = docId
            };



            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = docId.ToString(),
            //    Data = XmlUtilities.SerialiseObjectPureXml(request),
            //    TransactionName = TransactionNames.UploadFileRequest
            //});
            //
            try
            {
                var chunkSize = GetMaximumAllowedChunk();
                var offSet = 0;
                var numRetries = 0;
                var buffer = new byte[chunkSize];
                // Upload the document using the DSL Streaming Service
                WebServiceHostManager.DslServiceHost.CompleteFileUpload(request);
                using (fileStream)
                {
                    fileStream.Position = offSet;
                    int bytesRead;

                    // send the chunks to wcf, until FileStream.Read() returns 0, meaning the entire file has been read.
                    do
                    {
                        bytesRead = fileStream.Read(buffer, 0, chunkSize);

                        // check if this is the last chunk and resize the buffer
                        if (bytesRead != buffer.Length)
                        {
                            chunkSize = bytesRead;
                            var trimmedBuffer = new byte[bytesRead];
                            Array.Copy(buffer, trimmedBuffer, bytesRead);
                            buffer = trimmedBuffer;	// the trimmed buffer should become the new 'buffer'
                        }
                        if (buffer.Length == 0)
                            break;	// nothing more to send
                        try
                        {
                            // Append chunk to store 
                            AppendFileChunk(docId.ToString(), buffer, offSet);
                            offSet += bytesRead;	// save the offset position for resume
                        }
                        catch (Exception ex)
                        {
                            //EventLogManager.Log(new EventLog(ex) { SecondaryData = docId.ToString(), TransactionName = TransactionNames.UploadFileRequest });

                            // rewind the filestream and keep trying
                            fileStream.Position -= bytesRead;

                            if (numRetries++ >= ConfigSettingReader.FileAppendMaxRetry) // too many retries, bail out
                            {
                                fileStream.Close();
                                throw new Exception(String.Format("Error occurred during upload, too many retries. \n{0}", ex));
                            }
                        }
                    } while (bytesRead > 0);
                }


                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = docId.ToString(),
                //    Data = "Success",
                //    TransactionName = TransactionNames.UploadFileResponse
                //});

            }
            catch (Exception ex)
            {
                //EventLogManager.Log(ex);
                throw;
            }
            return docId.ToString();
        }

        public static DslDocumentCategoryView GetDocumentCategory(DslDocumentCategoryTypeView dslDocumentCategoryType)
        {
            GetDslCategoryRequest request;
            var groupingId = Guid.NewGuid().ToString();

            try
            {
                request = new GetDslCategoryRequest
                {
                    DslCategoryType = dslDocumentCategoryType.Convert()
                };

                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = XmlUtilities.SerializeObject(request),
                //    TransactionName = TransactionNames.GetDocumentCategoryRequest
                //});

                var response = WebServiceHostManager.DslServiceHost.GetDslCategory(request);

                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = XmlUtilities.SerializeObject(response),
                //    TransactionName = TransactionNames.GetDocumentCategoryResponse
                //});

                return response.DocumentCategory.Convert();
            }
            catch (Exception ex)
            {
                //EventLogManager.Log(ex);
                throw;
            }
        }

        public static void DeleteDocument(string documentId)
        {
            var request = new DeleteDslDocumentRequest { DocumentId = documentId };
            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = documentId,
            //    Data = XmlUtilities.SerializeObject(request),
            //    TransactionName = TransactionNames.DeleteDocumentCategoryRequest
            //});

            WebServiceHostManager.DslServiceHost.DeleteDslDocument(request);

            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = documentId,
            //    Data = "Success",
            //    TransactionName = TransactionNames.DeleteDocumentCategoryResponse
            //});
        }

        public static int GetMaximumAllowedChunk()
        {
            var groupingId = Guid.NewGuid().ToString();
            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = groupingId,
            //    Data = "Request",
            //    TransactionName = TransactionNames.GetAllowedChunkSizeRequest
            //});
            try
            {
                var response = WebServiceHostManager.DslServiceHost.GetMaximumChunkSize();
                //AuditLogManager.Log(new AuditLog
                //{
                //    AccountId = ConfigSettingReader.ClientCode,
                //    SecondaryData = groupingId,
                //    Data = "Success",
                //    TransactionName = TransactionNames.GetAllowedChunkSizeResponse
                //});
                return response.ChunkSize;
            }
            catch (Exception ex)
            {
                //EventLogManager.Log(new EventLog(ex) { TransactionName = TransactionNames.GetAllowedChunkSizeRequest, SecondaryData = groupingId });
                throw;
            }
        }

        private static void AppendFileChunk(string documentId, byte[] fileChunk, int offSet)
        {
            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = documentId,
            //    Data = string.Format("DocumentId: {0}, Offset: {1}", documentId, offSet),
            //    TransactionName = TransactionNames.UploadFileChunkRequest
            //});
            var request = new AppendFileChunkRequest
            {
                FileId = new Guid(documentId),
                Offset = offSet,
                Buffer = fileChunk
            };
            WebServiceHostManager.DslServiceHost.AppendFileChunk(request);
            //AuditLogManager.Log(new AuditLog
            //{
            //    AccountId = ConfigSettingReader.ClientCode,
            //    SecondaryData = documentId,
            //    Data = "Success",
            //    TransactionName = TransactionNames.UploadFileChunkResponse
            //});
        }

        #region Helpers
        public static string AcceptedFilesToString(this DslDocumentCategoryView dslCategory)
        {
            string acceptedFiles = string.Empty;
            foreach (string s in dslCategory.AcceptedFileTypes)
            {
                acceptedFiles += string.Format("{0},", s);
            }
            acceptedFiles = acceptedFiles.Substring(0, acceptedFiles.LastIndexOf(','));

            return acceptedFiles;
        }

        public static decimal ConvertedMaxSizeToMegabytes(this  DslDocumentCategoryView dslCategory)
        {
            decimal byteDivider = 1048576;
            decimal dslMax = (decimal)dslCategory.MaximumFileSize;
            return dslMax / byteDivider;
        }
        #endregion
    }
}
