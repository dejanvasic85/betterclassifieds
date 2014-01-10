using System;
using System.IO;
using System.Linq;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.DataService;
using Paramount.Common.DataTransferObjects.DSL;

namespace Paramount.DSL.UIController
{
    public static class DslController
    {
        public static long GetFileSize(Guid documentId, string entityCode)
        {
            return DslDataService.GetFileSize(documentId, entityCode);
        }

        private static byte[] DownloadChunk(Guid documentId, int offSet, string entityCode)
        {
            return DslDataService.DownloadFileChunk(documentId, entityCode, offSet, ConfigSettingReader.DslChunkLength);
        }

        public static void DownloadFile(Guid documentId, string entityCode, string downloadPath)
        {
            var offSet = 0;
            var fileSize = GetFileSize(documentId, entityCode);
            using (var fs = new FileStream(downloadPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Seek(offSet, SeekOrigin.Begin);

                // download the chunks from the web service one by one, until all the bytes have been read, meaning the entire file has been downloaded.
                while (offSet < fileSize)
                {
                    var buffer = DownloadChunk(documentId, offSet, entityCode);

                    fs.Write(buffer, 0, buffer.Length);
                    offSet += buffer.Length; // save the offset position for resume
                }
            }
        }

        public static string UploadDslDocument(DslDocumentCategoryType documentCategoryType,
            Stream fileStream, int fileLength, string fileName, string fileType, string username,
            string reference, string applicationName, string entityCode)
        {
            var docId = Guid.NewGuid();

            var chunkSize = GetMaximumAllowedChunk();
            var offSet = 0;
            var numRetries = 0;
            var buffer = new byte[chunkSize];

            DslDataService.InsertFile(docId, null, documentCategoryType, applicationName, DateTime.Now, null,
                entityCode, fileName, fileType, username, true, reference, fileLength);

            using (fileStream)
            {
                fileStream.Position = offSet;
                int bytesRead;

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


            return docId.ToString();
        }

        public static string UploadDslDocument(string documentId, DslDocumentCategoryType documentCategoryType,
            Stream fileStream, int fileLength, string fileName, string fileType, string username,
            string reference, string applicationName, string entityCode)
        {
            var docId = new Guid(documentId);

            var chunkSize = GetMaximumAllowedChunk();
            var offSet = 0;
            var numRetries = 0;
            var buffer = new byte[chunkSize];

            // Upload the document using the DSL Streaming Service
            DslDataService.InsertFile(docId, null, documentCategoryType, applicationName, DateTime.Now, null,
                    entityCode, fileName, fileType, username, true, reference, fileLength);

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
            return docId.ToString();
        }

        public static DslDocumentCategory GetDocumentCategory(DslDocumentCategoryType dslDocumentCategoryType)
        {
            return DslDataService.GetDocumentCategoryByCode(dslDocumentCategoryType);
        }

        public static void DeleteDocument(string documentId)
        {
            DslDataService.DeleteDocument(new Guid(documentId));
        }

        public static int GetMaximumAllowedChunk()
        {
            return ConfigSettingReader.DslChunkLength;
        }

        private static void AppendFileChunk(string documentId, byte[] fileChunk, int offSet)
        {
            DslDataService.AppendChunk(new Guid(documentId), fileChunk, offSet);
        }

        #region Helpers
        public static string AcceptedFilesToString(this DslDocumentCategory dslCategory)
        {
            string acceptedFiles = dslCategory.AcceptedFileTypes.Aggregate(string.Empty, (current, s) => current + string.Format("{0},", s));
            acceptedFiles = acceptedFiles.Substring(0, acceptedFiles.LastIndexOf(','));
            return acceptedFiles;
        }

        public static decimal ConvertedMaxSizeToMegabytes(this  DslDocumentCategory dslCategory)
        {
            const decimal byteDivider = 1048576;
            decimal dslMax = (decimal)dslCategory.MaximumFileSize;
            return dslMax / byteDivider;
        }
        #endregion

        public static Stream DownloadDocumentToStream(Guid documentId, string entityCode)
        {
            var offSet = 0;
            var fileSize = GetFileSize(documentId, entityCode);
            var stream = new MemoryStream();
            stream.Seek(offSet, SeekOrigin.Begin);
            while (offSet < fileSize)
            {
                var buffer = DownloadChunk(documentId, offSet, entityCode);

                stream.Write(buffer, 0, buffer.Length);
                offSet += buffer.Length; // save the offset position for resume
            }
            return stream;
        }
    }
}
