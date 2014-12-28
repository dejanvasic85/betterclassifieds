﻿using System.Collections.Generic;
using System.Configuration;

namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web.Services.Protocols;
    using ApplicationBlock.Configuration;
    using ApplicationBlock.Data;
    using Common.DataTransferObjects.DSL;
    
    [Obsolete]
    public static class DslDataService
    {
        private const string ConnectionName = "DocumentConnection";
        private const int DefaultStreamSize = 4096;

        public static void AppendChunk(Guid documentId, byte[] buffer, int offSet)
        {
            //ClearFile(documentId);
            var df = new DatabaseProxy(Proc.DocumentStorageUpdateChunks.Name, ConnectionName);
            df.AddParameter(Proc.DocumentStorageUpdateChunks.Params.DocumentId, documentId);
            df.AddParameter(Proc.DocumentStorageUpdateChunks.Params.FileData, buffer);
            df.AddParameter(Proc.DocumentStorageUpdateChunks.Params.Offset, offSet);
            df.ExecuteNonQuery();
        }

        public static void InsertFile(
            Guid documentId,
            string accountId,
            DslDocumentCategoryType documentCategory,
            string applicationCode,
            DateTime? startDate,
            DateTime? endDate,
            string entityCode,
            string fileName,
            string fileType,
            string username,
            bool isPrivate,
            string reference,
            int fileLength)
        {
            var df = new DatabaseProxy(Proc.DocumentStorageInsert.Name, ConnectionName);

            #region Insert Parameters

            if (entityCode.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.EntityCode, entityCode, StringType.VarChar);
            }

            if (accountId.HasValue())
            {
                var account = new Guid(accountId);
                df.AddParameter(Proc.DocumentStorageInsert.Params.AccountId, account);
            }

            df.AddParameter(Proc.DocumentStorageInsert.Params.CategoryCode, (int)documentCategory);

            if (startDate.HasValue)
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.StartDate, startDate.Value);
            }

            if (endDate.HasValue)
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.EndDate, endDate.Value);
            }

            if (username.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.Username, username, StringType.VarChar);
            }

            if (reference.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.Reference, reference, StringType.VarChar);
            }

            df.AddParameter(Proc.DocumentStorageInsert.Params.ApplicationCode, applicationCode, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.FileName, fileName, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.FileType, fileType, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.IsPrivate, isPrivate);
            df.AddParameter(Proc.DocumentStorageInsert.Params.DocumentId, documentId);
            df.ExecuteNonQuery();

            #endregion
        }

        public static void ClearFile(Guid documentId)
        {
            var spClearUpdate = new DatabaseProxy(Proc.DocumentStorageClearUpdate.Name, ConnectionName);
            spClearUpdate.AddParameter(Proc.DocumentStorageClearUpdate.Params.DocumentId, documentId);
            spClearUpdate.ExecuteNonQuery();
        }

        public static DslDocument CreateDocument(Guid? documentId, string accountId, DslDocumentCategoryType documentCategory,
            string applicationCode, DateTime? startDate, DateTime? endDate, string entityCode, Stream fileData, string fileName,
            string fileType, string username, bool isPrivate, string reference, int fileLength)
        {
            // Stored proc to insert reference data
            var df = new DatabaseProxy(Proc.DocumentStorageInsert.Name, ConnectionName);

            #region Insert Parameters

            if (documentId.HasValue)
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.DocumentId, documentId.Value);
            }

            if (entityCode.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.EntityCode, entityCode, StringType.VarChar);
            }

            if (accountId.HasValue())
            {
                Guid account = new Guid(accountId);
                df.AddParameter(Proc.DocumentStorageInsert.Params.AccountId, account);
            }

            df.AddParameter(Proc.DocumentStorageInsert.Params.CategoryCode, (int)documentCategory);

            if (startDate.HasValue)
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.StartDate, startDate.Value);
            }

            if (endDate.HasValue)
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.EndDate, endDate.Value);
            }

            if (username.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.Username, username, StringType.VarChar);
            }

            if (reference.HasValue())
            {
                df.AddParameter(Proc.DocumentStorageInsert.Params.Reference, reference, StringType.VarChar);
            }

            df.AddParameter(Proc.DocumentStorageInsert.Params.ApplicationCode, applicationCode, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.FileName, fileName, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.FileType, fileType, StringType.VarChar);
            df.AddParameter(Proc.DocumentStorageInsert.Params.IsPrivate, isPrivate);

            #endregion

            // Perform save on the reference data and get the document details into object to return
            var table = df.ExecuteQuery().Tables[0];
            DslDocument document = table.Rows[0].ToDslDocument();

            // Call stored proc to clear update
            var spClearUpdate = new DatabaseProxy(Proc.DocumentStorageClearUpdate.Name, ConnectionName);
            spClearUpdate.AddParameter(Proc.DocumentStorageClearUpdate.Params.DocumentId, document.DocumentId);
            spClearUpdate.ExecuteNonQuery();

            // Perform Chunking to DB
            var spUpdateChunks = new DatabaseProxy(Proc.DocumentStorageUpdateChunks.Name, ConnectionName);
            int chunkLength = ConfigSettingReader.DslChunkLength;
            var byteData = new byte[chunkLength];
            int totalSizeCount = 0;
            int currentPosition = 0;
            int readAmount = DefaultStreamSize;
            bool isLastChunk = false;

            while (totalSizeCount < fileLength)
            {
                if (fileLength - totalSizeCount < DefaultStreamSize)
                {
                    readAmount = fileLength - totalSizeCount;
                    isLastChunk = true;
                }
                readAmount = fileData.Read(byteData, currentPosition, (readAmount < DefaultStreamSize ? readAmount : DefaultStreamSize));
                currentPosition = currentPosition + readAmount;

                if (currentPosition == chunkLength || isLastChunk)
                {
                    // process the required chunk to Db
                    spUpdateChunks.ClearParamers();
                    spUpdateChunks.AddParameter(Proc.DocumentStorageUpdateChunks.Params.DocumentId, document.DocumentId);
                    spUpdateChunks.AddParameter(Proc.DocumentStorageUpdateChunks.Params.FileData, byteData);
                    spUpdateChunks.ExecuteNonQuery();

                    // reset the byte array
                    byteData = new byte[chunkLength];
                    // reset the current position to go to start again
                    currentPosition = 0;
                }

                totalSizeCount += (readAmount < DefaultStreamSize ? readAmount : DefaultStreamSize);
            }
            return document;
        }

        public static long GetFileSize(Guid documentId, string entityCode)
        {
            var document = GetDocumentItem(documentId, entityCode);

            if (document == null)
            {
                CustomSoapException("FileNotFound", string.Format("Requested file does not exist - File Id {0}", document));
                return 0;
            }
            return document.FileLength;
        }

        public static DslDocument GetDocumentItem(Guid documentId, string entityCode)
        {
            var spDocumentSelect = new DatabaseProxy(Proc.DocumentStorageSelect.Name, ConnectionName);
            spDocumentSelect.AddParameter(Proc.DocumentStorageSelect.Param.DocumentId, documentId);
            if (entityCode.HasValue())
            {
                spDocumentSelect.AddParameter(Proc.DocumentStorageSelect.Param.EntityCode, entityCode, StringType.VarChar);
            }

            var table = spDocumentSelect.ExecuteQuery().Tables[0];
            if (table.Rows.Count == 0)
            {
                throw new ApplicationException("The requested document cannot be found");
            }
            return table.Rows[0].ToDslDocument();
        }

        public static byte[] DownloadFileChunk(Guid documentId, string entityCode, int offset, int bufferSize)
        {
            var document = GetDocumentItem(documentId, entityCode);

            // if the requested Offset is larger than the file, quit.
            if (offset > document.FileLength)
                CustomSoapException("Invalid Download Offset", String.Format("The file size is {0}, received request for offset {1}", document.FileLength, offset));

            var sqlConnection = new SqlConnection(ConfigReader.GetConnectionString(ConnectionName));

            var tempBuffer = new byte[bufferSize];
            using (var sqlCommand = new SqlCommand(Proc.DocumentStorageSelectFileData.Name, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(Proc.DocumentStorageSelectFileData.Param.DocumentId, documentId.ToString());
                sqlCommand.Connection.Open();

                using (var sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    sqlDataReader.Read();

                    var sqlRead = sqlDataReader.GetBytes(0, offset, tempBuffer, 0, bufferSize);

                    if (sqlRead != bufferSize)
                    {
                        // the last chunk will almost certainly not fill the buffer, so it must be trimmed before returning
                        var trimmedBuffer = new byte[sqlRead];
                        Array.Copy(tempBuffer, trimmedBuffer, sqlRead);
                        return trimmedBuffer;
                    }
                    return tempBuffer;
                }
            }
        }

        public static DslDocument GetDocument(Guid documentId, string entityCode)
        {
            var document = GetDocumentItem(documentId, entityCode);
            // now fetch the file content
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            Int64 sqlRead = 0;
            Int64 index = 0;
            int chunkLength = ConfigSettingReader.DslChunkLength;
            byte[] bytes = new byte[chunkLength];

            var sqlConnection = new SqlConnection(ConfigReader.GetConnectionString(ConnectionName));
            using (var sqlCommand = new SqlCommand(Proc.DocumentStorageSelectFileData.Name, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(Proc.DocumentStorageSelectFileData.Param.DocumentId, documentId.ToString());
                sqlCommand.Connection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Read();

                        while ((sqlRead = sqlDataReader.GetBytes(0, index, bytes, 0, bytes.Length)) != 0)
                        {
                            binaryWriter.Write(bytes, 0, (int)sqlRead);
                            index += sqlRead;
                        }
                    }
                }
            }

            // set the stream for the file data into the Document object!
            document.FileData = stream;
            sqlConnection.Close();

            return document;
        }

        public static DslDocumentCategory GetDocumentCategoryByCode(DslDocumentCategoryType documentCategoryType)
        {
            DslDocumentCategory documentCategory = new DslDocumentCategory();

            var spDocumentCategory = new DatabaseProxy(Proc.DocumentCategorySelect.Name, ConnectionName);
            spDocumentCategory.AddParameter(Proc.DocumentCategorySelect.Param.CategoryCode, (int)documentCategoryType);

            var table = spDocumentCategory.ExecuteQuery().Tables[0];
            if (table.Rows.Count == 0)
            {
                throw new ApplicationException("The requested document category cannot be found.");
            }
            documentCategory = table.Rows[0].ToDslDocumentCategory();

            return documentCategory;
        }

        public static void DeleteDocument(Guid documentId)
        {
            var spDocumentDelete = new DatabaseProxy(Proc.DocumentStorageDelete.Name, ConnectionName);
            spDocumentDelete.AddParameter(Proc.DocumentStorageDelete.Param.DocumentId, documentId.ToString(), StringType.VarChar);
            spDocumentDelete.ExecuteNonQuery();
        }

        #region Exception Handling
        /// <summary>
        /// Throws a soap exception.  It is formatted in a way that is more readable to the client, after being put through the xml serialisation process
        /// Typed exceptions don't work well across web services, so these exceptions are sent in such a way that the client
        /// can determine the 'name' or type of the exception thrown, and any message that went with it, appended after a : character.
        /// </summary>
        /// <param name="exceptionName"></param>
        /// <param name="message"></param>
        public static void CustomSoapException(string exceptionName, string message)
        {
            throw new SoapException(exceptionName + ": " + message, new System.Xml.XmlQualifiedName("BufferedUpload"));
        }

        #endregion
    }

    internal static class Extensions
    {
        public static DslDocument ToDslDocument(this DataRow row)
        {
            var document = new DslDocument();

            document.DocumentId = (Guid)row["DocumentId"];
            document.ApplicationCode = row["ApplicationCode"].ToString();
            document.EntityCode = row["EntityCode"].ToString();
            document.AccounId = (row["AccountId"].ToString().HasValue() ? (Guid)row["AccountId"] : new Guid?());
            document.DocumentCategory = (DslDocumentCategoryType)row["DocumentCategoryId"];
            document.Username = row["Username"].ToString();
            document.FileType = row["FileType"].ToString();
            document.FileLength = (int)row["FileLength"];
            document.FileName = row["FileName"].ToString();
            document.Reference = row["Reference"].ToString();
            document.IsPrivate = false;
            document.StartDate = (row["StartDate"].ToString().HasValue() ? (DateTime)row["StartDate"] : new DateTime?());
            document.EndDate = (row["EndDate"].ToString().HasValue() ? (DateTime)row["EndDate"] : new DateTime?());
            document.CreatedDate = (row["CreatedDate"].ToString().HasValue() ? (DateTime)row["CreatedDate"] : new DateTime?());
            document.UpdatedDate = (row["UpdatedDate"].ToString().HasValue() ? (DateTime)row["UpdatedDate"] : new DateTime?());

            return document;
        }

        public static DslDocumentCategory ToDslDocumentCategory(this DataRow row)
        {
            var documentCategory = new DslDocumentCategory();
            documentCategory.DocumentCategoryId = (int)row["DocumentCategoryId"];
            documentCategory.Title = row["Title"].ToString();
            documentCategory.Code = (DslDocumentCategoryType)row["Code"];
            documentCategory.ExpiryPurgeDays = (row["ExpiryPurgeDays"].ToString().HasValue() ? (int?)row["ExpiryPurgeDays"] : null);

            // extract the accepted file types
            string[] acceptedFiles = row["AcceptedFileTypes"].ToString().Split(';');
            documentCategory.AcceptedFileTypes = new List<string>();
            foreach (string file in acceptedFiles)
            {
                documentCategory.AcceptedFileTypes.Add(file.ToLower());
            }

            documentCategory.MaximumFileSize = (row["MaximumFileSize"].ToString().HasValue() ? (int?)row["MaximumFileSize"] : null);
            return documentCategory;
        }
    }
}
