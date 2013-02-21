namespace Paramount.Common.DataTransferObjects.DSL.Messages
{
    using System;
    using System.ServiceModel;

    [MessageContract]
    public class CreateDslDocumentRequest : IDisposable
    {
        [MessageHeader(MustUnderstand = true, Name = "DocumentId")]
        public Guid DocumentId;

        [MessageHeader(MustUnderstand = true, Name = "ApplicationCode")]
        public string ApplicationCode;

        [MessageHeader(MustUnderstand = true, Name = "EntityCode")]
        public string EntityCode;

        [MessageHeader(MustUnderstand = true, Name = "AccountId")]
        public string AccountId;

        [MessageHeader(MustUnderstand = true, Name = "Username")]
        public string Username;

        [MessageHeader(MustUnderstand = true, Name = "FileName")]
        public string FileName;

        [MessageHeader(MustUnderstand = true, Name = "FileType")]
        public string FileType;

        [MessageHeader(MustUnderstand = true, Name = "FileLength")]
        public int FileLength;

        [MessageHeader(MustUnderstand = true, Name = "ReferenceData")]
        public string ReferenceData;

        [MessageHeader(MustUnderstand = true, Name = "IsPrivate")]
        public bool IsPrivate;

        [MessageHeader(MustUnderstand = true, Name = "StartDate")]
        public DateTime? StartDate { get; set; }

        [MessageHeader(MustUnderstand = true, Name = "EndDate")]
        public DateTime? EndDate { get; set; }

        [MessageHeader(MustUnderstand = true, Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [MessageHeader(MustUnderstand = true, Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [MessageHeader(MustUnderstand = true, Name = "DocumentCategory")]
        public DslDocumentCategoryType DocumentCategory { get; set; }

        [MessageBodyMember(Order = 1, Name = "FileData")]
        public System.IO.Stream FileData;


        public void Dispose()
        {
            if (FileData != null)
            {
                FileData.Dispose();
                FileData = null;
            }
        }
    }
}